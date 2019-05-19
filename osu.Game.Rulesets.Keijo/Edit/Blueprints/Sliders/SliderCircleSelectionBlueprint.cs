﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Keijo.Edit.Blueprints.Sliders.Components;
using osu.Game.Rulesets.Keijo.Objects;
using osu.Game.Rulesets.Keijo.Objects.Drawables;

namespace osu.Game.Rulesets.Keijo.Edit.Blueprints.Sliders
{
    public class SliderCircleSelectionBlueprint : KeijoSelectionBlueprint
    {
        public SliderCircleSelectionBlueprint(DrawableKeijoHitObject hitObject, Slider slider, SliderPosition position)
            : base(hitObject)
        {
            InternalChild = new SliderCirclePiece(slider, position);

            Select();
        }

        // Todo: This is temporary, since the slider circle masks don't do anything special yet. In the future they will handle input.
        public override bool HandlePositionalInput => false;
    }
}