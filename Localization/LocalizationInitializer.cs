using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria.ID;
using Terraria.ModLoader;

namespace JourneysBeginning.Localization
{
    /// <summary>
    /// Class made in order to initialize localization used by ModConfigs and other tML classes that can be localized but are loaded before localization.
    /// </summary>
    public static class LocalizationInitializer
    {
        public static ReadOnlyCollection<CultureLocalizationInfo> LocalizationInfo => _localizationInfo.AsReadOnly();

        private static List<CultureLocalizationInfo> _localizationInfo;

        internal static void Initialize()
        {
            _localizationInfo = new List<CultureLocalizationInfo>();

            #region Headers

            AddTranslation("Config.Headers.General", new CultureLocalizationInfo(english: "General Changes"), ItemID.Cog);

            #endregion Headers

            #region Labels

            AddTranslation("Config.Labels.AnglerShop", new CultureLocalizationInfo(english: "Angler Shop"));

            #endregion Labels

            #region Tooltips

            AddTranslation("Config.Tooltips.AnglerShop", new CultureLocalizationInfo(english: "Whether or not the Angler should be given a shop, where he sells items depending on your progression in the game and how many quests you've completed."), configTranslation: true);

            #endregion Tooltips
        }

        public static void AddTranslation(Mod mod, CultureLocalizationInfo localInfo, string name, int item = ItemID.None, string color = "ffffff", bool configTranslation = false)
        {
            localInfo.PopulateModTranslation(mod.CreateTranslation(name), item, mod, color, configTranslation);
            _localizationInfo.Add(localInfo);
        }

        private static void AddTranslation(string name, CultureLocalizationInfo localInfo, int item = ItemID.None, string color = "ffffff", bool configTranslation = false) => AddTranslation(JourneysBeginning.Instance, localInfo, name, item, color, configTranslation);
    }
}