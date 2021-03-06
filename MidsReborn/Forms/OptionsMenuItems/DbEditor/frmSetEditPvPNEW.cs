﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using mrbBase;
using mrbBase.Base.Data_Classes;
using mrbBase.Base.Display;

namespace Mids_Reborn.Forms.OptionsMenuItems.DbEditor
{
    public partial class frmSetEditPvPNEW : Form
    {
        public readonly EnhancementSet mySet;
        private bool Loading;
        private int[] SetBonusList;
        private int[] SetBonusListPVP;

        public frmSetEditPvPNEW(ref EnhancementSet iSet)
        {
            Load += frmSetEdit_Load;
            SetBonusList = new int[0];
            Loading = true;
            InitializeComponent();
            Name = nameof(frmSetEditPvPNEW);
            var componentResourceManager = new ComponentResourceManager(typeof(frmSetEditPvPNEW));
            Icon = Resources.reborn;
            btnImage.Image = Resources.enhData;
            mySet = new EnhancementSet(iSet);
        }

        private int BonusID()
        {
            return cbSlotCount.SelectedIndex;
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            if (Loading)
                return;
            ImagePicker.InitialDirectory = I9Gfx.GetEnhancementsPath();
            ImagePicker.FileName = mySet.Image;
            if (ImagePicker.ShowDialog() == DialogResult.OK)
            {
                var str = FileIO.StripPath(ImagePicker.FileName);
                if (!File.Exists(FileIO.AddSlash(ImagePicker.InitialDirectory) + str))
                {
                    MessageBox.Show(
                        $"You must select an image from the {I9Gfx.GetEnhancementsPath()} folder!\r\n\r\nIf you are adding a new image, you should copy it to the folder and then select it.",
                        @"Select Image", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    mySet.Image = str;
                    DisplayIcon();
                }
            }
        }

        private void btnNoImage_Click(object sender, EventArgs e)
        {
            mySet.Image = "";
            DisplayIcon();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mySet.LevelMin = Convert.ToInt32(decimal.Subtract(udMinLevel.Value, new decimal(1)));
            mySet.LevelMax = Convert.ToInt32(decimal.Subtract(udMaxLevel.Value, new decimal(1)));
            DialogResult = DialogResult.OK;
            Hide();
        }

        private void cbSetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Loading)
                return;
            mySet.SetType = (Enums.eSetType) cbSetType.SelectedIndex;
        }

