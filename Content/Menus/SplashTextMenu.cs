using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace JourneysBeginning.Content.Menus
{
    public class SplashTextMenu : ModMenu
    {
        public string drawText = "BASED";
        public float textScale = 1f;
        public bool textDirection = false;

        public override string DisplayName => "A";

        public override Asset<Texture2D> Logo => TextureAssets.Logo;

        public override void Update(bool isOnTitleScreen)
        {
            if (textScale >= 1.10f)
                textDirection = true;
            else if (textScale <= 0.90f)
                textDirection = false;

            if (textDirection)
                textScale -= 0.0075f;
            else
                textScale += 0.0075f;
        }

        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor) => false;

        public override void PostDrawLogo(SpriteBatch spriteBatch, Vector2 logoDrawCenter, float logoRotation, float logoScale, Color drawColor)
        {
            spriteBatch.Draw(Logo.Value, new Vector2(Main.screenWidth / 2, 100f), null, drawColor, 0f, Logo.Size() / 2f, 1f, SpriteEffects.None, 0f);

            for (int i = 0; i < 4; i++)
                spriteBatch.DrawString(FontAssets.DeathText.Value, drawText, new Vector2(Main.screenWidth / 2 + (Logo.Width() / 2), logoDrawCenter.Y * 1.5f) + (Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2), new Color(0, 0, 0, 200), MathHelper.ToRadians(-20f), FontAssets.DeathText.Value.MeasureString(drawText) / 2, 0.5f * textScale, SpriteEffects.None, 0f);

            spriteBatch.DrawString(FontAssets.DeathText.Value, drawText, new Vector2(Main.screenWidth / 2 + (Logo.Width() / 2), logoDrawCenter.Y * 1.5f), Main.OurFavoriteColor, MathHelper.ToRadians(-20f), FontAssets.DeathText.Value.MeasureString(drawText) / 2, 0.5f * textScale, SpriteEffects.None, 0f);
        }
    }
}
