// teria usings

// class inherits from GlobalProjectile
bool fromOlympusBows;

// this goes under OnSpawn hook
if (source is EntitySource_ItemUse_WithAmmo use && (use.Item.type == ModContent.ItemType<ArtemisFowl>() || use.Item.type == ModContent.ItemType<ApolloSolstice>()))
{
  fromOlympusBows = true;
}

//override predraw here
if (projectile.arrow && fromOlympusBows)
{
  // draw code
}
