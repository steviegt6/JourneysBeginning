using Terraria;
using Terraria.ID;

namespace JourneysBeginning.Content.Items
{
    public class TestItem : EngineerBaseItem
    {
        public override void SetDefaultsSafely()
        {
            Item.width = 22;
            Item.height = 22;
            Item.damage = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 22;
            Item.useTime = 22;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock)
                .Register();
        }
    }
}
