using JourneysBeginning.Common.Bases;
using JourneysBeginning.Content.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace JourneysBeginning.Common.Detours
{
    public class UIModItemDrawSelfOn : Detour
    {
        private delegate void OrigDrawSelf(object self, SpriteBatch spriteBatch);

        private delegate void HookDrawSelf(OrigDrawSelf orig, object self, SpriteBatch spriteBatch);

        private event HookDrawSelf OnDrawSelf
        {
            add => HookEndpointManager.Add<HookDrawSelf>(typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.UI.UIModItem").GetMethod("DrawSelf", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), value);

            remove => HookEndpointManager.Remove<HookDrawSelf>(typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.UI.UIModItem").GetMethod("DrawSelf", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), value);
        }

        public override string DictKey => "Terraria.ModLoader.UI.UIModItem.DrawSelf";

        public override void Load() => OnDrawSelf += ChangelogText;

        public override void Unload() => OnDrawSelf -= ChangelogText;

        private void ChangelogText(OrigDrawSelf orig, object self, SpriteBatch spriteBatch)
        {
            if (ChangelogData.ChangelogButton?.IsMouseHovering == true)
            {
                Rectangle bounds = ((CalculatedStyle)typeof(UIPanel).GetMethod("GetOuterDimensions", BindingFlags.Instance | BindingFlags.Public).Invoke(self, new object[0] { })).ToRectangle();
                bounds.Height += 16;
                string text = "Changelogs";
                float textWidth = Main.fontMouseText.MeasureString(text).X;
                Vector2 pos = Main.MouseScreen + new Vector2(16f);
                pos.X = Math.Min(pos.X, (float)bounds.Right - textWidth - 16f);
                pos.Y = Math.Min(pos.Y, bounds.Bottom - 30);
                Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, text, pos.X, pos.Y, Color.Goldenrod, Color.Black, Vector2.Zero);
            }

            orig(self, spriteBatch);
        }
    }
}