using log4net;
using log4net.Core;
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

        [Header("Misc.")]
        [Label("Verbose Logging")]
        [Tooltip("Extra logging, most likely won't come in handy much." +
            "\nIs enabled by default, enable if asked to by someone assisting you with debugging errors!")]
        [DefaultValue(true)]
        public bool verboseLogging;

        public bool VerboseLogging
        {
            get => verboseLogging;
            set => JourneysBeginning.ModLogger.Logger.Log(LogManager.GetLogger(JourneysBeginning.Instance.Name).GetType(), Level.Verbose, "Verbose logging was: " + (value ? "Enabled" : "Disabled"), null);
        }
    }
}