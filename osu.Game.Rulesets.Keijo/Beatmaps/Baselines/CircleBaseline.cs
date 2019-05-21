// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace osu.Game.Rulesets.Keijo.Beatmaps.Baselines
{
    /// <summary>
    /// Represents a circle baseline.
    /// </summary>
    public class CircleBaseline : Baseline
    {
        private uint edges;

        /// <summary>
        /// The number of edges of the polygon.
        /// 2 = Line
        /// 3 = Triangle
        /// 4 = Square
        /// etc.
        /// </summary>
        public int Edges
        {
            get => (int)edges;
            set
            {
                if (value < 2)
                    throw new InvalidOperationException("Cannot set the number of edges to a value below 2.");
                edges = (uint)value;
            }
        }

        public CircleBaseline(int edges)
        {
            Edges = edges;
        }
    }
}
