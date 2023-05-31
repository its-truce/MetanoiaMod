using Microsoft.Xna.Framework;
using Metanoia.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Items
{
    public class VikingAxe : ModItem
    {
        private int SwingDirection = 1;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Swing, swing, throw!");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Melee;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7f;
            Item.value = Item.sellPrice(silver: 43);
            Item.rare = ItemRarityID.Blue;
            Item.crit = 1;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<VikingAxeHoldout>();
            Item.shootSpeed = 1f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<VikingAxeHoldout>(), damage, knockback, player.whoAmI, 0f, this.SwingDirection);
            SwingDirection = -SwingDirection;
            return false;
        }
    }
}
