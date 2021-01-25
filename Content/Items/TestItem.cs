using Terraria.ModLoader;
using Terraria;

namespace JourneysBeginning.Content.Items
{
    public class TestItem : EngineerWeapon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 22;
            Item.height = 22;
            Item.damage = 22;
            Item.useStyle = Terraria.ID.ItemUseStyleID.Swing;
            Item.useAnimation = 22;
            Item.useTime = 22;
        }

        public override void AddRecipes() =>
            CreateRecipe()
            .AddIngredient(Terraria.ID.ItemID.DirtBlock)
            .Register();
    }
}