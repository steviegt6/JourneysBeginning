using JourneysBeginning.Common;
using log4net;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;

namespace JourneysBeginning
{
    /// <summary>
    /// Main <c>Mod</c> class. <br />
    /// Handles some core stuff, relatively uninteresting.
    /// </summary>
    public class JourneysBeginning : Mod
    {
        #region ModHelpers fields

        // Fields used for "cross-compat" with ModHelpers, handled by the mod itself
        public static string GithubUserName => "Steviegt6";

        public static string GithubProjectName => "JourneysBeginning";

        #endregion ModHelpers fields

        public static JourneysBeginning Instance => ModContent.GetInstance<JourneysBeginning>();

        public static ILog ModLogger => Instance.Logger;

        public bool showChangelogTextOptional, showChangelogTextVersionDifference = false;
        public Preferences SaveData = new Preferences(Main.SavePath + Path.DirectorySeparatorChar + "JourneysBeginning" + Path.DirectorySeparatorChar + "savedata.txt");

        private string _origVersionNumber;

        public override void Load()
        {
            ILManager.Load();
            SaveDataManager.Load();
            SaveDataManager.Save();

            ChangeVersionText();
        }

        public override void Unload()
        {
            Main.versionNumber = _origVersionNumber;

            ILManager.Unload();
            SaveDataManager.Unload();
        }

        private void ChangeVersionText()
        {
            string defaultText = "v1.3.5.3";

            _origVersionNumber = Main.versionNumber;

            // Insert "Terraria " (with a space) before "v1.3.5.3" if it hasn't already.
            // This is compatible with any other added text by any other mod.
            if (Main.versionNumber.Contains(defaultText) && !Main.versionNumber.Contains($"Terraria {defaultText}"))
                Main.versionNumber = Main.versionNumber.Insert(Main.versionNumber.IndexOf(defaultText), "Terraria ");
        }
    }
}