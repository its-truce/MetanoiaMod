using Metanoia.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Metanoia.Content.Systems;
using Terraria.DataStructures;

namespace Metanoia.Content.Items
{
    public class NemesisWhip : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<NemesisRope>(), 40, 4, 6);
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Red;
            Item.autoReuse = true;
            Item.UseSound = AudioSystem.ReturnSound("whip");
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2[] velocities = {
            velocity,
            new Vector2(-velocity.X, velocity.Y), // Down-Left
            new Vector2(velocity.X, -velocity.Y), // Up-Right
            new Vector2(-velocity.X, -velocity.Y) // Up-Left
        };
                foreach (Vector2 vel in velocities)
                {
                    Projectile.NewProjectile(source, position, vel, type, damage / 2, knockback, player.whoAmI);
                }

                return false;
            }
            else
            {
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, ai1: 666);
                return false;
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.PurpleCrystalShard, 0f, 0f, 0, default(Color), 2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.3f;

            int dust2 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.VioletMoss, 0f, 0f, 0, default(Color), 1.7f);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 0.3f;
        }
    }
}