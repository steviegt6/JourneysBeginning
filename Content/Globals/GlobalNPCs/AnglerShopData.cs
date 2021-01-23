using Terraria;

namespace JourneysBeginning.Content.Globals.GlobalNPCs
{
    public struct AnglerShopData
    {
        public int[] itemTypes;
        public int itemType;
        public int reqQuests;
        public bool hardmode;

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
            if (anglerItem.reqQuests <= Main.LocalPlayer.anglerQuestsFinished && ((anglerItem.hardmode && Main.hardMode) || !anglerItem.hardmode))
            {
                if (anglerItem.itemType != -1)
                    shop.item[nextSlot++].SetDefaults(anglerItem.itemType);
                else if (anglerItem.itemTypes != null)
                    foreach (int itemType in anglerItem.itemTypes)
                        shop.item[nextSlot++].SetDefaults(itemType);
            }
        }
    }
}