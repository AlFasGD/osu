// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Keijo.Objects.Baselines;
using osu.Game.Rulesets.Objects;
using osuTK;

namespace osu.Game.Rulesets.Keijo.Objects
{
    public abstract class KeijoHitObject : HitObject
    {
        // TODO: Test if these need to be adjusted
        public double TimePreempt = 600;
        public double TimeFadeIn = 400;

        public Baseline Baseline;

        public readonly Bindable<HitRegionPosition> PositionBindable = new Bindable<HitRegionPosition>();

        public HitRegionPosition Position
        {
            get => PositionBindable.Value;
            set => PositionBindable.Value = value;
        }
        public float StartPosition
        {
            get => PositionBindable.Value.StartPosition;
            set => PositionBindable.Value.StartPosition = value;
        }
        public float EndPosition
        {
            get => PositionBindable.Value.EndPosition;
            set => PositionBindable.Value.EndPosition = value;
        }

        public Vector2 PositionAt(float position)
        {
            // TODO: Implement some funky stuff to calculate the Vector2 position at a specific relative position
            return new Vector2();
        }

        protected override void ApplyDefaultsToSelf(ControlPointInfo controlPointInfo, BeatmapDifficulty difficulty)
        {
            base.ApplyDefaultsToSelf(controlPointInfo, difficulty);

            TimePreempt = (float)BeatmapDifficulty.DifficultyRange(difficulty.ApproachRate, 1800, 1200, 450);
            TimeFadeIn = (float)BeatmapDifficulty.DifficultyRange(difficulty.ApproachRate, 1200, 800, 300);
        }

        protected override HitWindows CreateHitWindows() => new KeijoHitWindows();
    }
}
