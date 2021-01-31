using JourneysBeginning.Content.Projectiles.Lighting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JourneysBeginning.Content.Globals.GlobalItems
{
    public class GlowstickModificationGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            switch (item.type)
            {
                case ItemID.StickyGlowstick:
                    item.shoot = ModContent.ProjectileType<NewStickyGlowstickProj>();
                    break;

                case ItemID.BouncyGlowstick:
                    item.shoot = ModContent.ProjectileType<NewBouncyGlowstickProj>();
                    break;
            }
        }
    }
}
