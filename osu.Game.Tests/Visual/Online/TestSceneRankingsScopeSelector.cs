﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Bindables;
using osu.Game.Overlays;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Allocation;
using osu.Game.Graphics;
using osu.Framework.Extensions.Color4Extensions;

namespace osu.Game.Tests.Visual.Online
{
    public class TestSceneRankingsScopeSelector : OsuTestScene
    {
        public override IReadOnlyList<Type> RequiredTypes => new[]
        {
            typeof(RankingsScopeSelector),
        };

        private readonly Box background;

        public TestSceneRankingsScopeSelector()
        {
            Bindable<RankingsScope> scope = new Bindable<RankingsScope>();

            Add(background = new Box
            {
                RelativeSizeAxes = Axes.Both
            });

            Add(new RankingsScopeSelector
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Current = { BindTarget = scope }
            });

            AddStep(@"Select country", () => scope.Value = RankingsScope.Country);
            AddStep(@"Select performance", () => scope.Value = RankingsScope.Performance);
            AddStep(@"Select score", () => scope.Value = RankingsScope.Score);
            AddStep(@"Select spotlights", () => scope.Value = RankingsScope.Spotlights);
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            background.Colour = colours.Yellow.Opacity(50);
        }
    }
}
