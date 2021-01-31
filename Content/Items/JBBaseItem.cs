using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using System.Collections.Generic;
using System.Linq;

namespace JourneysBeginning.Content.Items
{
    public abstract class JBBaseItem : ModItem
    {
        // Allows us to load items without a texture needed.
        // Useful for testing.
        public override string Texture
        {
            get
            {
                if (Mod.TextureExists(base.Texture))
                    return base.Texture;
                else
                {
                    Mod.Logger.Warn($"No texture found for Item: {Name}");
                    return "ModLoader/UnloadedItem";
                }
            }
        }

        /// <summary>
        /// The text displayed on the <c>Class</c> line, which appears under the name of an item.
        /// </summary>
        public virtual string ClassLineText => "";

        /// <summary>
        /// The color of <see cref="ClassLineText"/>.
        /// </summary>
        public virtual Color ClassLineColor => Color.White;

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
