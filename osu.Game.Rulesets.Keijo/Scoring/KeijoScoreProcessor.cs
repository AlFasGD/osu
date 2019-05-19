// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Extensions;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Keijo.Judgements;
using osu.Game.Rulesets.Keijo.Objects;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Keijo.Scoring
{
    internal class KeijoScoreProcessor : ScoreProcessor<KeijoHitObject>
    {
        public KeijoScoreProcessor(DrawableRuleset<KeijoHitObject> drawableRuleset)
            : base(drawableRuleset)
        {
        }

        private float hpDrainRate;

        protected override void ApplyBeatmap(Beatmap<KeijoHitObject> beatmap)
        {
            base.ApplyBeatmap(beatmap);

            hpDrainRate = beatmap.BeatmapInfo.BaseDifficulty.DrainRate;
        }

        protected override void Reset(bool storeResults)
        {
            base.Reset(storeResults);
        }

        protected override double HealthAdjustmentFactorFor(JudgementResult result)
        {
            switch (result.Type)
            {
                case HitResult.Great:
                    return 10.2 - hpDrainRate;

                case HitResult.Good:
                    return 8 - hpDrainRate;

                case HitResult.Meh:
                    return 4 - hpDrainRate;

                case HitResult.Miss:
                    return hpDrainRate;

                default:
                    return 0;
            }
        }

        public override HitWindows CreateHitWindows() => new KeijoHitWindows();
    }
}
