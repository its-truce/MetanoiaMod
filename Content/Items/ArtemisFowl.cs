using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Metanoia.Content.Systems;
using Terraria.Audio;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Metanoia.Content.Players;
using Metanoia.Content.Projectiles;
using System;

namespace Metanoia.Content.Items
{
    public class ArtemisFowl : ModItem
    {

        public override void SetDefaults()
        {
            Item.DefaultToBow(6, 14, true);
            Item.width = 34;
            Item.height = 78;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 5f;
            Item.noMelee = true;
            Item.useAmmo = AmmoID.Arrow;
            Item.damage = 60;
            Item.value = Item.sellPrice(gold: 3, silver: 60);
            Item.autoReuse = true;
            Item.useAnimation = 12;
            Item.useTime = 4; // one third of useAnimation
            Item.reuseDelay = 30;
            Item.consumeAmmoOnLastShotOnly = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.scale *= 1.25f;
            Item.UseSound = AudioSystem.ReturnSound("bow");
        }

        int shootCount = 0;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            shootCount++;
            if (shootCount == 15)
            {
                DustSystem.SpawnDustCircle((Vector2)player.HandPosition, DustID.TerraBlade, 20, 0.8f, 2f);
                Projectile.NewProjectile(source, position, velocity * 1.5f, ModContent.ProjectileType<ArtemisShot>(), damage + 150, 0);
                CameraSystem.Screenshake(6, 8);
                SoundEngine.PlaySound(AudioSystem.ReturnSound("ping"));
                Vector2 direction = -(velocity);
                direction.Normalize();
                player.velocity = direction * 10;
                shootCount = 0;
                player.vortexStealthActive = true;
                player.stealth = 100f;
                player.stealthTimer = 999;
                return false;
            }
            else
            {
                Item.DefaultToBow(6, 14, true);
                Item.UseSound = AudioSystem.ReturnSound("bow");
                Item.useAnimation = 12;
                Item.useTime = 4; // one third of useAnimation
            }
            return true;
        }
    }
}