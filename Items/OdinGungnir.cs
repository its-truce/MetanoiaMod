using Metanoia.Content.Projectiles;
using Metanoia.Content.Systems;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Content.Items
{
    public class OdinGungnir : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 52;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 8;
            Item.value = Item.sellPrice(gold: 3, silver: 57);
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.shootSpeed = 24f;
            Item.shoot = ModContent.ProjectileType<GungnirProj>();
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = AudioSystem.ReturnSound("throw");
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.UseSound = AudioSystem.ReturnSound("throw");
                Item.shootSpeed = 24f;
                Item.shoot = ModContent.ProjectileType<GungnirProj>();
                Item.useTime = 20;
                Item.useAnimation = 20;
                return true;
            }
            else
            {
                ItemID.Sets.SkipsInitialUseSound[Item.type] = true;
                ItemID.Sets.Spears[Item.type] = true;
                Item.UseSound = AudioSystem.ReturnSound("swing");
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.useAnimation = 16;
                Item.useTime = 24;
                Item.shootSpeed = 3.7f;
                Item.shoot = ModContent.ProjectileType<GungnirSpear>();
                return player.ownedProjectileCounts[ModContent.ProjectileType<GungnirSpear>()] < 1;
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (!Main.dedServ && Item.UseSound.HasValue && player.altFunctionUse == 2)
            {
                SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            }

            return null;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                float numberProjectiles = 3;
                float rotation = MathHelper.ToRadians(10);
                Vector2 direction = player.DirectionTo(Main.MouseWorld);
                direction.Normalize();
                direction *= 17;
                position += Vector2.Normalize(new Vector2(direction.X, direction.Y)) * 45f;

                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(direction.X, direction.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 Projectile.
                    Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<GungnirSpark>(), damage/3, knockback);
                }
            }
            return true;
        }
    }
}