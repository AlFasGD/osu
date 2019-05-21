// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Keijo.UI;
using osuTK;
using System;

namespace osu.Game.Rulesets.Keijo.Objects.Baselines
{
    /// <summary>
    /// Represents a baseline in a <see cref="KeijoPlayfield"/>. A baseline supports the hitobjects that appear during the map.
    /// </summary>
    public abstract class Baseline
    {
        private DisplayInfo displayInfo;

        /// <summary>
        /// The location of the baseline in the field.
        /// </summary>
        public Vector2 Location;
        /// <summary>
        /// The size of the baseline.
        /// </summary>
        public Vector2 Size;

        /// <summary>
        /// The rotation of the baseline.
        /// </summary>
        public float Rotation;

        /// <summary>
        /// The start position of the displayed part of the baseline, represented by a ratio within [0, 1].
        /// </summary>
        public float StartPosition
        {
            get => displayInfo.StartPosition;
            set => displayInfo.StartPosition = value;
        }
        /// <summary>
        /// The end position of the displayed part of the baseline, represented by a ratio within [0, 1].
        /// </summary>
        public float EndPosition
        {
            get => displayInfo.EndPosition;
            set => displayInfo.EndPosition = value;
        }
    }

    /// <summary>
    /// Provides information about the range of the baseline to display.
    /// </summary>
    public struct DisplayInfo
    {
        private float startPosition, endPosition;

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
        public float Length => endPosition - startPosition;

        public DisplayInfo(float start = 0, float end = 1)
        {
            startPosition = start;
            endPosition = end;
        }
    }
}
