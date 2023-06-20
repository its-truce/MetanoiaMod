using Metanoia.Content.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Metanoia.Content.Projectiles;

namespace Metanoia.Content.Items
{
    public class VikingAxe : ModItem
    {
        private int SwingDirection = 1;
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.DamageType = DamageClass.Melee;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 27;
            Item.useAnimation = 27;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 9f;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = AudioSystem.ReturnSound("slash");
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<CaliburnusHoldout>();
            Item.shootSpeed = 1f;
            Item.reuseDelay = 3;
        }

        int shootCount = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            shootCount++;
            if (shootCount < 5)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<VikingAxeHoldout>(), damage, knockback, player.whoAmI, 0f, SwingDirection);
                SwingDirection = -SwingDirection;
                Item.reuseDelay = 3;
            }
            else
            {
                Vector2 direction = player.DirectionTo(Main.MouseWorld);
                Vector2 spawn = player.Center + direction * 16f;
                direction.Normalize();
                Item.useStyle = ItemUseStyleID.Shoot;
                Projectile.NewProjectile(source, (Vector2)player.HandPosition, direction * 14, ModContent.ProjectileType<VikingAxeProj>(), damage, knockback, player.whoAmI, 0f);
                shootCount = 0;
            }
            return false;
        }
    }
}
