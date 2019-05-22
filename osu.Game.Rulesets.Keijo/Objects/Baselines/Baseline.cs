// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Keijo.UI;
using osuTK;
using System;
using System.Collections.Generic;

namespace osu.Game.Rulesets.Keijo.Objects.Baselines
{
    /// <summary>
    /// Represents a baseline in a <see cref="KeijoPlayfield"/>. A baseline supports the hitobjects that appear during the map.
    /// </summary>
    public abstract class Baseline
    {
        private HitRegionPosition displayInfo;

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
        /// The nested hit objects of this baseline.
        /// </summary>
        public readonly IReadOnlyList<KeijoHitObject> HitObjects = new List<KeijoHitObject>();

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

        public void AddHitObject(KeijoHitObject hitObject)
        {
            // Check whether this hit object can be added to this baseline determined by whether its hit window overlaps with any other hit object's hit window
            hitObject.Baseline = this;
        }
    }
}
