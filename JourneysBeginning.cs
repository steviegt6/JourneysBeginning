using JourneysBeginning.Common;
using JourneysBeginning.Content.UI;
using JourneysBeginning.Localization;
using log4net;
using Microsoft.Xna.Framework;
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
        public static JourneysBeginning Instance { get; private set; }

        /// <summary>
        /// Logger used by <see cref="JourneysBeginning"/>.
        /// </summary>
        internal static ILog ModLogger { get; private set; }

        /// <summary>
        /// Identical to <see cref="Main.OurFavoriteColor"/> but not affected by the 1/100 easter egg.
        /// </summary>
        public static Color TerrariaGoldYellow = new Color(255, 231, 69);

        /// <summary>
        /// <see cref="showChangelogTextOptional"/>: The boolean responsible for collapsing and uncollapsing the changelog text. <br />
        /// <see cref="showChangelogTextVersionDifference"/>: The boolean responsible for actually showing the changelog at all, only true on the first load after updating.
        /// </summary>
        public bool showChangelogTextOptional, showChangelogTextVersionDifference = false;

        public override void Load()
        {
            Instance = this;
            ModLogger = Instance.Logger;

            // Initialize all the things!
            AssetManager.Load();
            LocalizationInitializer.Initialize();
            ILManager.Load();
            SaveDataManager.Load();
            SaveDataManager.Save();
            ChangelogData.PopulateChangelogList(this);
        }

        public override void PostSetupContent() => AssetManager.SwapAssets();

        public override void Unload()
        {
            // Unload any data that goes unhandled by tML or contains static data.
            AssetManager.Unload();
            ChangelogData.Unload();
            ILManager.Unload();
            SaveDataManager.Unload();
            LocalizationInitializer.Unload();

            ModLogger = null;
            Instance = null;
        }
    }
}
