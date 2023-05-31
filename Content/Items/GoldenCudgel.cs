using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Metanoia.Projectiles;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Metanoia.Items
{
    public class GoldenCudgel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ruyi Jingu Bang"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("\"The legendary shape-shifting cudgel of Sun Wukong strikes fear into your foes.\"");
        }

        public override void SetDefaults()
        {
            Item.damage = 92;
            Item.crit = 1;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.width = 86;
            Item.height = 86;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 9);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 14);
            recipe.AddIngredient(ItemID.GoldBar, 14);
            recipe.AddIngredient(ItemID.SoulofMight, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Firework_Yellow, 0f, 0f, 0, default(Color), 1.2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0;
        }

        public override bool AllowPrefix(int pre)
        {
            return false;
        }
    }
}