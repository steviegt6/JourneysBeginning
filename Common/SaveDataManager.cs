using System;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader.Config;

namespace JourneysBeginning.Common
{
    /// <summary>
    /// Class responsible for managing <see cref="JourneysBeginning.SaveData"/>. Handles saving, loading, and unloading.
    /// </summary>
    public static class SaveDataManager
    {
        /// <summary>
        /// The <see cref="Preferences"/> instance used by <see cref="JourneysBeginning"/>. This manages a config separate from <see cref="ModConfig"/>.
        /// </summary>
        public static Preferences SaveData => new Preferences(Main.SavePath + Path.DirectorySeparatorChar + "JourneysBeginning" + Path.DirectorySeparatorChar + "savedata.json");

        internal static void Load()
        {
            SaveData.Load();

            if (SaveData.Contains("LastKnownVersion"))
                JourneysBeginning.Instance.showChangelogTextVersionDifference = new Version(SaveData.Get("LastKnownVersion", JourneysBeginning.Instance.Version.ToString())) < JourneysBeginning.Instance.Version;
            else
                JourneysBeginning.Instance.showChangelogTextVersionDifference = true;
        }

        internal static void Save()
        {
            SaveData.Clear();
            SaveData.Put("LastKnownVersion", JourneysBeginning.Instance.Version.ToString());
            SaveData.Save();
        }

        internal static void Unload() => Save();
    }
}