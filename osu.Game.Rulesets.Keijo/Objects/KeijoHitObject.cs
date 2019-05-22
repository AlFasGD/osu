// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects;
using osuTK;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Keijo.Objects.Baselines;

namespace osu.Game.Rulesets.Keijo.Objects
{
    public abstract class KeijoHitObject : HitObject
    {
        // TODO: Test if these need to be adjusted
        public double TimePreempt = 600;
        public double TimeFadeIn = 400;

        public Baseline Baseline;

        public readonly Bindable<DisplayInfo> PositionBindable = new Bindable<DisplayInfo>();

        protected override void ApplyDefaultsToSelf(ControlPointInfo controlPointInfo, BeatmapDifficulty difficulty)
        {
            base.ApplyDefaultsToSelf(controlPointInfo, difficulty);

            TimePreempt = (float)BeatmapDifficulty.DifficultyRange(difficulty.ApproachRate, 1800, 1200, 450);
            TimeFadeIn = (float)BeatmapDifficulty.DifficultyRange(difficulty.ApproachRate, 1200, 800, 300);
        }

        protected override HitWindows CreateHitWindows() => new KeijoHitWindows();
    }
}
