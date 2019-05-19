// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Keijo.Objects;
using osu.Game.Rulesets.Keijo.Replays;
using osu.Game.Scoring;
using osu.Game.Users;

namespace osu.Game.Rulesets.Keijo.Mods
{
    public class KeijoModAutoplay : ModAutoplay<KeijoHitObject>
    {
        public override Type[] IncompatibleMods => base.IncompatibleMods.Append(typeof(KeijoModAutopilot)).Append(typeof(KeijoModSpunOut)).ToArray();

        public override Score CreateReplayScore(IBeatmap beatmap) => new Score
        {
            ScoreInfo = new ScoreInfo { User = new User { Username = "Autoplay" } },
            Replay = new KeijoAutoGenerator(beatmap).Generate()
        };
    }
}
