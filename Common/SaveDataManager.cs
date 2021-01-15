using System;
using Terraria.IO;

namespace JourneysBeginning.Common
{
    /// <summary>
    /// Class responsible for managing <see cref="JourneysBeginning.SaveData"/>. Handles saving, loading, and unloading.
    /// </summary>
    public static class SaveDataManager
    {
        public static Preferences SaveData => JourneysBeginning.Instance.SaveData;

        public static void Load()
        {
            SaveData.Load();

            if (SaveData.Contains("LastKnownVersion"))
                JourneysBeginning.Instance.showChangelogTextVersionDifference = new Version(SaveData.Get("LastKnownVersion", JourneysBeginning.Instance.Version.ToString())) < JourneysBeginning.Instance.Version;
            else
                JourneysBeginning.Instance.showChangelogTextVersionDifference = true;
        }

        public static void Save()
        {
            SaveData.Clear();
            SaveData.Put("LastKnownVersion", JourneysBeginning.Instance.Version.ToString());
            SaveData.Save();
        }

        public static void Unload() => Save();
    }
}