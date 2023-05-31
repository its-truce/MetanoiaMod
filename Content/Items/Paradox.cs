using Metanoia.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Metanoia.Items
{
    public class Paradox : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Paradox"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("How does this even exist?");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;

            Item.maxStack = 99;
            Item.value = Item.buyPrice(gold: 5);
        }


        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}