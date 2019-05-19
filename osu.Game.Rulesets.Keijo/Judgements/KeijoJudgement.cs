// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Keijo.Judgements
{
    public class KeijoJudgement : Judgement
    {
        public override HitResult MaxResult => HitResult.Great;

        protected override int NumericResultFor(HitResult result)
        {
            switch (result)
            {
                default:
                    return 0;

                case HitResult.Meh:
                    return 50;

                case HitResult.Good:
                    return 100;

                case HitResult.Great:
                    return 300;
            }
        }

        protected override double HealthIncreaseFor(HitResult result)
        {
            switch (result)
            {
                case HitResult.Miss:
                    return -0.1;

                case HitResult.Meh:
                    return 0.005;

                case HitResult.Good:
                    return 0.03;

                case HitResult.Great:
                    return 0.075;

                default:
                    return 0;
            }
        }
    }
}
