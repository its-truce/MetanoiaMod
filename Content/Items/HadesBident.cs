using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Metanoia.Content.Systems;
using Metanoia.Content.Projectiles;


namespace Metanoia.Content.Items
{
    public class HadesBident : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.CrystalSerpent);
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 50;
            Item.width = 76;
            Item.height = 76;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.mana = 10;
            Item.UseSound = AudioSystem.ReturnSound("wand");
            Item.DamageType = DamageClass.Magic;
            Item.rare = ItemRarityID.Orange;
        }

        int currentAttack = 0;
        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0)
            {
                currentAttack++;
                Vector2 target = Main.MouseWorld;
                Vector2 direction = target - player.Center;
                direction.Normalize();
                if (currentAttack == 1)
                {
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<SlashWhite>(), 30, 0, player.whoAmI);
                }
                else if (currentAttack == 2)
                {
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<SlashPurple>(), 50, 0, player.whoAmI);
                }
                else if (currentAttack == 3)
                {
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), Main.MouseWorld - new Vector2(0, 100), new Vector2(0, 7), ModContent.ProjectileType<BidentHand>(), 55, 0, player.whoAmI);
                    currentAttack = 0;
                }
            }
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }
    }
}