using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Metanoia.Projectiles;

namespace Metanoia.Items
{
	public class AscendedPaladinsHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ascended Paladin's Hammer"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Now with an upgraded green tint!");
		}

		public override void SetDefaults()
		{
            Item.CloneDefaults(ItemID.PaladinsHammer);
			Item.damage = 135;
            Item.shootSpeed *= 1.25f;
			Item.knockBack = 8;
			Item.value = Item.sellPrice(gold: 12);
            Item.shoot = ModContent.ProjectileType<AscendedHammer>();
			Item.autoReuse = true;
        }
	}
}