// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Difficulty;

namespace osu.Game.Rulesets.Keijo.Difficulty
{
    public class KeijoDifficultyAttributes : DifficultyAttributes
    {
        public double AimStrain;
        public double SpeedStrain;
        public double ApproachRate;
        public double OverallDifficulty;
        public int MaxCombo;
    }
}
