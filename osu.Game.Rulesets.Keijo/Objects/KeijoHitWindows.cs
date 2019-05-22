// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Keijo.Objects
{
    public class KeijoHitWindows : HitWindows
    {
        private static readonly IReadOnlyDictionary<HitResult, (double od0, double od5, double od10)> base_ranges = new Dictionary<HitResult, (double, double, double)>
        {
            // Prime numbers are badass
            { HitResult.Great, (137, 83, 37) },
            { HitResult.Good, (233, 181, 113) },
            { HitResult.Meh, (367, 271, 173) },
            { HitResult.Miss, (367, 367, 367) },
        };

        public override void SetDifficulty(double difficulty)
        {
            Great = BeatmapDifficulty.DifficultyRange(difficulty, base_ranges[HitResult.Great]);
            Good = BeatmapDifficulty.DifficultyRange(difficulty, base_ranges[HitResult.Good]);
            Meh = BeatmapDifficulty.DifficultyRange(difficulty, base_ranges[HitResult.Meh]);
            Miss = BeatmapDifficulty.DifficultyRange(difficulty, base_ranges[HitResult.Miss]);
        }
    }
}
