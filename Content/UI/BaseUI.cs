using Terraria.UI;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System;

namespace JourneysBeginning.Content.UI {
    public abstract class BaseUI : UIState {
        /// <summary>
        /// Contains all things inheriting from <see cref="BaseUI"/>.
        /// </summary>
        public static List<BaseUI> UIs;

        internal bool visible;
        public string layerToSortAfter;

        private string _name;
        private UserInterface _userInterface;

        public BaseUI() {
            _userInterface = new UserInterface();
            _userInterface.SetState(this);
            UIs.Add(this);
        }
        public void UpdateUI(GameTime g) {
            if (visible)
                _userInterface?.Update(g);
        }
        public void SortAfter(List<GameInterfaceLayer> layers) {
            int index = layers.FindIndex(l => l.Name.Contains(layerToSortAfter));

            if (index != -1) {
                layers.Insert(index, new LegacyGameInterfaceLayer(JourneysBeginning.Instance.Name + ": " + _name,
                    delegate {
                        if (visible) 
                            _userInterface.Draw(Terraria.Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
        public static void PerformOperationOnAllUI(Action<BaseUI> action) {
            foreach (BaseUI b in UIs)
                action(b);
        }
    }
}
