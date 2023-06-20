using Metanoia.Content.Items;
using Metanoia.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Content.Pets
{
    public class GriffinItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Golden Feather");
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BirdieRattle);
            Item.shoot = ModContent.ProjectileType<GriffinPet>(); // "Shoot" your pet Projectile.
            Item.buffType = ModContent.BuffType<GriffinBuff>(); // Apply buff upon usage of the Item.
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600);
            }
        }
    }
}