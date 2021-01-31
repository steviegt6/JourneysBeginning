using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace JourneysBeginning.Content.Projectiles.Lighting
{
    public abstract class GlowstickProj : JBBaseProj
    {
        public enum GlowstickType
        {
            Normal,
            Sticky,
            Bouncy
        }

        public virtual GlowstickType GlowstickProjType => GlowstickType.Normal;

        public abstract Vector3 ProjLight { get; }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 14;
            Projectile.penetrate = -1;
            Projectile.alpha = 75;
            Projectile.light = 1f;
            Projectile.timeLeft *= 5;

            if (GlowstickProjType == GlowstickType.Sticky)
            {
                Projectile.tileCollide = false;
                aiType = ProjectileID.StickyGlowstick;
            }
            else
            {
                if (GlowstickProjType == GlowstickType.Bouncy)
                    aiType = ProjectileID.BouncyGlowstick;
                else
                    aiType = ProjectileID.Glowstick;
            }
        }

        public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 255, 0);

        public override void AI() => Terraria.Lighting.AddLight(Projectile.position, ProjLight);
    }
}
