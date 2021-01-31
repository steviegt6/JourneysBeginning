using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JourneysBeginning.Common
{
    public static class AssetManager
    {
        public struct AssetSwap
        {
            public FieldInfo SwappedField { get; set; }

            public Asset<Texture2D> OriginalTexture { get; set; }

            public Asset<Texture2D> SwappedTexture { get; set; }

            public int ArrayIndex { get; set; }

            private readonly object typeInstance;

            public AssetSwap(FieldInfo swappedField, Asset<Texture2D> originalTexture, Asset<Texture2D> swappedTexture, int arrayIndex = 0, object typeInstance = null)
            {
                SwappedField = swappedField;
                OriginalTexture = originalTexture;
                SwappedTexture = swappedTexture;
                ArrayIndex = arrayIndex;
                this.typeInstance = typeInstance;
            }

            public AssetSwap(FieldInfo swappedField, Asset<Texture2D> swappedTexture, int arrayIndex = 0, object typeInstance = null)
            {
                SwappedField = swappedField;
                SwappedTexture = swappedTexture;
                this.typeInstance = typeInstance;
                ArrayIndex = arrayIndex;
                OriginalTexture = SwappedField.GetValue(typeInstance) as Asset<Texture2D>;
            }

            public void Swap()
            {
                if (ArrayIndex > 0)
                    (SwappedField.GetValue(typeInstance) as Array).SetValue(SwappedTexture, ArrayIndex);
                else
                    SwappedField.SetValue(typeInstance, SwappedTexture);
            }

            public void Unswap()
            {
                if (ArrayIndex > 0)
                    (SwappedField.GetValue(typeInstance) as Array).SetValue(OriginalTexture, ArrayIndex);
                else
                    SwappedField.SetValue(typeInstance, OriginalTexture);
            }
        }

        public static List<AssetSwap> AssetSwaps;

        public static void Load()
        {
            Type assets = typeof(TextureAssets);

            AssetSwaps = new List<AssetSwap>()
            {
                #region Items

                new AssetSwap(assets.GetField("Item", BindingFlags.Static | BindingFlags.Public), GetItemSwapAsset("BouncyGlowstick"), ItemID.BouncyGlowstick),
                new AssetSwap(assets.GetField("Item", BindingFlags.Static | BindingFlags.Public), GetItemSwapAsset("StickyGlowstick"), ItemID.StickyGlowstick),

                #endregion Items

                #region Projectiles

                new AssetSwap(assets.GetField("Projectile", BindingFlags.Static | BindingFlags.Public), GetItemSwapAsset("BouncyGlowstick"), ProjectileID.BouncyGlowstick),
                new AssetSwap(assets.GetField("Projectile", BindingFlags.Static | BindingFlags.Public), GetItemSwapAsset("StickyGlowstick"), ProjectileID.StickyGlowstick)

#endregion Projectiles
            };
        }

        public static void SwapAssets()
        {
            if (!Main.dedServ)
                foreach (AssetSwap swap in AssetSwaps)
                    swap.Swap();
        }

        public static void Unload()
        {
            if (!Main.dedServ)
                foreach (AssetSwap swap in AssetSwaps)
                    swap.Unswap();
        }

        public static Asset<Texture2D> GetSwapAsset(string path) => ModContent.GetTexture($"JourneysBeginning/Assets/{path}");

        public static Asset<Texture2D> GetItemSwapAsset(string imageName) => GetSwapAsset($"ItemAssets/{imageName}");

        public static Asset<Texture2D> GetProjectileSwapAsset(string imageName) => GetSwapAsset($"ProjectileAssets/{imageName}");
    }
}
