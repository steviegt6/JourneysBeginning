using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using JourneysBeginning.Content.UI;
using System.Collections.Generic;
using Terraria.UI;

namespace JourneysBeginning.Common.ModSystems
{
    public class GeneralModSystem : ModSystem
    {
        public override void UpdateUI(GameTime gameTime)
        {
            BaseUI.PerformOperationOnAllUI(u => u.UpdateUI(gameTime));
            base.UpdateUI(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            BaseUI.PerformOperationOnAllUI(u => u.SortAfter(layers));
            base.ModifyInterfaceLayers(layers);
        }
    }
}
