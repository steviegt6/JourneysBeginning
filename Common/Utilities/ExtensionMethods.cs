using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.UI;

namespace JourneysBeginning.Common.Utilities
{
    /// <summary>
    /// A collection of useful extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Attempts to insert a layer at the specified index. <br />
        /// Returns <c>false</c> if the specified index is <c>-1</c>, otherwise it returns true.
        /// </summary>
        public static bool TryInsertInterfaceLayer(this List<GameInterfaceLayer> layers, int index, LegacyGameInterfaceLayer layer)
        {
            if (index != -1)
            {
                layers.Insert(index, layer);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a <see cref="Texture2D"/> with all pixels converted to the specified color.
        /// </summary>
        public static Texture2D ToFlatColor(this Asset<Texture2D> texture, Color color) => texture.Value.ToFlatColor(color);

        /// <summary>
        /// Returns a <see cref="Texture2D"/> with all pixels converted to the specified color.
        /// </summary>
        public static Texture2D ToFlatColor(this Texture2D texture, Color color)
        {
            Texture2D flatTexture = new Texture2D(Main.spriteBatch.GraphicsDevice, texture.Width, texture.Height);
            Color[] textureData = new Color[flatTexture.Width * flatTexture.Height];

            texture.GetData(textureData);

            for (int i = 0; i < textureData.Length; i++)
                if (textureData[i].A > 0)
                    textureData[i] = new Color(color.R, color.G, color.B, color.A);

            flatTexture.SetData(textureData);
            return flatTexture;
        }
    }
}
