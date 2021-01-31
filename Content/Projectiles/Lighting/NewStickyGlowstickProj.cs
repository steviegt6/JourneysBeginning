using Microsoft.Xna.Framework;

namespace JourneysBeginning.Content.Projectiles.Lighting
{
    public class NewStickyGlowstickProj : GlowstickProj
    {
        public override GlowstickType GlowstickProjType => GlowstickType.Sticky;

        public override Vector3 ProjLight => new Vector3(Color.Green.R / 255f, Color.Green.G / 255f, Color.Green.B / 255f);
    }
}
