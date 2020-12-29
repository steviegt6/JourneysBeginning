using JourneysBeginning.Common;
using JourneysBeginning.Common.ILEdits;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace JourneysBeginning
{
    public class JourneysBeginning : Mod
    {
        public static JourneysBeginning Instance { get; private set; }

        public static Assembly TMLAssembly { get; private set; }

        private string _origVersionNumber;

        public JourneysBeginning()
        {
            Instance = this;
            TMLAssembly = typeof(ModLoader).Assembly;
        }

        public override void Load()
        {
            _origVersionNumber = Main.versionNumber;
            Main.versionNumber += $"\nJourney's Beginning v{Version}";

            ILLoader.LoadIL();
        }

        public override void Unload()
        {
            Main.versionNumber = _origVersionNumber;

            ILLoader.UnloadIL();

            UnloadStaticFields();
        }

        public void UnloadStaticFields()
        {
            JBLogger.Log("Unloading static fields...");

            Instance = null;
            TMLAssembly = null;

            Logger.Info("Unloaded static fields!");
        }
    }
}