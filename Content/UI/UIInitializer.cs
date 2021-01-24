using Terraria.ModLoader;
using JourneysBeginning.Content.UI.States;

namespace JourneysBeginning.Content.UI {
    public class UIInitializer : ILoadable {
        public static UIInitializer Instance;

        public UIEngineerCharges engineerUIInstance;
        void ILoadable.Load(Mod mod) {
            Instance = new UIInitializer();
            engineerUIInstance = new UIEngineerCharges();
        }
        void ILoadable.Unload() {
            Instance = null;
        }
    }
}
