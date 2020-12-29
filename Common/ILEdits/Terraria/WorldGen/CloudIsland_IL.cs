using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria.ID;

namespace JourneysBeginning.Common.ILEdits.Terraria.WorldGen
{
    public static class CloudIsland_IL
    {
        public static void ModifyGrassType(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            for (int i = 0; i < 8; i++)
            {
                if (!c.TryGotoNext(x => x.MatchLdcI4(0)))
                {
                    JBLogger.Log($"Could not match ldc.i4.0 ({i + 1})!");
                    return;
                }
            }

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, TileID.HallowedGrass);
        }
    }
}