        private void cbSlotX_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Loading)
                return;
            DisplayBonus();
            DisplayBonusText();
        }

        private void udMaxLevel_Leave(object sender, EventArgs e)
        {
            SetMaxLevel((int) Math.Round(Conversion.Val(udMaxLevel.Text)));
            mySet.LevelMax = Convert.ToInt32(decimal.Subtract(udMaxLevel.Value, new decimal(1)));
        }

        private void udMaxLevel_ValueChanged(object sender, EventArgs e)
        {
            if (Loading)
                return;
            mySet.LevelMax = Convert.ToInt32(decimal.Subtract(udMaxLevel.Value, new decimal(1)));
            udMinLevel.Maximum = udMaxLevel.Value;
        }

        private void udMinLevel_Leave(object sender, EventArgs e)
        {
            SetMinLevel((int) Math.Round(Conversion.Val(udMinLevel.Text)));
            mySet.LevelMin = Convert.ToInt32(decimal.Subtract(udMinLevel.Value, new decimal(1)));
        }

        private void udMinLevel_ValueChanged(object sender, EventArgs e)
        {
            if (Loading)
                return;
            mySet.LevelMin = Convert.ToInt32(decimal.Subtract(udMinLevel.Value, new decimal(1)));
            udMaxLevel.Minimum = udMinLevel.Value;
        }

        private void txtAlternate_TextChanged(object sender, EventArgs e)
        {
            if (isBonus())
                mySet.Bonus[BonusID()].AltString = txtAlternate.Text;
            else if (isSpecial())
                mySet.SpecialBonus[SpecialID()].AltString = txtAlternate.Text;
            DisplayBonusText();
        }

        private void txtDesc_TextChanged(object sender, EventArgs e)
        {
            if (Loading)
                return;
            mySet.Desc = txtDesc.Text;
        }

        private void txtInternal_TextChanged(object sender, EventArgs e)

        {
            if (Loading)
                return;
            mySet.Uid = txtInternal.Text;
        }

        private void txtNameFull_TextChanged(object sender, EventArgs e)
        {
            if (Loading)
                return;
            mySet.DisplayName = txtNameFull.Text;
        }

        private void txtNameShort_TextChanged(object sender, EventArgs e)
        {
            if (Loading)
                return;
            mySet.ShortName = txtNameShort.Text;
        }

        private void lstBonus_DoubleClick(object sender, EventArgs e)
        {
            if (lstBonus.SelectedIndex < 0)
                return;
            var selectedIndex = lstBonus.SelectedIndex;
            var index1 = 0;
            if (isBonus())
            {
                var numArray2 = new int[mySet.Bonus[BonusID()].Index.Length - 2 + 1];
                var strArray2 = new string[mySet.Bonus[BonusID()].Name.Length - 2 + 1];
                var num1 = mySet.Bonus[BonusID()].Index.Length - 1;
                for (var index2 = 0; index2 <= num1; ++index2)
                {
                    if (index2 == selectedIndex)
                        continue;
                    numArray2[index1] = mySet.Bonus[BonusID()].Index[index2];
                    strArray2[index1] = mySet.Bonus[BonusID()].Name[index2];
                    ++index1;
                }

                mySet.Bonus[BonusID()].Name = new string[numArray2.Length - 1 + 1];
                mySet.Bonus[BonusID()].Index = new int[strArray2.Length - 1 + 1];
                var num2 = numArray2.Length - 1;
                for (var index2 = 0; index2 <= num2; ++index2)
                {
                    mySet.Bonus[BonusID()].Index[index2] = numArray2[index2];
                    mySet.Bonus[BonusID()].Name[index2] = strArray2[index2];
                }
            }
            else if (isSpecial())
            {
                var numArray2 = new int[mySet.SpecialBonus[SpecialID()].Index.Length - 2 + 1];
                var strArray2 = new string[mySet.SpecialBonus[SpecialID()].Name.Length - 2 + 1];
                var num1 = mySet.SpecialBonus[SpecialID()].Index.Length - 1;
                for (var index2 = 0; index2 <= num1; ++index2)
                {
                    if (index2 == selectedIndex)
                        continue;
                    numArray2[index1] = mySet.SpecialBonus[SpecialID()].Index[index2];
                    strArray2[index1] = mySet.SpecialBonus[SpecialID()].Name[index2];
                    ++index1;
                }

                mySet.SpecialBonus[SpecialID()].Name = new string[numArray2.Length - 1 + 1];
                mySet.SpecialBonus[SpecialID()].Index = new int[strArray2.Length - 1 + 1];
                var num2 = numArray2.Length - 1;
                for (var index2 = 0; index2 <= num2; ++index2)
                {
                    mySet.SpecialBonus[SpecialID()].Index[index2] = numArray2[index2];
                    mySet.SpecialBonus[SpecialID()].Name[index2] = strArray2[index2];
                }

                if (mySet.SpecialBonus[SpecialID()].Index.Length < 1)
                    mySet.SpecialBonus[SpecialID()].Special = -1;
            }

            DisplayBonus();
            DisplayBonusText();
        }

        private void lvBonusList_DoubleClick(object sender, EventArgs e)
        {
            if (lvBonusList.SelectedIndices.Count < 1) return;

            var index = Convert.ToInt32(lvBonusList.SelectedItems[0].Tag);
            if (index < 0)
            {
                MessageBox.Show(@"Tag was < 0!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (isBonus())
                {
                    var BonusPower = lvBonusList.SelectedItems[0].Text;
                    var BonusMode = BonusPower.Contains("(PVP Only)") ? Enums.ePvX.PvP : Enums.ePvX.Any;
                    mySet.Bonus[BonusID()].PvMode = BonusMode;
                    mySet.Bonus[BonusID()].Name = (string[]) Utils.CopyArray(mySet.Bonus[BonusID()].Name,
                        new string[mySet.Bonus[BonusID()].Name.Length + 1]);
                    mySet.Bonus[BonusID()].Index = (int[]) Utils.CopyArray(mySet.Bonus[BonusID()].Index,
                        new int[mySet.Bonus[BonusID()].Index.Length + 1]);
                    mySet.Bonus[BonusID()].Name[mySet.Bonus[BonusID()].Name.Length - 1] =
                        DatabaseAPI.Database.Power[index].FullName;
                    mySet.Bonus[BonusID()].Index[mySet.Bonus[BonusID()].Index.Length - 1] = index;
                }
                else if (isSpecial())
                {
                    mySet.SpecialBonus[SpecialID()].Special = SpecialID();
                    mySet.SpecialBonus[SpecialID()].Name = (string[]) Utils.CopyArray(
                        mySet.SpecialBonus[SpecialID()].Name,
                        new string[mySet.SpecialBonus[SpecialID()].Name.Length + 1]);
                    mySet.SpecialBonus[SpecialID()].Index = (int[]) Utils.CopyArray(
                        mySet.SpecialBonus[SpecialID()].Index,
                        new int[mySet.SpecialBonus[SpecialID()].Index.Length + 1]);
                    mySet.SpecialBonus[SpecialID()].Name[mySet.SpecialBonus[SpecialID()].Name.Length - 1] =
                        DatabaseAPI.Database.Power[index].FullName;
                    mySet.SpecialBonus[SpecialID()].Index[mySet.SpecialBonus[SpecialID()].Index.Length - 1] = index;
                }

                DisplayBonus();
                DisplayBonusText();
            }
        }

        private void SetMaxLevel(int iValue)
        {
            if (decimal.Compare(new decimal(iValue), udMaxLevel.Minimum) < 0)
                iValue = Convert.ToInt32(udMaxLevel.Minimum);
            if (decimal.Compare(new decimal(iValue), udMaxLevel.Maximum) > 0)
                iValue = Convert.ToInt32(udMaxLevel.Maximum);
            udMaxLevel.Value = new decimal(iValue);
        }

        private void SetMinLevel(int iValue)
        {
            if (decimal.Compare(new decimal(iValue), udMinLevel.Minimum) < 0)
                iValue = Convert.ToInt32(udMinLevel.Minimum);
            if (decimal.Compare(new decimal(iValue), udMinLevel.Maximum) > 0)
                iValue = Convert.ToInt32(udMinLevel.Maximum);
            udMinLevel.Value = new decimal(iValue);
        }

        private int SpecialID()
        {
            var num1 = mySet.Enhancements.Length;
            var num2 = num1 + num1 - 2;
            return cbSlotCount.SelectedIndex - num2;
        }

        private void DisplayBonus()
        {
            try
            {
                lstBonus.BeginUpdate();
                lstBonus.Items.Clear();
                if (isBonus())
                {
                    var index1 = BonusID();
                    var num = mySet.Bonus[index1].Index.Length - 1;
                    for (var index2 = 0; index2 <= num; ++index2)
                    {
                        lstBonus.Items.Add(DatabaseAPI.Database.Power[mySet.Bonus[index1].Index[index2]].PowerName);
                        txtAlternate.Text = mySet.Bonus[index1].AltString;
                    }
                }
                else if (isSpecial())
                {
                    var index1 = SpecialID();
                    var num = mySet.SpecialBonus[index1].Index.Length - 1;
                    for (var index2 = 0; index2 <= num; ++index2)
                        lstBonus.Items.Add(DatabaseAPI.Database.Power[mySet.SpecialBonus[index1].Index[index2]]
                            .PowerName);
                    txtAlternate.Text = mySet.SpecialBonus[index1].AltString;
                }

                lstBonus.EndUpdate();
                cbSlotCount.Enabled = mySet.Enhancements.Length > 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n\n{ex.StackTrace}");
                //ProjectData.SetProjectError(ex);
                //ProjectData.ClearProjectError();
            }
        }

        private void DisplayBonusText()
        {
            var str1 = RTF.StartRTF();
            var num1 = mySet.Bonus.Length - 1;
            for (var index1 = 0; index1 <= num1; ++index1)
            {
                switch (index1)
                {
                    case 0:
                        mySet.Bonus[index1].Slotted = 2;
                        if (mySet.Bonus[index1].Index.Length > 0)
                            str1 = str1 + RTF.Color(RTF.ElementID.Black) +
                                   RTF.Bold(Convert.ToString(mySet.Bonus[index1].Slotted) + " Enhancements: ");
                        break;
                    case 1:
                        mySet.Bonus[index1].Slotted = 2;
                        if (mySet.Bonus[index1].Index.Length > 0)
                            str1 = str1 + RTF.Color(RTF.ElementID.Black) +
                                   RTF.Bold(Convert.ToString(mySet.Bonus[index1].Slotted) + " Enhancements: ");
                        break;
                    case 2:
                        mySet.Bonus[index1].Slotted = 3;
                        if (mySet.Bonus[index1].Index.Length > 0)
                            str1 = str1 + RTF.Color(RTF.ElementID.Black) +
                                   RTF.Bold(Convert.ToString(mySet.Bonus[index1].Slotted) + " Enhancements: ");
                        break;
                    case 3:
                        mySet.Bonus[index1].Slotted = 3;
                        if (mySet.Bonus[index1].Index.Length > 0)
                            str1 = str1 + RTF.Color(RTF.ElementID.Black) +
                                   RTF.Bold(Convert.ToString(mySet.Bonus[index1].Slotted) + " Enhancements: ");
                        break;
                    case 4:
                        mySet.Bonus[index1].Slotted = 4;
                        if (mySet.Bonus[index1].Index.Length > 0)
                            str1 = str1 + RTF.Color(RTF.ElementID.Black) +
                                   RTF.Bold(Convert.ToString(mySet.Bonus[index1].Slotted) + " Enhancements: ");
                        break;
                    case 5:
                        mySet.Bonus[index1].Slotted = 4;
                        if (mySet.Bonus[index1].Index.Length > 0)
                            str1 = str1 + RTF.Color(RTF.ElementID.Black) +
                                   RTF.Bold(Convert.ToString(mySet.Bonus[index1].Slotted) + " Enhancements: ");
                        break;
                    case 6:
                        mySet.Bonus[index1].Slotted = 5;
                        if (mySet.Bonus[index1].Index.Length > 0)
                            str1 = str1 + RTF.Color(RTF.ElementID.Black) +
                                   RTF.Bold(Convert.ToString(mySet.Bonus[index1].Slotted) + " Enhancements: ");
                        break;
                    case 7:
                        mySet.Bonus[index1].Slotted = 5;
                        if (mySet.Bonus[index1].Index.Length > 0)
                            str1 = str1 + RTF.Color(RTF.ElementID.Black) +
                                   RTF.Bold(Convert.ToString(mySet.Bonus[index1].Slotted) + " Enhancements: ");
                        break;
                    case 8:
                        mySet.Bonus[index1].Slotted = 6;
                        if (mySet.Bonus[index1].Index.Length > 0)
                            str1 = str1 + RTF.Color(RTF.ElementID.Black) +
                                   RTF.Bold(Convert.ToString(mySet.Bonus[index1].Slotted) + " Enhancements: ");
                        break;
                    case 9:
                        mySet.Bonus[index1].Slotted = 6;
                        if (mySet.Bonus[index1].Index.Length > 0)
                            str1 = str1 + RTF.Color(RTF.ElementID.Black) +
                                   RTF.Bold(Convert.ToString(mySet.Bonus[index1].Slotted) + " Enhancements: ");
                        break;
                }

                var num2 = mySet.Bonus[index1].Index.Length - 1;
                for (var index2 = 0; index2 <= num2; ++index2)
                {
                    if (mySet.Bonus[index1].Index[index2] <= -1)
                        continue;
                    if (index2 > 0)
                        str1 += ", ";
                    str1 = str1 + RTF.Color(RTF.ElementID.InventionInvert) +
                           DatabaseAPI.Database.Power[mySet.Bonus[index1].Index[index2]].PowerName;
                }

                if (mySet.Bonus[index1].Index.Length > 0)
                    str1 = str1 + RTF.Crlf() + "   " + RTF.Italic(mySet.GetEffectString(index1, false));
                if (mySet.Bonus[index1].PvMode == Enums.ePvX.PvP)
                    str1 += " (PvP)";
                if (mySet.Bonus[index1].Index.Length > 0)
                    str1 += RTF.Crlf();
            }

            var num3 = mySet.SpecialBonus.Length - 1;
            for (var index1 = 0; index1 <= num3; ++index1)
            {
                if (mySet.SpecialBonus[index1].Special > -1)
                {
                    var str2 = str1 + RTF.Color(RTF.ElementID.Black) + RTF.Bold("Special Case Enhancement: ") +
                               RTF.Color(RTF.ElementID.InventionInvert);
                    if (mySet.Enhancements[mySet.SpecialBonus[index1].Special] > -1)
                        str2 += DatabaseAPI.Database
                            .Enhancements[mySet.Enhancements[mySet.SpecialBonus[index1].Special]].Name;
                    var str3 = str2 + RTF.Crlf();
                    var num2 = mySet.SpecialBonus[index1].Index.Length - 1;
                    for (var index2 = 0; index2 <= num2; ++index2)
                    {
                        if (mySet.SpecialBonus[index1].Index[index2] <= -1)
                            continue;
                        if (index2 > 0)
                            str3 += ", ";
                        str3 = str3 + RTF.Color(RTF.ElementID.InventionInvert) +
                               DatabaseAPI.Database.Power[mySet.SpecialBonus[index1].Index[index2]].PowerName;
                    }

                    str1 = str3 + RTF.Crlf() + "   " + RTF.Italic(mySet.GetEffectString(index1, true)) + RTF.Crlf();
                }

                if (mySet.SpecialBonus[index1].Index.Length > 0)
                    str1 += RTF.Crlf();
            }

            rtbBonus.Rtf = str1 + RTF.EndRTF();
        }

        private void DisplayIcon()
        {
            if (!string.IsNullOrWhiteSpace(mySet.Image))
            {
                using var extendedBitmap1 = new ExtendedBitmap($"{I9Gfx.GetEnhancementsPath()}{mySet.Image}");
                using var extendedBitmap2 = new ExtendedBitmap(30, 30);
                extendedBitmap2.Graphics.DrawImage(I9Gfx.Borders.Bitmap, extendedBitmap2.ClipRect,
                    I9Gfx.GetOverlayRect(Origin.Grade.SetO), GraphicsUnit.Pixel);
                extendedBitmap2.Graphics.DrawImage(extendedBitmap1.Bitmap, extendedBitmap2.ClipRect,
                    extendedBitmap2.ClipRect, GraphicsUnit.Pixel);
                btnImage.Image = new Bitmap(extendedBitmap2.Bitmap);
                btnImage.Text = mySet.Image;
            }
            else
            {
                using var extendedBitmap = new ExtendedBitmap(30, 30);
                extendedBitmap.Graphics.DrawImage(I9Gfx.Borders.Bitmap, extendedBitmap.ClipRect,
                    I9Gfx.GetOverlayRect(Origin.Grade.SetO), GraphicsUnit.Pixel);
                btnImage.Image = new Bitmap(extendedBitmap.Bitmap);
                btnImage.Text = @"Select Image";
            }
        }

        private void DisplaySetData()
        {
            DisplaySetIcons();
            DisplayIcon();
            txtNameFull.Text = mySet.DisplayName;
            txtNameShort.Text = mySet.ShortName;
            txtDesc.Text = mySet.Desc;
            txtInternal.Text = mySet.Uid;
            SetMinLevel(mySet.LevelMin + 1);
            SetMaxLevel(mySet.LevelMax + 1);
            udMaxLevel.Minimum = udMinLevel.Value;
            udMinLevel.Maximum = udMaxLevel.Value;
            cbSetType.SelectedIndex = (int) mySet.SetType;
            btnImage.Text = mySet.Image;
            DisplayBonusText();
            DisplayBonus();
        }

        private void DisplaySetIcons()
        {
            FillImageList();
            var items = new string[2];
            lvEnh.BeginUpdate();
            lvEnh.Items.Clear();
            FillImageList();
            var num1 = mySet.Enhancements.Length - 1;
            for (var imageIndex = 0; imageIndex <= num1; ++imageIndex)
            {
                var enhancement = DatabaseAPI.Database.Enhancements[mySet.Enhancements[imageIndex]];
                items[0] = enhancement.Name + " (" + enhancement.ShortName + ")";
                items[1] = "";
                var num2 = enhancement.ClassID.Length - 1;
                for (var index1 = 0; index1 <= num2; ++index1)
                {
                    if (!string.IsNullOrWhiteSpace(items[1]))
                    {
                        var strArray1 = items;
                        var num3 = 1;
                        string[] strArray2;
                        IntPtr index2;
                        (strArray2 = strArray1)[(int) (index2 = (IntPtr) num3)] = strArray2[(int) index2] + ",";
                    }

                    var strArray3 = items;
                    var num4 = 1;
                    string[] strArray4;
                    IntPtr index3;
                    (strArray4 = strArray3)[(int) (index3 = (IntPtr) num4)] = strArray4[(int) index3] +
                                                                              DatabaseAPI.Database
                                                                                  .EnhancementClasses[
                                                                                      enhancement.ClassID[index1]]
                                                                                  .ShortName;
                }

                lvEnh.Items.Add(new ListViewItem(items, imageIndex));
            }

            lvEnh.EndUpdate();
        }

        private void FillBonusCombos()
        {
            cbSlotCount.BeginUpdate();
            cbSlotCount.Items.Clear();
            var num0 = mySet.Enhancements.Length;
            var num1 = num0 + num0 - 1;
            for (var index = 0; index <= num1; ++index)
                switch (index)
                {
                    case 2:
                        cbSlotCount.Items.Add(Convert.ToString(index) + " Enhancements");
                        break;
                    case 3:
                        cbSlotCount.Items.Add(Convert.ToString(index - 1) + " Enhancements (PVP Effect)");
                        break;
                    case 4:
                        cbSlotCount.Items.Add(Convert.ToString(index - 1) + " Enhancements");
                        break;
                    case 5:
                        cbSlotCount.Items.Add(Convert.ToString(index - 2) + " Enhancements (PVP Effect)");
                        break;
                    case 6:
                        cbSlotCount.Items.Add(Convert.ToString(index - 2) + " Enhancements");
                        break;
                    case 7:
                        cbSlotCount.Items.Add(Convert.ToString(index - 3) + " Enhancements (PVP Effect)");
                        break;
                    case 8:
                        cbSlotCount.Items.Add(Convert.ToString(index - 3) + " Enhancements");
                        break;
                    case 9:
                        cbSlotCount.Items.Add(Convert.ToString(index - 4) + " Enhancements (PVP Effect)");
                        break;
                    case 10:
                        cbSlotCount.Items.Add(Convert.ToString(index - 4) + " Enhancements");
                        break;
                    case 11:
                        cbSlotCount.Items.Add(Convert.ToString(index - 5) + " Enhancements (PVP Effect)");
                        break;
                }

            var num2 = mySet.Enhancements.Length - 1;
            for (var index = 0; index <= num2; ++index)
                cbSlotCount.Items.Add(DatabaseAPI.Database.Enhancements[mySet.Enhancements[index]].Name);
            if (cbSlotCount.Items.Count > 0)
                cbSlotCount.SelectedIndex = 0;
            cbSlotCount.EndUpdate();
        }

        private void FillBonusList()
        {
            lvBonusList.BeginUpdate();
            lvBonusList.Items.Clear();
            var items = new string[2];
            var num1 = SetBonusList.Length - 1;
            for (var index = 0; index <= num1; ++index)
            {
                items[1] = "";
                if (DatabaseAPI.Database.Power[SetBonusList[index]].Effects.Length > 0)
                    items[1] = DatabaseAPI.Database.Power[SetBonusList[index]].Effects[0]
                        .BuildEffectStringShort(false, true);
                items[0] = DatabaseAPI.Database.Power[SetBonusList[index]].PowerName;
                lvBonusList.Items.Add(new ListViewItem(items)
                {
                    Tag = SetBonusList[index]
                });
            }

            var num2 = SetBonusListPVP.Length - 1;
            for (var index = 0; index <= num2; ++index)
            {
                items[1] = "";
                if (DatabaseAPI.Database.Power[SetBonusListPVP[index]].Effects.Length > 0)
                    items[1] = DatabaseAPI.Database.Power[SetBonusListPVP[index]].Effects[0]
                        .BuildEffectStringShort(false, true);
                items[0] = DatabaseAPI.Database.Power[SetBonusListPVP[index]].PowerName + " (PVP Only)";
                lvBonusList.Items.Add(new ListViewItem(items)
                {
                    Tag = SetBonusListPVP[index]
                });
            }

            lvBonusList.Sort();
            lvBonusList.EndUpdate();
        }

        private void FillComboBoxes()
        {
            var names = Enum.GetNames(Enums.eSetType.Untyped.GetType());
            cbSetType.BeginUpdate();
            cbSetType.Items.Clear();
            cbSetType.Items.AddRange(names.ToArray<object>());
            cbSetType.EndUpdate();
        }

        private void FillImageList()
        {
            var imageSize1 = ilEnh.ImageSize;
            var width1 = imageSize1.Width;
            imageSize1 = ilEnh.ImageSize;
            var height1 = imageSize1.Height;
            using var extendedBitmap = new ExtendedBitmap(width1, height1);
            ilEnh.Images.Clear();
            var num = mySet.Enhancements.Length - 1;
            for (var index = 0; index <= num; ++index)
            {
                var enhancement = DatabaseAPI.Database.Enhancements[mySet.Enhancements[index]];
                if (enhancement.ImageIdx > -1)
                {
                    var gfxGrade = I9Gfx.ToGfxGrade(enhancement.TypeID);
                    extendedBitmap.Graphics.Clear(Color.White);
                    var graphics = extendedBitmap.Graphics;
                    I9Gfx.DrawEnhancement(ref graphics,
                        DatabaseAPI.Database.Enhancements[mySet.Enhancements[index]].ImageIdx, gfxGrade);
                    ilEnh.Images.Add(extendedBitmap.Bitmap);
                }
                else
                {
                    var images = ilEnh.Images;
                    var imageSize2 = ilEnh.ImageSize;
                    var width2 = imageSize2.Width;
                    imageSize2 = ilEnh.ImageSize;
                    var height2 = imageSize2.Height;
                    var bitmap = new Bitmap(width2, height2);
                    images.Add(bitmap);
                }
            }
        }

        private void frmSetEdit_Load(object sender, EventArgs e)
        {
            SetBonusList = DatabaseAPI.NidPowers("set_bonus.set_bonus");
            SetBonusListPVP = DatabaseAPI.NidPowers("set_bonus.pvp_set_bonus");
            if (mySet.Bonus.Length < 1 || mySet.Bonus.Length < 6)
                mySet.InitBonusPvP();
            FillComboBoxes();
            FillBonusCombos();
            FillBonusList();
            DisplaySetData();
            Loading = false;
            DisplayBonus();
        }

        private bool isBonus()
        {
            var num0 = mySet.Enhancements.Length;
            var num1 = num0 + num0 - 2;
            return (cbSlotCount.SelectedIndex > -1) & (cbSlotCount.SelectedIndex < num1);
        }

        private bool isSpecial()
        {
            var num0 = mySet.Enhancements.Length;
            var num1 = num0 + num0 - 1;
            var num2 = num1 - 1;
            var num3 = num1 + num0 - 1;
            return (cbSlotCount.SelectedIndex >= num2) & (cbSlotCount.SelectedIndex < num3);
        }

        private void TxtBonusFilter_TextChanged(object sender, EventArgs e)
        {
            FillBonusList();
        }
    }
}