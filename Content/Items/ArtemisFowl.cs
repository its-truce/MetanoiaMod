using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.UI;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Metanoia.Content.Players;
using Metanoia.Content.Projectiles;

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
            Item.damage = 54;
            Item.value = Item.sellPrice(gold: 3, silver: 60);
            Item.autoReuse = true;
            Item.useAnimation = 12;
            Item.useTime = 4; // one third of useAnimation
            Item.reuseDelay = 28;
            Item.consumeAmmoOnLastShotOnly = true;
            Item.shoot = ProjectileID.PurificationPowder;
        }

        int shootCount = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            shootCount++;
            if (shootCount == 15)
            {
                Item.damage = 150;
                Item.useAmmo = AmmoID.None;
                Item.shoot = ModContent.ProjectileType<Glowball>();
                shootCount = 0;
                Item.useAnimation = 12;
                Item.useTime = 12;
                player.vortexStealthActive = true;
                player.stealth = 100f;
                player.stealthTimer = 999;
                player.GetModPlayer<ScreenshakePlayer>().screenshakeMagnitude = 4;
                player.GetModPlayer<ScreenshakePlayer>().screenshakeTimer = 7;
            }
            else
            {
                Item.damage = 54;
                Item.useAmmo = AmmoID.Arrow;
                Item.shoot = ProjectileID.PurificationPowder;
                Item.useAnimation = 12;
                Item.useTime = 4;
            }
            return true;
        }
    }
}