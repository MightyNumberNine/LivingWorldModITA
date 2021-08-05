﻿using LivingWorldMod.Content.Tiles.Building;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace LivingWorldMod.Content.Items.Placeables.Building {
    public class SunslabBlockItem : BaseItem {
        public override void SetDefaults() {
            Item.CloneDefaults(ItemID.DirtBlock);
            Item.value = Item.buyPrice(silver: 1);
            Item.placeStyle = 0;
            Item.createTile = ModContent.TileType<SunslabBlockTile>();
        }
    }
}
