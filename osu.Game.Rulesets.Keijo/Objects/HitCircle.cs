﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Keijo.Judgements;

namespace osu.Game.Rulesets.Keijo.Objects
{
    public class HitCircle : KeijoHitObject
    {
        public override Judgement CreateJudgement() => new KeijoJudgement();
    }
}