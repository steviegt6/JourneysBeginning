using System.Collections.Generic;
using JourneysBeginning.Common.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace JourneysBeginning.Common.ModSystems
{
    public class SonarPotionItemSystem : ModSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            layers.TryInsertInterfaceLayer(layers.FindIndex(i => i.Name.Equals("Vanilla: MP Player Names")), new LegacyGameInterfaceLayer($"{JourneysBeginning.Instance.Name}: Sonar Potion Item",
                delegate
                {
                    foreach (PopupText text in Main.popupText)
                    {
                        if (text.sonar && text.active && text.context == PopupTextContext.SonarAlert)
                        {
                            ItemID.Search.TryGetId(text.name, out int itemType);

                            if (itemType <= 0)
                            {
                                for (int i = 0; i < ItemLoader.ItemCount; i++)
                                {
                                    Item item = new Item(i);

                                    if (item.AffixName() == text.name)
                                    {
                                        itemType = item.type;
                                        break;
                                    }
                                }
                            }

                            Asset<Texture2D> itemTex = TextureAssets.Item[itemType];

                            if (itemTex == null)
                                itemTex = Main.Assets.Request<Texture2D>($"Images/Item_{itemType}");

                            Vector2 position = new Vector2(text.position.X - Main.screenPosition.X + (int)(FontAssets.ItemStack.Value.MeasureString(text.name).X / 2f), text.position.Y - Main.screenPosition.Y - (TextureAssets.Item[itemType].Width() / 2f) - 4);

                            for (int i = 0; i < 4; i++)
                            {
                                Vector2 offsetPos = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                                Main.spriteBatch.Draw(TextureAssets.Item[itemType].ToFlatColor(Color.White), position + offsetPos, null, Color.White, 0f, TextureAssets.Item[itemType].Size() / 2f, text.scale, SpriteEffects.None, 0f);
                            }

                            Main.spriteBatch.Draw(TextureAssets.Item[itemType].Value, position, null, Color.White, 0f, TextureAssets.Item[itemType].Size() / 2f, text.scale, SpriteEffects.None, 0f);
                        }
                    }

                    return true;
                },
                InterfaceScaleType.UI));
        }
    }
}
