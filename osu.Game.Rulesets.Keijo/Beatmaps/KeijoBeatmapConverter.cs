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
            var positionData = original as IHasPosition;
            var comboData = original as IHasCombo;
            var legacyOffset = original as IHasLegacyLastTickOffset;

            if (curveData != null)
            {
                yield return new Slider
                {
                    StartTime = original.StartTime,
                    Samples = original.Samples,
                    Path = curveData.Path,
                    NodeSamples = curveData.NodeSamples,
                    RepeatCount = curveData.RepeatCount,
                    Position = positionData?.Position ?? Vector2.Zero,
                    NewCombo = comboData?.NewCombo ?? false,
                    ComboOffset = comboData?.ComboOffset ?? 0,
                    LegacyLastTickOffset = legacyOffset?.LegacyLastTickOffset,
                    // prior to v8, speed multipliers don't adjust for how many ticks are generated over the same distance.
                    // this results in more (or less) ticks being generated in <v8 maps for the same time duration.
                    TickDistanceMultiplier = beatmap.BeatmapInfo.BeatmapVersion < 8 ? 1f / beatmap.ControlPointInfo.DifficultyPointAt(original.StartTime).SpeedMultiplier : 1
                };
            }
            else if (endTimeData != null)
            {
                yield return new Spinner
                {
                    StartTime = original.StartTime,
                    Samples = original.Samples,
                    EndTime = endTimeData.EndTime,
                    Position = positionData?.Position ?? KeijoPlayfield.BASE_SIZE / 2,
                    NewCombo = comboData?.NewCombo ?? false,
                    ComboOffset = comboData?.ComboOffset ?? 0,
                };
            }
            else
            {
                yield return new HitRegion
                {
                    StartTime = original.StartTime,
                    Samples = original.Samples,
                    Position = positionData?.Position ?? Vector2.Zero,
                    NewCombo = comboData?.NewCombo ?? false,
                    ComboOffset = comboData?.ComboOffset ?? 0,
                };
            }
        }

        protected override Beatmap<KeijoHitObject> CreateBeatmap() => new KeijoBeatmap();
    }
}
