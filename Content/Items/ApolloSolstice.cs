using Metanoia.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Metanoia.Content.Systems;
using Terraria.Audio;

namespace Metanoia.Content.Items
{
    public class ApolloSolstice : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToBow(6, 14, true);
            Item.width = 33;
            Item.height = 87;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 5f;
            Item.noMelee = true;
            Item.damage = 42;
            Item.value = Item.sellPrice(gold: 3, silver: 60);
            Item.autoReuse = true;
            Item.useAnimation = 14;
            Item.useTime = 14; // one third of useAnimation
            Item.reuseDelay = 15;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.channel = true;
            Item.UseSound = AudioSystem.ReturnSound("bow");
        }

        int shootCount = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (shootCount <= 5)
            {
                Item.DefaultToBow(6, 14, true);
                Item.UseSound = AudioSystem.ReturnSound("bow");
                Item.shoot = ProjectileID.PurificationPowder;
                float numberProjectiles = 4;
                float rotation = MathHelper.ToRadians(15);
                position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 45f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 Projectile.
                    Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback);
                }
            }
            else
            {
                for (int y = 0; y < 10; y++)
                {
                    SoundEngine.PlaySound(SoundID.Item13);
                }
                DustSystem.SpawnDustCircle((Vector2)player.HandPosition, DustID.Firework_Yellow, 20, 0.8f, 2f);
                Item.useAmmo = AmmoID.None;
                Item.shoot = ModContent.ProjectileType<ApolloBeam>();
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ApolloBeam>(), damage + 10, 0);
                shootCount = 0;
                Item.DefaultToBow(6, 14, true);
                Item.shoot = ProjectileID.PurificationPowder;
            }

            shootCount++;
            return false;
        }
    }
}