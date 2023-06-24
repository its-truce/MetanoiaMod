// teria usings

// class inherits from GlobalProjectile
bool fromOlympusBows;
int itemType;
Color drawColor;

// this goes under OnSpawn hook
if (source is EntitySource_ItemUse_WithAmmo use && (use.Item.type == ModContent.ItemType<ArtemisFowl>() || use.Item.type == ModContent.ItemType<ApolloSolstice>()))
{
  fromOlympusBows = true;
  itemType = use.Item.type;
}

//override predraw here
if (projectile.arrow && fromOlympusBows)
{
  drawColor = itemType == ModContent.ItemType<ArtemisFowl>() ? Color.Green : Color.Gold;
  // draw code here, use drawColor, multiply by float, set .A to 0
}
