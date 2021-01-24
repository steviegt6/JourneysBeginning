using JourneysBeginning.Common;
using JourneysBeginning.Content.UI;
using JourneysBeginning.Localization;
using log4net;
using System;
using System.IO;
using Terraria;
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

        /// <summary>
        /// The instance of <see cref="JourneysBeginning"/> used by tML, fetched with <see cref="ModContent.GetInstance{T}"/>.
        /// </summary>
        public static JourneysBeginning Instance => ModContent.GetInstance<JourneysBeginning>();

        /// <summary>
        /// Logger used by <see cref="JourneysBeginning"/>.
        /// </summary>
        internal static ILog ModLogger => Instance.Logger;

        /// <summary>
        /// <see cref="showChangelogTextOptional"/>: The boolean responsible for collapsing and uncollapsing the changelog text. <br />
        /// <see cref="showChangelogTextVersionDifference"/>: The boolean responsible for actually showing the changelog at all, only true on the first load after updating.
        /// </summary>
        public bool showChangelogTextOptional, showChangelogTextVersionDifference = false;

        private string _origVersionNumber;

        public override void Load()
        {
            // Initialize all the things!
            LocalizationInitializer.Initialize();
            ILManager.Load();
            SaveDataManager.Load();
            SaveDataManager.Save();
            ChangelogData.PopulateChangelogList(this);

            ChangeVersionText();
        }

        public override void Unload()
        {
            Main.versionNumber = _origVersionNumber;

            // Unload any data that goes unhandled by tML or contains static data.
            ChangelogData.Unload();
            ILManager.Unload();
            SaveDataManager.Unload();
        }

        private void ChangeVersionText()
        {
            string defaultText = "v1.4.1.2";

            _origVersionNumber = Main.versionNumber;

            // Insert "Terraria " (with a space) before "v1.3.5.3" if it hasn't already.
            // This is compatible with any other added text by any other mod.
            if (Main.versionNumber.Contains(defaultText) && !Main.versionNumber.Contains($"Terraria {defaultText}"))
                Main.versionNumber = Main.versionNumber.Insert(Main.versionNumber.IndexOf(defaultText), "Terraria ");
        }
    }
}