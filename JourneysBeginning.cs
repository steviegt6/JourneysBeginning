using JourneysBeginning.Common;
using JourneysBeginning.Common.IL;
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

            ILLoader.LoadIL();
        }

        public override void Load()
        {
            JBLogger.Load();

            _origVersionNumber = Main.versionNumber;
            Main.versionNumber += $"\nJourney's Beginning v{Version}";
        }

        public override void Unload()
        {
            Main.versionNumber = _origVersionNumber;

            ILLoader.UnloadIL();

            UnloadStaticFields();

            JBLogger.Unload();
        }

        public void UnloadStaticFields()
        {
            JBLogger.Log("Unloaded static fields...");

            Instance = null;
            TMLAssembly = null;

            JBLogger.Log("Unloaded static fields!");
        }
    }
}