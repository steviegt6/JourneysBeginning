using JourneysBeginning.Common;
using JourneysBeginning.Common.Utilities;
using log4net;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace JourneysBeginning
{
    /// <summary>
    /// Main <c>Mod</c> class. <br />
    /// Handles some core some, relatively uninteresting.
    /// </summary>
    public class JourneysBeginning : Mod
    {
        public static JourneysBeginning Instance { get; private set; }

        public static ILog ModLogger => Instance.Logger;

        public Assembly TMLAssembly { get; set; }

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

            ILManager.Load();
        }

        public override void Unload()
        {
            Main.versionNumber = _origVersionNumber;

            ILManager.Unload();

            UnloadStaticFields();
        }

        public void UnloadStaticFields()
        {
            Logger.Verbose("Unloading static fields...");

            Instance = null;
            TMLAssembly = null;

            Logger.Verbose("Unloaded static fields!");
        }
    }
}