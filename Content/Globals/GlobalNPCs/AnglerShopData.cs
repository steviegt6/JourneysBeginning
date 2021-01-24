using Terraria;

namespace JourneysBeginning.Content.Globals.GlobalNPCs
{
    /// <summary>
    /// Struct containing basic vital data for easy additions to the Angler's shop. <br />
    /// Allows for specifying the amount of required quests, whether it's a hardmode-exclusive or not, and a method for adding one or multiple items.
    /// </summary>
    public struct AnglerShopData
    {
        public readonly int[] itemTypes;
        public readonly int itemType;
        public readonly int reqQuests;
        public readonly bool hardmode;

        public AnglerShopData(int itemType, int reqQuests = 0, bool hardmode = false)
        {
            itemTypes = null;

            this.itemType = itemType;
            this.reqQuests = reqQuests;
            this.hardmode = hardmode;
        }

        public AnglerShopData(int reqQuests, bool hardmode, params int[] itemTypes)
        {
            itemType = -1;

            this.itemTypes = itemTypes;
            this.reqQuests = reqQuests;
            this.hardmode = hardmode;
        }

        public static void AddQuestItem(AnglerShopData anglerItem, Chest shop, ref int nextSlot)
        {
            // Check conditions.
            if (anglerItem.reqQuests <= Main.LocalPlayer.anglerQuestsFinished && ((anglerItem.hardmode && Main.hardMode) || !anglerItem.hardmode))
            {
                // Logic for single-item additions
                if (anglerItem.itemType != -1)
                    shop.item[nextSlot++].SetDefaults(anglerItem.itemType);
                // Logic for multi-item additions
                else if (anglerItem.itemTypes != null)
                    foreach (int itemType in anglerItem.itemTypes)
                        shop.item[nextSlot++].SetDefaults(itemType);
            }
        }
    }
}