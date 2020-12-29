using JourneysBeginning.Common.ILEdits.Terraria.WorldGen;

namespace JourneysBeginning.Common.ILEdits
{
    public static class ILLoader
    {
        public static void LoadIL()
        {
            JBLogger.Log("Loading IL...");

            IL.Terraria.WorldGen.FloatingIsland += CloudIsland_IL.ModifyGrassType;

            JBLogger.Log("Loaded IL!");
        }

        public static void UnloadIL()
        {
            JBLogger.Log("Unloading IL...");

            IL.Terraria.WorldGen.FloatingIsland -= CloudIsland_IL.ModifyGrassType;

            JBLogger.Log("Unloaded IL!");
        }
    }
}