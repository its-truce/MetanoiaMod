using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Metanoia.Content.Projectiles;
using Metanoia.Content.Systems;
using Terraria.Audio;

namespace Metanoia.Content.Items
{
    public class ZeusLightning : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.CrystalSerpent);
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.noUseGraphic = true;
            Item.damage = 120;
            Item.width = 14;
            Item.height = 68;
            Item.useTime = 1;
            Item.useAnimation = 2;
            Item.UseSound = AudioSystem.ReturnSound("thunder");
            Item.mana = 0;
            Item.crit = 11;
            Item.DamageType = DamageClass.Magic;
            Item.rare = ItemRarityID.Lime;
            Item.mana = 1;
        }

        bool IsInRange(Vector2 startPos, Vector2 mouseWorld)
        {
            // Calculate the min and max range vectors
            Vector2 minRange = mouseWorld - new Vector2(40, 40);
            Vector2 maxRange = mouseWorld + new Vector2(40, 40);

            // Check if startPos is within the range
            if (startPos.X >= minRange.X && startPos.X <= maxRange.X &&
                startPos.Y >= minRange.Y && startPos.Y <= maxRange.Y)
            {
                return true;
            }
            return false;
        }

        int timerMouse = -1;
        Vector2 startPos;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.statMana > 0)
            {
                player.statMana--;
            }
            timerMouse++;
            if (timerMouse % 60 == 0)
            { 
                startPos = Main.MouseWorld;
                SoundEngine.PlaySound(AudioSystem.ReturnSound("thunder"));
            }
            bool inRange = IsInRange(startPos, Main.MouseWorld);
            if (inRange)
            {
                Item.damage = 40;
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<ZeusBolt>(), damage, 6, player.whoAmI);
            }
            else
            {
                Item.damage = 120;
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<ZeusBolt>(), damage, 6, player.whoAmI);
            }
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center - new Vector2(0, 50), Vector2.Zero, ModContent.ProjectileType<LightningIcon>(), 0, 0, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.statMana > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}