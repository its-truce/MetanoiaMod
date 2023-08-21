using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Metanoia.Content.Projectiles;
using Metanoia.Content.Systems;

namespace Metanoia.Content.Items
{
    public class Caliburnus : ModItem
    {
        private int SwingDirection = 1;
        private int[] Fireballs = new int[3] { -1, -1, -1 };

        public override void SetDefaults()
        {
            Item.damage = 96;
            Item.DamageType = DamageClass.Melee;
            Item.width = 112;
            Item.height = 112;
            Item.useTime = 27;
            Item.useAnimation = 27;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6f;
            Item.value = 30;
            Item.rare = ItemRarityID.Orange;
            Item.crit = 1;
            Item.UseSound = AudioSystem.ReturnSound("slash");
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<CaliburnusHoldout>();
            Item.shootSpeed = 1f;
        }

        int shootCount = 0;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            shootCount++;
            Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<CaliburnusHoldout>(), damage, knockback, player.whoAmI, 0f, SwingDirection);
            SwingDirection = -SwingDirection;

            if (shootCount < 4)
            {
                Fireballs[shootCount - 1] = Projectile.NewProjectile(source, player.Center, new Vector2(0f, 0f), ModContent.ProjectileType<CaliburnusShot>(), 70, 6, player.whoAmI, ai1: shootCount);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    Main.projectile[Fireballs[i]].ai[0] = 2;
                }
                shootCount = 0;
            }

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FieryGreatsword, 1);
            recipe.AddIngredient(ItemID.Excalibur, 1);
            recipe.AddIngredient(ItemID.SoulofLight, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
