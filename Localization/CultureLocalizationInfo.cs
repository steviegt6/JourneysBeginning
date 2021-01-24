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
        public readonly Dictionary<GameCulture.CultureName, string> Translations;

        public CultureLocalizationInfo(string english, string german = "", string italian = "", string french = "", string spanish = "", string russian = "", string chinese = "", string portuguese = "", string polish = "")
        {
            Translations = new Dictionary<GameCulture.CultureName, string>()
            {
                { GameCulture.CultureName.English, english },
                { GameCulture.CultureName.German, german },
                { GameCulture.CultureName.Italian, italian },
                { GameCulture.CultureName.French, french },
                { GameCulture.CultureName.Spanish, spanish },
                { GameCulture.CultureName.Russian, russian },
                { GameCulture.CultureName.Chinese, chinese },
                { GameCulture.CultureName.Portuguese, portuguese },
                { GameCulture.CultureName.Polish, polish }
            };
        }

        public void PopulateModTranslation(ModTranslation translation, int item, Mod mod, string color = "ffffff", bool configTooltip = false)
        {
            string enText = configTooltip ? $"[c/{color}:{Translations[GameCulture.CultureName.English]}]" : Translations[GameCulture.CultureName.English];

            if (item != ItemID.None && !configTooltip)
                enText = $"[i:{item}] " + enText;

            foreach (GameCulture.CultureName culture in Translations.Keys)
                translation.AddTranslation(GameCulture.FromCultureName(culture), string.IsNullOrEmpty(Translations[culture]) ? enText : (item != ItemID.None && !configTooltip ? $"[i:{item}] " : "") + (!configTooltip ? $"[c/{color}:{Translations[culture]}]" : ""));

            mod.AddTranslation(translation);
        }
    }
}