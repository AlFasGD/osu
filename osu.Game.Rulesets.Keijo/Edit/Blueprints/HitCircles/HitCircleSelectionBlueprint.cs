// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Keijo.Edit.Blueprints.HitCircles.Components;
using osu.Game.Rulesets.Keijo.Objects;
using osu.Game.Rulesets.Keijo.Objects.Drawables;

namespace osu.Game.Rulesets.Keijo.Edit.Blueprints.HitCircles
{
    public class HitCircleSelectionBlueprint : KeijoSelectionBlueprint
    {
        public HitCircleSelectionBlueprint(DrawableHitRegion hitCircle)
            : base(hitCircle)
        {
            InternalChild = new HitCirclePiece((HitRegion)hitCircle.HitObject);
        }
    }
}
