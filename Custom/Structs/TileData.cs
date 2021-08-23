﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LivingWorldMod.Custom.Structs {

    /// <summary>
    /// Struct that holds Tile Data, mainly usage being for structures. Used for generating
    /// pre-determined structures.
    /// </summary>
    public readonly struct TileData {
        /* Relevant Tile Fields/Properties:
            tile.type;
            IsActivated
            IsHalfBlock;
            FrameNumber;
            frameX;
            frameY;
            tile.Slope;
            tile.Color;
            tile.IsActuated;
            tile.HasActuator;
            tile.RedWire;
            tile.BlueWire;
            tile.GreenWire;
            tile.YellowWire;
            tile.LiquidType;
            tile.LiquidAmount;
            tile.wall;
            tile.WallColor;
            tile.WallFrameNumber;
            ile.WallFrameX;
            tile.WallFrameY;
            */

        public readonly int type;

        public readonly bool isActivated;

        public readonly bool isHalfBlock;

        public readonly int frameNumber;

        public readonly int frameX;

        public readonly int frameY;

        public readonly int slopeType;

        public readonly int color;

        public readonly bool isActuated;

        public readonly bool hasRedWire;

        public readonly bool hasBlueWire;

        public readonly bool hasGreenWire;

        public readonly bool hasYellowWire;

        public readonly int liquidType;

        public readonly int liquidAmount;

        public readonly int wallType;

        public readonly int wallColor;

        public readonly int wallFrame;

        public readonly int wallFrameX;

        public readonly int wallFrameY;

        public readonly string modName;

        public readonly string modTileName;

        public TileData(Tile tile) {
            type = tile.type > 0 ? tile.type : -1;
            isActivated = tile.IsActive;
            isHalfBlock = tile.IsHalfBlock;
            frameNumber = tile.FrameNumber;
            frameX = tile.frameX;
            frameY = tile.frameY;
            slopeType = (int)tile.Slope;
            color = tile.Color;
            isActuated = tile.IsActuated;
            hasRedWire = tile.RedWire;
            hasBlueWire = tile.BlueWire;
            hasGreenWire = tile.GreenWire;
            hasYellowWire = tile.YellowWire;
            liquidType = tile.LiquidType;
            liquidAmount = tile.LiquidAmount;
            wallType = tile.wall;
            wallColor = tile.WallColor;
            wallFrame = tile.WallFrameNumber;
            wallFrameX = tile.WallFrameX;
            wallFrameY = tile.WallFrameY;

            ModTile modTile = ModContent.GetModTile(type);
            modName = modTile?.Mod.Name;
            modTileName = modTile?.Name;
        }

        public TileData(int type, bool isActivated, bool isHalfBlock, int frameNumber, int frameX, int frameY, int slopeType, int color, bool isActuated,
            bool hasRedWire, bool hasBlueWire, bool hasGreenWire, bool hasYellowWire, int liquidType, int liquidAmount, int wallType, int wallColor, int wallFrame,
            int wallFrameX, int wallFrameY) {
            this.type = type > 0 ? type : -1;
            this.isActivated = isActivated;
            this.isHalfBlock = isHalfBlock;
            this.frameNumber = frameNumber;
            this.frameX = frameX;
            this.frameY = frameY;
            this.slopeType = slopeType;
            this.color = color;
            this.isActuated = isActuated;
            this.hasRedWire = hasRedWire;
            this.hasBlueWire = hasBlueWire;
            this.hasGreenWire = hasGreenWire;
            this.hasYellowWire = hasYellowWire;
            this.liquidType = liquidType;
            this.liquidAmount = liquidAmount;
            this.wallType = wallType;
            this.wallColor = wallColor;
            this.wallFrame = wallFrame;
            this.wallFrameX = wallFrameX;
            this.wallFrameY = wallFrameY;

            ModTile modTile = ModContent.GetModTile(type);
            modName = modTile?.Mod.Name;
            modTileName = modTile?.Name;
        }

        public TagCompound ToCompound() {
            return new TagCompound() {
                {nameof(type), type},
                {nameof(isActivated), isActivated},
                {nameof(isHalfBlock), isHalfBlock},
                {nameof(frameNumber), frameNumber},
                {nameof(frameX), frameX},
                {nameof(frameY), frameY},
                {nameof(slopeType), slopeType},
                {nameof(color), color},
                {nameof(isActuated), isActuated},
                {nameof(hasRedWire), hasRedWire},
                {nameof(hasBlueWire), hasBlueWire},
                {nameof(hasGreenWire), hasGreenWire},
                {nameof(hasYellowWire), hasYellowWire},
                {nameof(liquidType), liquidType},
                {nameof(liquidAmount), liquidAmount},
                {nameof(wallType), wallType},
                {nameof(wallColor), wallColor},
                {nameof(wallFrame), wallFrame},
                {nameof(wallFrameX), wallFrameX},
                {nameof(wallFrameY), wallFrameY},
                {nameof(modName), modName},
                {nameof(modTileName), modTileName}
            };
        }
    }
}