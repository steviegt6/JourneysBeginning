using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace JourneysBeginning.Content.UI.Panels
{
    /// <summary>
    /// UI panel that displays a message box. <br />
    /// Code taken from tML.
    /// </summary>
    public class UIMessageBox : UIPanel
    {
        public UIScrollbar scrollbar;
        public string text;
        public float height;
        public bool heightNeedsRecalculating;
        public readonly List<Tuple<string, float>> drawTexts = new List<Tuple<string, float>>();

        public UIMessageBox(string text) => SetText(text);

        public override void OnActivate()
        {
            base.OnActivate();
            heightNeedsRecalculating = true;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            CalculatedStyle innerDims = GetInnerDimensions();
            float pos = 0f;

            if (scrollbar != null)
                pos = -scrollbar.GetValue();

            foreach (var drawText in drawTexts)
            {
                if (pos + drawText.Item2 > innerDims.Height)
                    break;

                if (pos >= 0)
                    Utils.DrawBorderString(spriteBatch, drawText.Item1, new Vector2(innerDims.X, innerDims.Y + pos), Color.White, 1f);

                pos += drawText.Item2;
            }
            Recalculate();
        }

        public override void RecalculateChildren()
        {
            base.RecalculateChildren();

            if (!heightNeedsRecalculating)
                return;

            CalculatedStyle innerDims = GetInnerDimensions();

            if (innerDims.Width <= 0 || innerDims.Height <= 0)
                return;

            DynamicSpriteFont font = Main.fontMouseText;
            drawTexts.Clear();
            float pos = 0f;
            float textHeight = font.MeasureString("A").Y;

            foreach (string line in text.Split('\n'))
            {
                string drawString = line;

                while (drawString.Length > 0)
                {
                    string remainder = "";

                    while (font.MeasureString(drawString).X > innerDims.Width)
                    {
                        remainder = drawString[drawString.Length - 1] + remainder;
                        drawString = drawString.Substring(0, drawString.Length - 1);
                    }

                    if (remainder.Length > 0)
                    {
                        int index = drawString.LastIndexOf(' ');

                        if (index >= 0)
                        {
                            remainder = drawString.Substring(index + 1) + remainder;
                            drawString = drawString.Substring(0, index);
                        }
                    }

                    drawTexts.Add(new Tuple<string, float>(drawString, textHeight));
                    pos += textHeight;
                    drawString = remainder;
                }
            }

            height = pos;
            heightNeedsRecalculating = false;
        }

        public override void Recalculate()
        {
            base.Recalculate();
            UpdateScrollbar();
        }

        public override void ScrollWheel(UIScrollWheelEvent evt)
        {
            base.ScrollWheel(evt);

            if (scrollbar != null)
                scrollbar.ViewPosition -= evt.ScrollWheelValue;
        }

        public void SetText(string text)
        {
            this.text = text;
            ResetScrollbar();
        }

        public void ResetScrollbar()
        {
            if (scrollbar != null)
            {
                scrollbar.ViewPosition = 0;
                heightNeedsRecalculating = true;
            }
        }

        public void SetScrollbar(UIScrollbar scrollbar)
        {
            this.scrollbar = scrollbar;
            UpdateScrollbar();
            heightNeedsRecalculating = true;
        }

        private void UpdateScrollbar() => scrollbar?.SetView(GetInnerDimensions().Height, height);
    }
}