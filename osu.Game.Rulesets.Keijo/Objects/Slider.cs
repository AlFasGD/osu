﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osuTK;
using osu.Game.Rulesets.Objects.Types;
using System.Collections.Generic;
using osu.Game.Rulesets.Objects;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Caching;
using osu.Game.Audio;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Keijo.Judgements;

namespace osu.Game.Rulesets.Keijo.Objects
{
    public class Slider : KeijoHitObject, IHasCurve
    {
        /// <summary>
        /// Scoring distance with a speed-adjusted beat length of 1 second.
        /// </summary>
        private const float base_scoring_distance = 100;

        public double EndTime => StartTime + this.SpanCount() * Path.Distance / Velocity;
        public double Duration => EndTime - StartTime;

        public readonly Bindable<SliderPath> PathBindable = new Bindable<SliderPath>();

        public SliderPath Path
        {
            get => PathBindable.Value;
            set
            {
                PathBindable.Value = value;
            }
        }

        public double Distance => Path.Distance;

        public double? LegacyLastTickOffset { get; set; }

        /// <summary>
        /// The position of the cursor at the point of completion of this <see cref="Slider"/> if it was hit
        /// with as few movements as possible. This is set and used by difficulty calculation.
        /// </summary>
        internal Vector2? LazyEndPosition;

        /// <summary>
        /// The distance travelled by the cursor upon completion of this <see cref="Slider"/> if it was hit
        /// with as few movements as possible. This is set and used by difficulty calculation.
        /// </summary>
        internal float LazyTravelDistance;

        public List<List<SampleInfo>> NodeSamples { get; set; } = new List<List<SampleInfo>>();

        private int repeatCount;

        public int RepeatCount
        {
            get => repeatCount;
            set
            {
                repeatCount = value;
            }
        }

        /// <summary>
        /// The length of one span of this <see cref="Slider"/>.
        /// </summary>
        public double SpanDuration => Duration / this.SpanCount();

        /// <summary>
        /// Velocity of this <see cref="Slider"/>.
        /// </summary>
        public double Velocity { get; private set; }

        /// <summary>
        /// Spacing between <see cref="SliderTick"/>s of this <see cref="Slider"/>.
        /// </summary>
        public double TickDistance { get; private set; }

        /// <summary>
        /// An extra multiplier that affects the number of <see cref="SliderTick"/>s generated by this <see cref="Slider"/>.
        /// An increase in this value increases <see cref="TickDistance"/>, which reduces the number of ticks generated.
        /// </summary>
        public double TickDistanceMultiplier = 1;

        public HitRegion HeadCircle;
        public SliderTailCircle TailCircle;

        protected override void ApplyDefaultsToSelf(ControlPointInfo controlPointInfo, BeatmapDifficulty difficulty)
        {
            base.ApplyDefaultsToSelf(controlPointInfo, difficulty);

            TimingControlPoint timingPoint = controlPointInfo.TimingPointAt(StartTime);
            DifficultyControlPoint difficultyPoint = controlPointInfo.DifficultyPointAt(StartTime);

            double scoringDistance = base_scoring_distance * difficulty.SliderMultiplier * difficultyPoint.SpeedMultiplier;

            Velocity = scoringDistance / timingPoint.BeatLength;
            TickDistance = scoringDistance / difficulty.SliderTickRate * TickDistanceMultiplier;
        }

        protected override void CreateNestedHitObjects()
        {
            base.CreateNestedHitObjects();

            foreach (var e in
                SliderEventGenerator.Generate(StartTime, SpanDuration, Velocity, TickDistance, Path.Distance, this.SpanCount(), LegacyLastTickOffset))
            {
                var firstSample = Samples.Find(s => s.Name == SampleInfo.HIT_NORMAL)
                                  ?? Samples.FirstOrDefault(); // TODO: remove this when guaranteed sort is present for samples (https://github.com/ppy/osu/issues/1933)
                var sampleList = new List<SampleInfo>();

                if (firstSample != null)
                    sampleList.Add(new SampleInfo
                    {
                        Bank = firstSample.Bank,
                        Volume = firstSample.Volume,
                        Name = @"slidertick",
                    });

                switch (e.Type)
                {
                    case SliderEventType.Tick:
                        AddNested(new SliderTick
                        {
                            SpanIndex = e.SpanIndex,
                            SpanStartTime = e.SpanStartTime,
                            StartTime = e.Time,
                            Position = Position + Path.PositionAt(e.PathProgress),
                            Samples = sampleList
                        });
                        break;

                    case SliderEventType.Head:
                        AddNested(HeadCircle = new SliderCircle
                        {
                            StartTime = e.Time,
                            Position = Position,
                            Samples = getNodeSamples(0),
                            SampleControlPoint = SampleControlPoint,
                        });
                        break;

                    case SliderEventType.Repeat:
                        AddNested(new RepeatPoint
                        {
                            RepeatIndex = e.SpanIndex,
                            SpanDuration = SpanDuration,
                            StartTime = StartTime + (e.SpanIndex + 1) * SpanDuration,
                            Position = Position + Path.PositionAt(e.PathProgress),
                            Samples = getNodeSamples(e.SpanIndex + 1)
                        });
                        break;
                }
            }
        }

        private List<SampleInfo> getNodeSamples(int nodeIndex) =>
            nodeIndex < NodeSamples.Count ? NodeSamples[nodeIndex] : Samples;

        public override Judgement CreateJudgement() => new KeijoJudgement();
    }
}
