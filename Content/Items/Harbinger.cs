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
    public class Harbinger : ModItem
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
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.mana = 35;
            Item.UseSound = AudioSystem.ReturnSound("wand");
            Item.DamageType = DamageClass.Magic;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<LightBolt>();
            Item.shootSpeed = 16;
        }
    }
}