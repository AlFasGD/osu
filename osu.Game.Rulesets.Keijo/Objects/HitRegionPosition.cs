using System;
using System.Collections.Generic;
using System.Text;

namespace osu.Game.Rulesets.Keijo.Objects
{
    /// <summary>
    /// Represents the position of a hit region, containing a start position and an end position in ratios within [0, 1].
    /// </summary>
    public struct HitRegionPosition
    {
        private float startPosition, endPosition;

        /// <summary>
        /// The start position of the region, represented by a ratio within [0, 1].
        /// </summary>
        public float StartPosition
        {
            get => startPosition;
            set
            {
                if (value > endPosition)
                    throw new InvalidOperationException("The starting position cannot be greater than the ending position.");
                if (value < 0 || value > 1)
                    throw new InvalidOperationException("Cannot set the position to a value outside the range [0, 1].");
                startPosition = value;
            }
        }
        /// <summary>
        /// The end position of the region, represented by a ratio within [0, 1].
        /// </summary>
        public float EndPosition
        {
            get => endPosition;
            set
            {
                if (value < startPosition)
                    throw new InvalidOperationException("The starting position cannot be greater than the ending position.");
                if (value < 0 || value > 1)
                    throw new InvalidOperationException("Cannot set the position to a value outside the range [0, 1].");
                endPosition = value;
            }
        }

        /// <summary>
        /// The length of the region, represented by a ratio within [0, 1].
        /// </summary>
        public float Length => endPosition - startPosition;

        public HitRegionPosition(float start = 0, float end = 1)
        {
            startPosition = start;
            endPosition = end;
        }
    }
}
