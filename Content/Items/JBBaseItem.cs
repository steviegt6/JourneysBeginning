using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;

namespace JourneysBeginning.Content.Items
{
    public abstract class JBBaseItem : ModItem
    {
        /// <summary>
        /// Assists with offsetting projectiles in <see cref="ModItem.Shoot(Player, ref Vector2, ref float, ref float, ref int, ref int, ref float)"/>.
        /// </summary>
        public void ProjectileOffsetHelper(ref float speedX, ref float speedY, ref Vector2 position, float off)
        {
            Vector2 offset = Vector2.Normalize(new Vector2(speedX, speedY)) * off;

            if (Collision.CanHit(position, 0, 0, position + offset, 0, 0))
                position += offset;
        }

        /// <summary>
        /// Can be used for items such as flamethrowers.
        /// </summary>
        public bool MultiuseAmmoHelper(Player player) => player.itemAnimation == player.itemAnimationMax - 1;
    }
}