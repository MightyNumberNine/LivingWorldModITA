﻿using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace LivingWorldMod.Common.VanillaOverrides.WorldGen.GenShapes {
    /// <summary>
    /// GenShape that generates an "equilateral" triangle with the passed in side length.
    /// </summary>
    public class EqualTriangle : GenShape {
        private readonly int _sideLength;

        public EqualTriangle(int sideLength) {
            _sideLength = sideLength;
        }

        public override bool Perform(Point origin, GenAction action) {
            bool hasCenter = _sideLength % 2 == 1;

            int x = 0;
            int y = 0;
            int layerWidthExcludingCenter = 0;

            while (layerWidthExcludingCenter * 2 + (hasCenter ? 1 : 2) <= _sideLength) {
                //Do action on the left side of the center
                for (; x <= 0; x++) {
                    if (!UnitApply(action, origin, origin.X + x, origin.Y + y)) {
                        return false;
                    }
                }
                //Do action on the right side of the center
                for (; x <= layerWidthExcludingCenter + (hasCenter ? 0 : 1); x++) {
                    if (!UnitApply(action, origin, origin.X + x, origin.Y + y)) {
                        return false;
                    }
                }
                //Increase length on both sides
                layerWidthExcludingCenter++;

                //Move i and j accordingly
                x = -layerWidthExcludingCenter;
                y++;
            }

            return true;
        }
    }
}