using Metanoia.Content.Projectiles;
using Metanoia.Content.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Content.Items
{
    public class PoseidonTrident : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 5, silver: 40);
            Item.width = 66;
            Item.height = 66;

            Item.useStyle = ItemUseStyleID.Shoot; 
            Item.useAnimation = 18;
            Item.useTime = 18;
            SoundStyle BubbleSound = AudioSystem.ReturnSound("bubble");
            BubbleSound.PitchVariance = 0.5f;
            Item.UseSound = BubbleSound;
            Item.autoReuse = true;

            Item.damage = 25;
            Item.knockBack = 6.5f;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 9;

            Item.shootSpeed = 14f; 
            Item.shoot = ModContent.ProjectileType<PoseidonFish>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 direction = player.DirectionTo(Main.MouseWorld);
            direction.Normalize();
            int fishType = Main.rand.Next(1, 27);
            Projectile.NewProjectile(source, position, direction * 16, type, damage + fishType, 5, player.whoAmI, fishType);
            return false;
        }
    }
}