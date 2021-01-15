using JourneysBeginning.Common.Bases;
using JourneysBeginning.Common.Utilities;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.UI.Chat;

namespace JourneysBeginning.Common.ILEdits
{
    internal sealed class MainDrawMenuIL : ILEdit
    {
        public override string DictKey => "Terraria.Main.DrawMenu";

        public override void Load() => IL.Terraria.Main.DrawMenu += ChangeDrawMethodIL;

        public override void Unload() => IL.Terraria.Main.DrawMenu -= ChangeDrawMethodIL;

        private void ChangeDrawMethodIL(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            #region Change Copyright Text draw method

            if (!c.TryGotoNext(x => x.MatchLdstr("Copyright © 2017 Re-Logic")))
            {
                ILHelper.LogILError("ldstr", "Copyright © 2017 Re-Logic");
                return;
            }

            if (!c.TryGotoNext(x => x.MatchLdloc(179)))
            {
                ILHelper.LogILError("ldloc.s", "179");
                return;
            }

            for (int i = 0; i < 2; i++)
                if (!c.TryGotoNext(x => x.MatchLdsfld(typeof(Main).GetField("spriteBatch", BindingFlags.Static | BindingFlags.Public))))
                {
                    ILHelper.LogILError("ldsfld", "Terraria.Main::spriteBatch");
                    return;
                }

            c.Index++;

            c.RemoveRange(31);

            c.Emit(OpCodes.Ldloc_2); // color
            c.Emit(OpCodes.Ldloc, 179); // text to draw
            c.EmitDelegate<Action<Color, string>>((color, text) =>
            {
                Color drawColor = color;
                drawColor.R = (byte)((255 + drawColor.R) / 2);
                drawColor.G = (byte)((255 + drawColor.R) / 2);
                drawColor.B = (byte)((255 + drawColor.R) / 2);
                drawColor.A = (byte)(drawColor.A * 0.3f);

                Utils.DrawBorderString(Main.spriteBatch, text, new Vector2(Main.screenWidth - Main.fontMouseText.MeasureString(text).X - 10f, Main.screenHeight - 2f - Main.fontMouseText.MeasureString(text).Y), drawColor);
            });

            #endregion Change Copyright Text draw method

            #region Change Version Text draw method & add Changelog drawing

            if (!c.TryGotoNext(x => x.MatchStloc(193)))
            {
                ILHelper.LogILError("stloc.s", "193");
                return;
            }

            if (!c.TryGotoNext(x => x.MatchLdsfld(typeof(Main).GetField("spriteBatch", BindingFlags.Static | BindingFlags.Public))))
            {
                ILHelper.LogILError("ldsfld", "Terraria.Main::spriteBatch");
                return;
            }

            c.Index++;

            c.RemoveRange(28); // Remove the entire method

            c.Emit(OpCodes.Ldloc, 186); // num107 (for index)
            c.Emit(OpCodes.Ldloc, 187); // text color
            c.Emit(OpCodes.Ldloc, 188); // x offset
            c.Emit(OpCodes.Ldloc, 193); // text to draw
            c.EmitDelegate<Action<int, Color, int, string>>((index, drawColor, xOffset, text) =>
            {
                if (index == 4)
                {
                    DrawVersionText(drawColor, xOffset, text);
                    DrawChangelog();
                }
            });

            #endregion Change Version Text draw method & add Changelog drawing

            ILHelper.LogILCompletion("DrawMenu");
        }

        private void DrawVersionText(Color drawColor, int xOffset, string text)
        {
            string[] lines = text.Split('\n').Reverse().ToArray();
            float extraYOffset = Main.fontMouseText.MeasureString(text).Y / lines.Length;

            for (int i = 0; i < lines.Length; i++)
                Utils.DrawBorderString(Main.spriteBatch, lines[i], new Vector2(xOffset + 10f, Main.screenHeight - 2f - (extraYOffset * (i + 1))), drawColor);

            Utils.DrawBorderString(Main.spriteBatch, $"Journey's Beginning {JourneysBeginning.Instance.Version}", new Vector2(xOffset + 10f, Main.screenHeight - extraYOffset - 2f - Main.fontMouseText.MeasureString(text).Y), Color.Goldenrod);
        }

        private void DrawChangelog()
        {
            if (JourneysBeginning.Instance.showChangelogTextVersionDifference)
            {
                // TODO: Move these to localization files & make it so the changelog is read from an online website or smth
                string versionText = JourneysBeginning.Instance.showChangelogTextOptional ? "Collapse JB Changelog" : "Open JB Changelog";
                Vector2 versionTextPos = new Vector2(10f + Main.fontMouseText.MeasureString(versionText).X, 38f);
                string changelogText = $"JB v{JourneysBeginning.Instance.Version} Changelog:" +
                    "\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris condimentum scelerisque rutrum. Morbi sagittis odio libero, ut volutpat augue vulputate aliquam." +
                    "\n* Curabitur dapibus imperdiet erat sed vestibulum." +
                    "\n* Sed elementum massa ac ipsum dignissim efficitur." +
                    "\n* In consequat ante pretium dolor convallis aliquet." +
                    "\n* Nam commodo consectetur mollis." +
                    "\n* Duis rhoncus bibendum condimentum." +
                    "\n* Nulla turpis velit, scelerisque eget sem eu, cursus consectetur odio." +
                    "\nClass aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Aliquam pellentesque purus ac posuere pellentesque.";

                // Draw middle version text
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, ChatManager.ParseMessage(versionText, Main.OurFavoriteColor).ToArray(), versionTextPos, 0f, Main.fontMouseText.MeasureString(versionText), Vector2.One, out _, maxWidth: 500f);

                if (JourneysBeginning.Instance.showChangelogTextOptional)
                    // Draw changelog text
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, ChatManager.ParseMessage(changelogText, Color.White).ToArray(), new Vector2(10f + Main.fontMouseText.MeasureString(changelogText).X, 38f + Main.fontMouseText.MeasureString(changelogText).Y), 0f, Main.fontMouseText.MeasureString(changelogText), Vector2.One, out _, maxWidth: 500f);

                // Code that collapses and exapands the changelog
                if (Main.mouseLeft && Main.mouseLeftRelease && new Rectangle(Main.mouseX, Main.mouseY, 1, 1).Intersects(new Rectangle((int)versionTextPos.X - (int)Main.fontMouseText.MeasureString(versionText).X, (int)versionTextPos.Y - (int)Main.fontMouseText.MeasureString(versionText).Y, (int)Main.fontMouseText.MeasureString(versionText).X, (int)Main.fontMouseText.MeasureString(versionText).Y)))
                    JourneysBeginning.Instance.showChangelogTextOptional = !JourneysBeginning.Instance.showChangelogTextOptional;
            }
        }
    }
}