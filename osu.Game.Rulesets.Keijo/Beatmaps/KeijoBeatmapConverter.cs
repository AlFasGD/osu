// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osuTK;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Keijo.Objects;
using System.Collections.Generic;
using osu.Game.Rulesets.Objects.Types;
using System;
using osu.Game.Rulesets.Keijo.UI;

namespace osu.Game.Rulesets.Keijo.Beatmaps
{
    public class KeijoBeatmapConverter : BeatmapConverter<KeijoHitObject>
    {
        public KeijoBeatmapConverter(IBeatmap beatmap)
            : base(beatmap)
        {
        }

        protected override IEnumerable<Type> ValidConversionTypes { get; } = new[] { typeof(IHasPosition) };

        protected override IEnumerable<KeijoHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap)
        {
            var curveData = original as IHasCurve;
            var endTimeData = original as IHasEndTime;
            var positionData = original as IHasRegionPosition;

            if (curveData != null)
            {
                yield return new Slider
                {
                    StartTime = original.StartTime,
                    Samples = original.Samples,
                    Path = curveData.Path,
                    NodeSamples = curveData.NodeSamples,
                    RepeatCount = curveData.RepeatCount,
                    Position = new HitRegionPosition(positionData?.StartPosition ?? 0, positionData?.StartPosition ?? 1),
                    TickDistanceMultiplier = 1f / beatmap.ControlPointInfo.DifficultyPointAt(original.StartTime).SpeedMultiplier
                };
            }
            else if (endTimeData != null)
            {
                yield return new Spinner
                {
                    StartTime = original.StartTime,
                    Samples = original.Samples,
                    EndTime = endTimeData.EndTime,
                    Position = new HitRegionPosition(positionData?.StartPosition ?? 0, positionData?.StartPosition ?? 1),
                };
            }
            else
            {
                yield return new HitRegion
                {
                    StartTime = original.StartTime,
                    Samples = original.Samples,
                    Position = new HitRegionPosition(positionData?.StartPosition ?? 0, positionData?.StartPosition ?? 1),
                };
            }
        }

        protected override Beatmap<KeijoHitObject> CreateBeatmap() => new KeijoBeatmap();
    }
}
