using Microsoft.Xna.Framework;

namespace JourneysBeginning.Content.Projectiles.Lighting
{
    public class NewBouncyGlowstickProj : GlowstickProj
    {
        public override GlowstickType GlowstickProjType => GlowstickType.Bouncy;

        public override Vector3 ProjLight => new Vector3(Terraria.Main.DiscoColor.R / 255f, Terraria.Main.DiscoColor.G / 255f, Terraria.Main.DiscoColor.B / 255f);
    }
}
