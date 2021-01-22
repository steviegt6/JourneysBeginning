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
    public class UIChangelogBox : UIState
    {
        public int index;
        public ChangelogData changelog;
        public UIElement area;
        public UIMessageBox messageBox;
        public UITextPanel<string> exitButton;
        public UITextPanel<string> previousButton;
        public UITextPanel<string> nextButton;
        public UIState previousUIState;
        public UIState nextUIState;

        public UIChangelogBox(ChangelogData changelog, int index)
        {
            this.changelog = changelog;
            this.index = index;
        }

        public override void OnInitialize()
        {
            area = new UIElement
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
            area.Append(backPanel);

            messageBox = new UIMessageBox(string.Empty)
            {
                Width = { Pixels = -25, Percent = 1f },
                Height = { Percent = 1f }
            };
            backPanel.Append(messageBox);

            UIScrollbar scrollbar = new UIScrollbar
            {
                Height = { Pixels = -20, Percent = 1f },
                VAlign = 0.5f,
                HAlign = 1f
            }.WithView(100f, 1000f);
            backPanel.Append(scrollbar);
            messageBox.SetScrollbar(scrollbar);

            exitButton = new UITextPanel<string>(Language.GetTextValue("UI.Back"), 0.7f, true)
            {
                Width = { Pixels = -10, Percent = 1f },
                Height = { Pixels = 50 },
                Top = { Pixels = -108, Percent = 1f }
            }.WithFadedMouseOver();
            exitButton.OnClick += ExitButtonClick;
            area.Append(exitButton);

            previousButton = new UITextPanel<string>("Previous", 0.7f, true);
            previousButton.CopyStyle(exitButton);
            previousButton.Width.Set(-10, 0.5f);
            previousButton.Top.Set(-55f, 1f);
            previousButton.WithFadedMouseOver();
            previousButton.OnClick += PreviousOnClick;

            if (index != ChangelogData.Changelogs.Count - 1)
                area.Append(previousButton);

            nextButton = new UITextPanel<string>("Next", 0.7f, true);
            nextButton.CopyStyle(previousButton);
            nextButton.HAlign = 1f;
            nextButton.WithFadedMouseOver();
            nextButton.OnClick += NextOnClick;

            if (index != 0)
                area.Append(nextButton);

            Append(area);
        }

        public override void OnActivate() => messageBox.SetText(changelog.text);

        private void NextOnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.menuMode = 888;
            Main.MenuUI.SetState(new UIChangelogBox(ChangelogData.Changelogs[index - 1], index - 1));
        }

        private void PreviousOnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.menuMode = 888;
            Main.MenuUI.SetState(new UIChangelogBox(ChangelogData.Changelogs[index + 1], index + 1));
        }

        private void ExitButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.menuMode = 888;
            Main.MenuUI.SetState(typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.UI.Interface").GetField("modsMenu", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null) as UIState);
        }
    }
}