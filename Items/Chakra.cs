using Metanoia.Content.Projectiles;
using Metanoia.Content.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Content.Items
{
    public class Chakra : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 42;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.knockBack = 8;
            Item.value = Item.sellPrice(gold: 3, silver: 57);
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.shootSpeed = 32f;
            Item.shoot = ModContent.ProjectileType<ChakraProj>();
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.channel = true;
            Item.UseSound = AudioSystem.ReturnSound("throw");
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}