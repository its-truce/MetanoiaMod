using Metanoia.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Metanoia.Content.Systems;

namespace Metanoia.Content.Items
{
    public class GleipnirWhip : ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gleipnir"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            /* Tooltip.SetDefault("Binds enemies it hits, dealing constant damage." +
                "\n\"Made from the noise of a cat's footfall, a woman's beard, mountain roots," +
                "\nthe sinews of a bear, a fish's breath, and the spittle of a bird.\""); */
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DefaultToWhip(ModContent.ProjectileType<GleipnirRope>(), 153, 2, 4);
            Item.value = Item.sellPrice(gold: 13);
            Item.shootSpeed = 4;
            Item.rare = ItemRarityID.Red;
            Item.autoReuse = true;
            Item.UseSound = AudioSystem.ReturnSound("whip");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentStardust, 18);
            recipe.AddIngredient(ModContent.ItemType<Paradox>());
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.YellowStarDust, 0f, 0f, 0, default(Color), 1.7f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.3f;

            int dust2 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.BlueCrystalShard, 0f, 0f, 0, default(Color), 1.7f);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 0.3f;
        }
    }
}