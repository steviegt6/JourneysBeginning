using JourneysBeginning.Common.Bases;
using JourneysBeginning.Common.Utilities;
using JourneysBeginning.Content.UI;
using JourneysBeginning.Content.UI.States;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace JourneysBeginning.Common.ILEdits
{
    public class UIModItemOnInitializeDrawSelfIL : ILEdit
    {
        private MethodInfo OnInitializeInfo;

        private event ILContext.Manipulator ModifyOnInitialize
        {
            add => HookEndpointManager.Modify(OnInitializeInfo, value);
            remove => HookEndpointManager.Unmodify(OnInitializeInfo, value);
        }

        public override string DictKey => "Terraria.ModLoader.UI.UIModItem.OnInitialize";

        public override void Load()
        {
            OnInitializeInfo = typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.UI.UIModItem").GetMethod("OnInitialize", BindingFlags.Public | BindingFlags.Instance);

            ModifyOnInitialize += AddConfigButtonIL;
        }

        public override void Unload() => ModifyOnInitialize -= AddConfigButtonIL;

        private void AddConfigButtonIL(ILContext il)
        {
            il.CreateCursor(out ILCursor c);

            AddUIButton(c);

            ILHelper.LogILCompletion("OnInitialize (UIModItem)");
        }

        private void AddUIButton(ILCursor c)
        {
            if (!c.TryGotoNext(x => x.MatchStfld(typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.UI.UIModItem").GetField("_moreInfoButton", BindingFlags.Instance | BindingFlags.NonPublic))))
            {
                ILHelper.LogILError("stfld", "Terraria.ModLoader.UI.UIModItem::UI_moreInfoButton");
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                if (!c.TryGotoNext(x => x.MatchLdfld(typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.UI.UIModItem").GetField("_moreInfoButton", BindingFlags.Instance | BindingFlags.NonPublic))))
                {
                    ILHelper.LogILError("ldfld", "Terraria.ModLoader.UI.UIModItem::UI_moreInfoButton");
                    return;
                }
            }

            c.Index += 2;

            c.Emit(OpCodes.Ldarg_0);
            c.Emit(OpCodes.Ldloc_0);
            c.EmitDelegate<Action<UIPanel, string>>((@this, name) =>
            {
                if (!name.Equals($"{JourneysBeginning.Instance.DisplayName} v{JourneysBeginning.Instance.Version}"))
                    return;

                // TODO: asset manager
                ChangelogData.ChangelogButton = new UIImage(ModContent.GetTexture("JourneysBeginning/Content/UI/ChangelogButton"))
                {
                    Width = { Pixels = 36f },
                    Height = { Pixels = 36f },
                    Left = { Pixels = -108f - 10f, Precent = 1f },
                    Top = { Pixels = 40f }
                };
                ChangelogData.ChangelogButton.OnClick += ChangelogButtonClick;

                @this.Append(ChangelogData.ChangelogButton);
            });
        }

        private void ChangelogButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.menuMode = 888;
            Main.MenuUI.SetState(new UIChangelogBox(ChangelogData.Changelogs.First(), 0));
        }
    }
}