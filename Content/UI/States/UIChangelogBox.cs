using JourneysBeginning.Content.UI.Panels;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace JourneysBeginning.Content.UI.States
{
    /// <summary>
    /// Adapted from tML UI code, used for displaying <see cref="ChangelogData"/>. <br />
    /// Allows the user to cycle between different changelogs.
    /// </summary>
    public class UIChangelogBox : UIState
    {
        public int Index { get; set; }

        public ChangelogData Changelog { get; set; }

        private UIElement _area;
        private UIMessageBox _messageBox;
        private UITextPanel<string> _exitButton;
        private UITextPanel<string> _previousButton;
        private UITextPanel<string> _nextButton;

        public UIChangelogBox(ChangelogData changelog, int index)
        {
            Changelog = changelog;
            Index = index;
        }

        public override void OnInitialize()
        {
            _area = new UIElement
            {
                Width = { Percent = 0.8f },
                Top = { Pixels = 200 },
                Height = { Pixels = -210, Percent = 1f },
                HAlign = 0.5f
            };

            UIPanel backPanel = new UIPanel
            {
                Width = { Percent = 1f },
                Height = { Pixels = -110, Percent = 1f },
                BackgroundColor = UICommon.MainPanelBackground
            };
            _area.Append(backPanel);

            _messageBox = new UIMessageBox(string.Empty)
            {
                Width = { Pixels = -25, Percent = 1f },
                Height = { Percent = 1f }
            };
            backPanel.Append(_messageBox);

            UIScrollbar scrollbar = new UIScrollbar
            {
                Height = { Pixels = -20, Percent = 1f },
                VAlign = 0.5f,
                HAlign = 1f
            }.WithView(100f, 1000f);
            backPanel.Append(scrollbar);
            _messageBox.SetScrollbar(scrollbar);

            _exitButton = new UITextPanel<string>(Language.GetTextValue("UI.Back"), 0.7f, true)
            {
                Width = { Pixels = -10, Percent = 1f },
                Height = { Pixels = 50 },
                Top = { Pixels = -108, Percent = 1f }
            }.WithFadedMouseOver();
            _exitButton.OnClick += ExitButtonClick;
            _area.Append(_exitButton);

            _previousButton = new UITextPanel<string>("Previous", 0.7f, true);
            _previousButton.CopyStyle(_exitButton);
            _previousButton.Width.Set(-10, 0.5f);
            _previousButton.Top.Set(-55f, 1f);
            _previousButton.WithFadedMouseOver();
            _previousButton.OnClick += PreviousOnClick;

            if (Index != ChangelogData.Changelogs.Count - 1)
                _area.Append(_previousButton);

            _nextButton = new UITextPanel<string>("Next", 0.7f, true);
            _nextButton.CopyStyle(_previousButton);
            _nextButton.HAlign = 1f;
            _nextButton.WithFadedMouseOver();
            _nextButton.OnClick += NextOnClick;

            if (Index != 0)
                _area.Append(_nextButton);

            Append(_area);
        }

        public override void OnActivate() => _messageBox.SetText(Changelog.ToString());

        private void NextOnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.menuMode = 888;
            Main.MenuUI.SetState(new UIChangelogBox(ChangelogData.Changelogs[Index - 1], Index - 1));
        }

        private void PreviousOnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.menuMode = 888;
            Main.MenuUI.SetState(new UIChangelogBox(ChangelogData.Changelogs[Index + 1], Index + 1));
        }

        private void ExitButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.menuMode = 888;
            Main.MenuUI.SetState(typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.UI.Interface").GetField("modsMenu", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null) as UIState);
        }
    }
}