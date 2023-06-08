using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Metanoia.Content.Projectiles;

namespace Metanoia.Content.Items
{
	public class Mjolnir : ModItem
	{
        SoundStyle ThrowSound = new SoundStyle("Metanoia/Content/Audio/throw");
        public override void SetStaticDefaults()
		{
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.PaladinsHammer);	
			Item.damage = 301;
			Item.crit = 12;
			Item.DamageType = DamageClass.MeleeNoSpeed;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(gold: 23);
            Item.rare = ItemRarityID.Red;
			Item.UseSound = ThrowSound;
			Item.autoReuse = true;
			Item.shootSpeed = 9.5f;
			Item.hammer = 666;
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

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.knockBack = 7;
                Item.UseSound = SoundID.Item1;
                Item.autoReuse = true;
                Item.hammer = 666;
                Item.damage = 120;
                Item.shoot = ProjectileID.None;
                Item.noMelee = false;
                Item.noUseGraphic = false;
            }
            else
            {
                Item.damage = 301;
                Item.crit = 12;
                Item.DamageType = DamageClass.MeleeNoSpeed;
                Item.width = 40;
                Item.height = 40;
                Item.useTime = 15;
                Item.useAnimation = 15;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noUseGraphic = true;
                Item.knockBack = 2;
                Item.value = Item.sellPrice(gold: 23);
                Item.rare = ItemRarityID.Red;
                Item.UseSound = ThrowSound;
                Item.autoReuse = true;
                Item.shootSpeed = 9.5f;
                Item.hammer = 666;
                Item.shoot = ModContent.ProjectileType<MjolnirHammer>();
                Item.noMelee = true;

            }
            return base.CanUseItem(player);
        }
    }
}