﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using mrbBase;
using mrbBase.Base.Master_Classes;

namespace Mids_Reborn
{
    public static class clsGenFreebies
    {
        private const int EnhancementsTrayCapacity = 70;
        private const string BoostCmd = "boost";
        private const string CmdSeparator = "$$";
        private const bool AutoAttune = true;
        public const string MenuName = "MRBTest";
        public const string MenuExt = "mnu";

        private static List<List<string>> GenerateBoostChunks()
        {
            var k = 0;
            var l = 0;
            List<List<string>> commandChunks = new List<List<string>>();
            IEnhancement enhData;
            EnhancementSet enhSet;
            string enhUID;

            foreach (var p in MidsContext.Character.CurrentBuild.Powers.Where(p => p.State != Enums.ePowerState.Empty))
            {
                for (var j = 0; j < p.Slots.Length; j++)
                {
                    if (p.Slots[j].Enhancement.Enh < 0) continue; // Empty slot
                    if (k % EnhancementsTrayCapacity == 0)
                    {
                        commandChunks.Add(new List<string>());
                        if (k > 0) l++;
                    }

                    enhData = DatabaseAPI.Database.Enhancements[p.Slots[j].Enhancement.Enh];
                    if (enhData.nIDSet > -1)
                    {
                        enhSet = DatabaseAPI.Database.EnhancementSets[enhData.nIDSet];
                        if (AutoAttune & enhSet.LevelMax < 49)
                        {
                            enhUID = enhData.UID.Replace("Crafted_", "Attuned_");
                            commandChunks[l].Add($"{BoostCmd} {enhUID} {enhUID} 1");
                        }
                        else
                        {
                            commandChunks[l].Add($"{BoostCmd} {enhData.UID} {enhData.UID} 50");
                        }
                    }
                    else
                    {
                        commandChunks[l].Add($"{BoostCmd} {enhData.UID} {enhData.UID} 50");
                    }

                    k++;
                }
            }

            return commandChunks;
        }

        public static string GenerateMenu()
        {
            if (MainModule.MidsController.Toon == null) return string.Empty;

            List<List<string>> commandChunks = GenerateBoostChunks();
            var dateTag = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss", null);
            var mnuStr = "// Generated by " + Application.ProductName + " v" + Application.ProductVersion + " - " + dateTag + "\r\n";
            mnuStr += "// Open the menu ingame: /popmenu " + MenuName + "\r\n\r\n";
            mnuStr += "Menu \"" + MenuName + "\"\r\n";
            mnuStr += "{\r\n";
            mnuStr += "\tTitle \"" + (string.IsNullOrWhiteSpace(MainModule.MidsController.Toon.Name) ? "Test build" : MainModule.MidsController.Toon.Name.Trim() + " test build") + "\"\r\n";
            mnuStr += "\tDIVIDER\r\n";

            for (var i = 0; i < commandChunks.Count; i++)
            {
                mnuStr += "\tOption \"Give enhancements (part " + Convert.ToString(i + 1, null) + ")\" \"" + string.Join(CmdSeparator, commandChunks[i].ToArray()) + "\"\r\n";
            }

            mnuStr += "\tDIVIDER\r\n";
            mnuStr += "\tLockedOption\r\n";
            mnuStr += "\t{\r\n";
            mnuStr += "\t\tDisplayName \"" + Application.ProductName + " v" + Application.ProductVersion + "\"\r\n";
            mnuStr += "\t\tBadge \"X\"\r\n";
            mnuStr += "\t}\r\n";
            mnuStr += "\tLockedOption\r\n";
            mnuStr += "\t{\r\n";
            mnuStr += "\t\tDisplayName \"Generated: " + dateTag + "\"\r\n";
            mnuStr += "\t\tBadge \"X\"\r\n";
            mnuStr += "\t}\r\n";
            mnuStr += "}";

            return mnuStr;
        }

        public static bool SaveTo(string file)
        {
            var mnuStr = GenerateMenu();
            if (string.IsNullOrEmpty(mnuStr)) return false;
            try
            {
                using var sw = new StreamWriter(file);
                sw.Write(mnuStr);
                sw.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}