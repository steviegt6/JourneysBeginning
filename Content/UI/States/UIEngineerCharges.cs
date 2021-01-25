using Terraria;
using Microsoft.Xna.Framework;

namespace JourneysBeginning.Content.UI.States
{
    public class UIEngineerCharges : BaseUI
    {
        protected override void Initializer()
        {
            Name = "EngineerUI";
            Visible = true;
            layerToSortAfter = "Vanilla: Resource Bars";
        }

        public override void Update(GameTime gameTime) => Main.NewText("i'm working as intended", Main.DiscoColor);
    }
}