﻿using System.Collections.Generic;
using System.Linq;
using LivingWorldMod.Common.VanillaOverrides;
using LivingWorldMod.Content.TileEntities.Interactables;
using LivingWorldMod.Custom.Classes;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LivingWorldMod.Common.Systems {
    /// <summary>
    /// System that handles some Waystone functionality, in tandem with the Waystone tile entity
    /// and tiles.
    /// </summary>
    public class WaystoneSystem : ModSystem {
        public List<WaystoneInfo> waystoneData;

        public static WaystoneEntity BaseWaystoneEntity => ModContent.GetInstance<WaystoneEntity>();

        public override void Load() {
            // Initialize Waystone data list
            waystoneData = new List<WaystoneInfo>();

            // Add Waystone layer to map
            Main.MapIcons.AddLayer(new WaystoneMapLayer());
        }

        public override void SaveWorldData(TagCompound tag) {
            //Save data then remove the tile entities, as work around 
            tag["waystoneData"] = waystoneData;
        }

        public override void LoadWorldData(TagCompound tag) {
            //Load Waystone data and place the tile entities in the world
            waystoneData = tag.GetList<WaystoneInfo>("waystoneData").ToList();

            foreach (WaystoneInfo info in waystoneData) {
                Point16 entityLocation = new Point16(info.tileLocation.X, info.tileLocation.Y);
                BaseWaystoneEntity.PlaceEntity(entityLocation.X, entityLocation.Y, (int)info.waystoneType, false);

                ((WaystoneEntity)TileEntity.ByPosition[entityLocation]).isActivated = info.isActivated;
            }
        }
    }
}