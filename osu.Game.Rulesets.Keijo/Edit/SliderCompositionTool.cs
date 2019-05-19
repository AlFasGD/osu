// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Edit.Tools;
using osu.Game.Rulesets.Keijo.Edit.Blueprints.Sliders;
using osu.Game.Rulesets.Keijo.Objects;

namespace osu.Game.Rulesets.Keijo.Edit
{
    public class SliderCompositionTool : HitObjectCompositionTool
    {
        public SliderCompositionTool()
            : base(nameof(Slider))
        {
        }

        public override PlacementBlueprint CreatePlacementBlueprint() => new SliderPlacementBlueprint();
    }
}
