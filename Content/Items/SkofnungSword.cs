using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;
using Metanoia.Content.Projectiles;

namespace Metanoia.Content.Items
{
    public class SkofnungSword : ModItem
    {
        private int SwingDirection = 1;
        SoundStyle SwingSound = new SoundStyle("Metanoia/Content/Audio/swing");
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 27;
            Item.DamageType = DamageClass.Melee;
            Item.width = 52;
            Item.height = 58;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f;
            Item.value = Item.sellPrice(gold: 1, silver: 20);
            Item.rare = ItemRarityID.Green;
            Item.crit = 16;
            Item.UseSound = SwingSound;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<SkofnungHoldout>();
            Item.shootSpeed = 1f;
            Item.scale *= 1.6f;
            Item.reuseDelay = 15;
        }

        int shootCount = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            shootCount++;
            if (shootCount < 10)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.UseSound = SwingSound;
                Item.shootSpeed = 1f;
                Item.damage = Convert.ToInt16(Item.damage * 106 / 100);
                Item.shoot = ModContent.ProjectileType<SkofnungHoldout>();
                Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<SkofnungHoldout>(), damage, knockback, player.whoAmI, 0f, SwingDirection);
                SwingDirection = -SwingDirection;
                float xDirection = Main.MouseWorld.X > player.Center.X ? 1f : -1f;
                Vector2 direction = new Vector2(xDirection, 0);
                direction.Normalize();
                player.velocity = direction * 9;
                return false;
            }
            else if (shootCount == 10)
            {
                SoundStyle WandSound = new SoundStyle("Metanoia/Content/Audio/wand");
                Item.shootSpeed = 16f;
                Item.UseSound = WandSound;
                Vector2 direction = player.DirectionTo(Main.MouseWorld);
                Vector2 spawn = player.Center + direction * 16f;
                direction.Normalize();
                Item.useStyle = ItemUseStyleID.Shoot;
                Projectile.NewProjectile(source, (Vector2)player.HandPosition, direction * 12, ModContent.ProjectileType<SkofnungProj>(), 50, knockback, player.whoAmI, 0f);
                Item.damage = 27;
                shootCount = 0;
                Item.useStyle = ItemUseStyleID.Swing;
                return true;
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddIngredient(ItemID.IronBar, 20);
            recipe.AddIngredient(ItemID.ShadowScale, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
