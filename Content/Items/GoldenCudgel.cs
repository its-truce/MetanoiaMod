using Metanoia.Content.Projectiles;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Transactions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Content.Items
{
    public class GoldenCudgel : ModItem
    {

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.MonkStaffT1);
            Item.reuseDelay = 0;
            Item.damage = 68;
            Item.crit = 1;
            Item.DamageType = DamageClass.Magic;
            Item.width = 80;
            Item.height = 80;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = false;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<CudgelHoldout1>();
            Item.mana = 12;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        bool hasCudgel3 = false;
        Projectile cudgel3;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Item.reuseDelay = 0;
            if (attackNumber == 1)
            {
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.mana = 12;
                Projectile.NewProjectile(source, player.Center, Vector2.Zero, type, damage, knockback, player.whoAmI);
            }
            else if (attackNumber == 2)
            {
                Item.useTime = 40;
                Item.useAnimation = 40;
                Item.mana = 30;
                Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<CudgelHoldout2>(), damage + 20, knockback + 3, player.whoAmI);
            }
            else if (attackNumber == 3)
            {
                Item.useTime = 60;
                Item.useAnimation = 60;
                Item.mana = 50;
                //IEnumerable<Projectile> ownedProjs = Main.Projectile.Take(Main.maxProjectiles).Where(p => p.active && p.owner == player.whoAmI);
                //if (Main.Projectile[player.heldProj].type == ModContent.ProjectileType<CudgelHoldout3>())
                //{
                //    hasCudgel3 = true;
                //    cudgel3 = Main.Projectile[player.heldProj];
                //}
                //if (hasCudgel3)
                //{
                //    cudgel3.timeLeft = 60;  
                //    cudgel3.ai[0] = 0f;
                //    cudgel3.rotation = 0;
                //    cudgel3.Center = player.Center;
                //}
                Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<CudgelHoldout3>(), damage + 30, knockback + 6, player.whoAmI);
            }
            return false;
        }

        int attackNumber = 1;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (attackNumber < 3)
                {
                    AdvancedPopupRequest request = default;
                    request.Text = "Engorgio!";
                    float seconds = Math.Min(request.Text.Length / 7.5f, 4);
                    request.DurationInFrames = (int)(seconds * 60);
                    request.Color = new Color(255, 217, 0);
                    PopupText.NewText(request, player.Center - new Vector2(0, 100));
                    attackNumber += 1;
                }
                else
                {
                    AdvancedPopupRequest request = default;
                    request.Text = "Reducio!";
                    float seconds = Math.Min(request.Text.Length / 7.5f, 4);
                    request.DurationInFrames = (int)(seconds * 60);
                    request.Color = new Color(255, 217, 0);
                    PopupText.NewText(request, player.Center - new Vector2(0, 100));
                    attackNumber = 1;
                }
                return false;
            }
            else
            {
                return true;
            }
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

        public override bool AllowPrefix(int pre)
        {
            return false;
        }
    }
}