if (source is EntitySource_ItemUse_WithAmmo use && use.Item.type == ModContent.ItemType<ArtemisFowl>() || use.Item.type == ModContent.ItemType<ApolloSolstice>())
{
  fromOlympusBows = true;
}
