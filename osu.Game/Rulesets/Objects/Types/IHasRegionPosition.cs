// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Game.Rulesets.Objects.Types
{
    // TODO: Determine whether this is staying in osu.Game.Rulesets.Objects.Types or will have to be specifically moved to osu.Rulesets.Keijo.Objects.Types
    // Also account for HitRegionPosition
    // TODO: Decide whether *RegionPosition will be renamed to *RelativePosition, *RegionRelativePositions or something similar
    /// <summary>
    /// A HitObject that has a region position; that is, its position is represented by a value within [0, 1] indicating the relative position within a container.
    /// </summary>
    public interface IHasRegionPosition
    {
        /// <summary>
        /// The starting position of the HitObject.
        /// </summary>
        float StartPosition { get; }
        /// <summary>
        /// The ending position of the HitObject.
        /// </summary>
        float EndPosition { get; }
    }
}
