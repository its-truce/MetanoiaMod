using Metanoia.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Content.Items
{
    public class PoseidonTrident : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SkipsInitialUseSound[Item.type] = true;
            ItemID.Sets.Spears[Item.type] = true; 
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 5, silver: 40);
            Item.width = 66;
            Item.height = 66;

            Item.useStyle = ItemUseStyleID.Shoot; 
            Item.useAnimation = 12;
            Item.useTime = 18;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;

            Item.damage = 25;
            Item.knockBack = 6.5f;
            Item.noUseGraphic = true; 
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;

            Item.shootSpeed = 3.7f; 
            Item.shoot = ModContent.ProjectileType<PoseidonTridentHoldout>();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override bool? UseItem(Player player)
        {
            if (!Main.dedServ && Item.UseSound.HasValue)
            {
                SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            }

            return null;
        }
    }
}