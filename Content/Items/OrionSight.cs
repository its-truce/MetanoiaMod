using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Metanoia.Content.Systems;
using Microsoft.Xna.Framework;
using Metanoia.Content.Projectiles;

namespace Metanoia.Content.Items
{
    public class OrionSight : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToBow(40, 11, true);
            Item.width = 60;
            Item.height = 30;
            Item.rare = ItemRarityID.Green;
            Item.knockBack = 8f;
            Item.damage = 13;
            Item.value = Item.sellPrice(silver: 60);
            Item.useAnimation = 30;
            Item.useTime = 15;
            Item.reuseDelay = 15;
            Item.consumeAmmoOnLastShotOnly = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.UseSound = AudioSystem.ReturnSound("bow");
            Item.scale = 1.2f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<OrionArrow>();
            }
        }
    }
}