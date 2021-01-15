using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;

namespace JourneysBeginning.Content.Items {
    public abstract class BaseItem : ModItem {

        // goes in shoot method for things that need it
        public void ProjectileOffsetHelper(ref float speedX, ref float speedY, ref Vector2 position, float off) {
            Vector2 offset = Vector2.Normalize(new Vector2(speedX, speedY)) * off;
            if (Collision.CanHit(position, 0, 0, position + offset, 0, 0)) {
                position += offset;
            }
        }
        // helps for things like flamethrowers
        public bool MultiuseAmmoHelper(Player player) => player.itemAnimation == player.itemAnimationMax - 1;
    }
}
