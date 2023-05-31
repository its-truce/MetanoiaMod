using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Metanoia.Projectiles;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Metanoia.Items
{
	public class Mjolnir : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mjölnir"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("\"It's not for the worthy, it's just really heavy.\"");
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.PaladinsHammer);	
			Item.damage = 301;
			Item.crit = 4;
			Item.DamageType = DamageClass.MeleeNoSpeed;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(gold: 23);
            Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shootSpeed = 9.5f;
			Item.hammer = 200;
			Item.shoot = ModContent.ProjectileType<MjolnirHammer>();
			Item.noMelee = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<AscendedPaladinsHammer>(), 1);
			recipe.AddIngredient(ItemID.FragmentSolar, 30);
			recipe.AddIngredient(ItemID.LunarBar, 14);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.DrillContainmentUnit, 0f, 0f, 0, default(Color), 0.7f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity *= 0;
        }
    }
}