using JourneysBeginning.Common.Bases;
using JourneysBeginning.Common.Utilities;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace JourneysBeginning.Common.ILEdits
{
    public class AnglerShopIL : ILEdit
    {
        public override string DictKey => "Terraria.Main.GUIChatDrawInner";

        public override void Load() => IL.Terraria.Main.GUIChatDrawInner += AnglerShopButton;

        public override void Unload() => IL.Terraria.Main.GUIChatDrawInner -= AnglerShopButton;

        private void AnglerShopButton(ILContext il)
        {
            il.CreateCursor(out ILCursor c);

            AddAnglerShop(c);

            ILHelper.LogILCompletion("GUIChatDrawInner");
        }

        private void AddAnglerShop(ILCursor c)
        {
            for (int i = 0; i < 2; i++)
                if (!c.TryGotoNext(x => x.MatchLdcI4(NPCID.Angler)))
                {
                    ILHelper.LogILError("ldc.i4", NPCID.Angler.ToString(), i);
                    return;
                }

            c.Index += 2;

            c.EmitDelegate<Func<string>>(() => Language.GetTextValue("LegacyInterface.28"));
            c.Emit(OpCodes.Stloc_S, (byte)10);
        }
    }
}