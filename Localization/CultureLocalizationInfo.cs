using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace JourneysBeginning.Localization
{
    /// <summary>
    /// Struct containing basic data needed for localization. <br />
    /// Currently only being used for ModConfig localization.
    /// </summary>
    public struct CultureLocalizationInfo
    {
        public readonly Dictionary<int, string> Translations;

        public CultureLocalizationInfo(string english, string german = "", string italian = "", string french = "", string spanish = "", string russian = "", string chinese = "", string portuguese = "", string polish = "")
        {
            Translations = new Dictionary<int, string>()
            {
                { GameCulture.English.LegacyId, english },
                { GameCulture.German.LegacyId, german },
                { GameCulture.Italian.LegacyId, italian },
                { GameCulture.French.LegacyId, french },
                { GameCulture.Spanish.LegacyId, spanish },
                { GameCulture.Russian.LegacyId, russian },
                { GameCulture.Chinese.LegacyId, chinese },
                { GameCulture.Portuguese.LegacyId, portuguese },
                { GameCulture.Polish.LegacyId, polish }
            };
        }

        public void PopulateModTranslation(ModTranslation translation, int item, Mod mod, string color = "ffffff", bool configTooltip = false)
        {
            if (!configTooltip)
            {
                string enText = $"[c/{color}:{Translations[GameCulture.English.LegacyId]}]";

                if (item != ItemID.None)
                    enText = $"[i:{item}] " + enText;

                foreach (int culture in Translations.Keys)
                    translation.AddTranslation(culture, string.IsNullOrEmpty(Translations[culture]) ? enText : (item != ItemID.None ? $"[i:{item}] " : "") + $"[c/{color}:{Translations[culture]}]");

                mod.AddTranslation(translation);
            }
            else
            {
                string enText = Translations[GameCulture.English.LegacyId];

                foreach (int culture in Translations.Keys)
                    translation.AddTranslation(culture, string.IsNullOrEmpty(Translations[culture]) ? enText : Translations[culture]);

                mod.AddTranslation(translation);
            }
        }
    }
}