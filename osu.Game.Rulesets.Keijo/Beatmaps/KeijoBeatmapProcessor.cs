// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Keijo.Objects;

namespace osu.Game.Rulesets.Keijo.Beatmaps
{
    public class KeijoBeatmapProcessor : BeatmapProcessor
    {
        public KeijoBeatmapProcessor(IBeatmap beatmap)
            : base(beatmap)
        {
        }

        public override void PostProcess()
        {
            base.PostProcess();

            var osuBeatmap = (Beatmap<KeijoHitObject>)Beatmap;
        }
    }
}
