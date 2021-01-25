using Terraria.ModLoader;
using JourneysBeginning.Content.UI.States;
using System.Collections.Generic;

namespace JourneysBeginning.Content.UI
{
    public class UIInitializer : ILoadable
    {
        public static UIInitializer Instance;

        public UIEngineerCharges engineerUIInstance;

        void ILoadable.Load(Mod mod)
        {
            BaseUI.UIs = new List<BaseUI>();
            Instance = new UIInitializer();
            engineerUIInstance = new UIEngineerCharges();
        }

        void ILoadable.Unload()
        {
            BaseUI.UIs = null;
            Instance = null;
        }
    }
}
