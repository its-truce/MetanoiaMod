using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Metanoia.Content.Projectiles;
using Metanoia.Content.Systems;

namespace Metanoia.Content.Items
{
    public class Bashosen : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 92;
            Item.DamageType = DamageClass.Melee;
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 8;
            Item.rare = ItemRarityID.Lime;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<BashosenHoldout>();
            Item.shootSpeed = 2f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.channel = true;
            Item.UseSound = AudioSystem.ReturnSound("fan", 1.5f, 0.3f);
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int basic = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            Main.projectile[basic].rotation = Main.projectile[basic].DirectionTo(Main.MouseWorld).ToRotation();
            return false;
        }
    }
}
