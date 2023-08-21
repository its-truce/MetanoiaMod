using Metanoia.Content.Buffs;
using Metanoia.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Content.Items
{
    public class Sumarbrander : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 146;
            Item.knockBack = 4f;
            Item.mana = 16;
            Item.width = 60;
            Item.height = 60;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(gold: 27);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item44;
            Item.noMelee = false;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<SumarbranderBuff>();
            Item.shoot = ModContent.ProjectileType<SumarbranderSword>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            projectile.originalDamage = Item.damage;
            return false;
        }
    }
}