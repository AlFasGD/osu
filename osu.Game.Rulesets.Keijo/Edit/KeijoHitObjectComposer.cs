// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Edit.Tools;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Edit.Blueprints.HitCircles;
using osu.Game.Rulesets.Osu.Edit.Blueprints.Sliders;
using osu.Game.Rulesets.Osu.Edit.Blueprints.Spinners;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using osu.Game.Rulesets.UI;
using osu.Game.Screens.Edit.Compose.Components;

namespace osu.Game.Rulesets.Osu.Edit
{
    public class KeijoHitObjectComposer : HitObjectComposer<KeijoHitObject>
    {
        public KeijoHitObjectComposer(Ruleset ruleset)
            : base(ruleset)
        {
        }

        protected override DrawableRuleset<KeijoHitObject> CreateDrawableRuleset(Ruleset ruleset, WorkingBeatmap beatmap, IReadOnlyList<Mod> mods)
            => new DrawableKeijoEditRuleset(ruleset, beatmap, mods);

        protected override IReadOnlyList<HitObjectCompositionTool> CompositionTools => new HitObjectCompositionTool[]
        {
            new HitCircleCompositionTool(),
            new SliderCompositionTool(),
            new SpinnerCompositionTool()
        };

        public override SelectionHandler CreateSelectionHandler() => new KeijoSelectionHandler();

        public override SelectionBlueprint CreateBlueprintFor(DrawableHitObject hitObject)
        {
            switch (hitObject)
            {
                case DrawableHitCircle circle:
                    return new HitCircleSelectionBlueprint(circle);

                case DrawableSlider slider:
                    return new SliderSelectionBlueprint(slider);

                case DrawableSpinner spinner:
                    return new SpinnerSelectionBlueprint(spinner);
            }

            return base.CreateBlueprintFor(hitObject);
        }
    }
}
