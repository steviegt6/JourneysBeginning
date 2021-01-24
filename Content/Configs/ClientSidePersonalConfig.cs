using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace JourneysBeginning.Content.Configs
{
    [Label("Personal JB Config")]
    public class ClientSidePersonalConfig : ModConfig
    {
        public static ClientSidePersonalConfig Instance;

        public override ConfigScope Mode => ConfigScope.ClientSide;

        public override void OnLoaded() => Instance = this;

        [Header("$Mods.JourneysBeginning.Config.Headers.General")]
        [Label("$Mods.JourneysBeginning.Config.Labels.AnglerShop")]
        [Tooltip("$Mods.JourneysBeginning.Config.Tooltips.AnglerShop")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool anglerShop;
    }
}