using JourneysBeginning.Common.Bases;
using JourneysBeginning.Content.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace JourneysBeginning.Common.Detours
{
    public class UIModItemDrawOn : Detour
    {
        private delegate void OrigDraw(object self, SpriteBatch spriteBatch);

        private delegate void HookDraw(OrigDraw orig, object self, SpriteBatch spriteBatch);

        private event HookDraw OnDraw
        {
            add => HookEndpointManager.Add<HookDraw>(typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.UI.UIModItem").GetMethod("Draw", BindingFlags.Public | BindingFlags.Public | BindingFlags.Instance), value);

            remove => HookEndpointManager.Remove<HookDraw>(typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.UI.UIModItem").GetMethod("Draw", BindingFlags.Public | BindingFlags.Public | BindingFlags.Instance), value);
        }

        public override string DictKey => "Terraria.ModLoader.UI.UIModItem.Draw";

        public override void Load() => OnDraw += ChangelogText;

        public override void Unload() => OnDraw -= ChangelogText;

        private void ChangelogText(OrigDraw orig, object self, SpriteBatch spriteBatch)
        {
            orig(self, spriteBatch);

            object localModInstance = typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.UI.UIModItem").GetField("_mod", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
            string localModName = typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.Core.LocalMod").GetProperty("Name", BindingFlags.Instance | BindingFlags.Public).GetValue(localModInstance) as string;

            if (ChangelogData.ChangelogButton?.IsMouseHovering == true && localModName == JourneysBeginning.Instance.Name)
            {
                Rectangle bounds = ((CalculatedStyle)typeof(UIPanel).GetMethod("GetOuterDimensions", BindingFlags.Instance | BindingFlags.Public).Invoke(self, new object[0] { })).ToRectangle();
                bounds.Height += 16;
                string text = "Changelogs";
                float textWidth = FontAssets.MouseText.Value.MeasureString(text).X;
                Vector2 pos = Main.MouseScreen + new Vector2(16f);
                pos.X = Math.Min(pos.X, bounds.Right - textWidth - 16f);
                pos.Y = Math.Min(pos.Y, bounds.Bottom - 30);
                Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, text, pos.X, pos.Y, Color.Goldenrod, Color.Black, Vector2.Zero);
            }
        }
    }
}