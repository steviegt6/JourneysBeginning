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
        public string Text { get; private set; }

        public readonly List<Tuple<string, float>> drawTexts = new List<Tuple<string, float>>();

        private UIScrollbar _scrollbar;
        private float _height;
        private bool _heightNeedsRecalculating;

        public UIMessageBox(string text)
        {
            SetText(text);
        }

        public override void OnActivate()
        {
            base.OnActivate();

            _heightNeedsRecalculating = true;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            float pos = 0f;
            CalculatedStyle innerDims = GetInnerDimensions();

            if (_scrollbar != null)
                pos = -_scrollbar.GetValue();

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

            if (!_heightNeedsRecalculating)
                return;

            CalculatedStyle innerDims = GetInnerDimensions();

            if (innerDims.Width <= 0 || innerDims.Height <= 0)
                return;

            DynamicSpriteFont font = Main.fontMouseText;
            float pos = 0f;
            float textHeight = font.MeasureString("A").Y;

            drawTexts.Clear();

            foreach (string line in Text.Split('\n'))
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

            _height = pos;
            _heightNeedsRecalculating = false;
        }

        public override void Recalculate()
        {
            base.Recalculate();

            UpdateScrollbar();
        }

        public override void ScrollWheel(UIScrollWheelEvent evt)
        {
            base.ScrollWheel(evt);

            if (_scrollbar != null)
                _scrollbar.ViewPosition -= evt.ScrollWheelValue;
        }

        public void SetText(string text)
        {
            Text = text;
            ResetScrollbar();
        }

        public void ResetScrollbar()
        {
            if (_scrollbar != null)
            {
                _scrollbar.ViewPosition = 0;
                _heightNeedsRecalculating = true;
            }
        }

        public void SetScrollbar(UIScrollbar scrollbar)
        {
            _scrollbar = scrollbar;
            UpdateScrollbar();
            _heightNeedsRecalculating = true;
        }

        private void UpdateScrollbar() => _scrollbar?.SetView(GetInnerDimensions().Height, _height);
    }
}