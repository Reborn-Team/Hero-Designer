using System;
using System.Collections.Generic;
using Base.Data_Classes;

namespace Base.Master_Classes
{
    public static class MidsContext
    {
        public const string AppName = "Mids' Reborn";
        private const int AppMajorVersion = 2;
        private const int AppMinorVersion = 7;
        private const int AppBuildVersion = 3;
        private const int AppRevisionVersion = 51;
        public const string AppAssemblyVersion = "2.7.3.51";
        public const string AppVersionStatus = "(Alpha)";

        public const string Title = "Mids' Reborn : Hero Designer";
        public const string AssemblyName = "Hero Designer.exe";
        public static int MathLevelBase = 49;
        public static int MathLevelExemp = -1;

        public static readonly Version AppVersion =
            new Version(AppMajorVersion, AppMinorVersion, AppBuildVersion, AppRevisionVersion);

        public static Archetype Archetype;
        public static Character Character;

        public static ConfigData Config => ConfigData.Current;

        public static ConfigDataSpecial ConfigSp => ConfigDataSpecial.Current;

        public static void AssertVersioning()
        {
            if (AppAssemblyVersion != $"{AppMajorVersion}.{AppMinorVersion}.{AppBuildVersion}.{AppRevisionVersion}")
                throw new InvalidOperationException("Program assembly version is not internally consistent");
            if (AppVersion.CompareTo(new Version(AppAssemblyVersion)) != 0)
                throw new InvalidOperationException(
                    "Program app version is not internally consistent, failing startup");
        }

        public static string GetCryptedValue(string type, string name)
        {
            switch (type)
            {
                case "Auth":
                    ConfigSp.Auth.TryGetValue(name, out var authValue);
                    return authValue?.ToString();
                case "User":
                    ConfigSp.User.TryGetValue(name, out var userValue);
                    return userValue?.ToString();
                case "BotUser":
                    ConfigSp.BotUser.TryGetValue(name, out var botValue);
                    return botValue?.ToString();
                default:
                    return null;
            }
        }
    }
}