// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Osu.UI;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Osu.Edit
{
    public class DrawableKeijoEditRuleset : DrawableKeijoRuleset
    {
        public DrawableKeijoEditRuleset(Ruleset ruleset, WorkingBeatmap beatmap, IReadOnlyList<Mod> mods)
            : base(ruleset, beatmap, mods)
        {
        }

        protected override Playfield CreatePlayfield() => new KeijoPlayfieldNoCursor();

        public override PlayfieldAdjustmentContainer CreatePlayfieldAdjustmentContainer() => new KeijoPlayfieldAdjustmentContainer { Size = Vector2.One };

        private class KeijoPlayfieldNoCursor : KeijoPlayfield
        {
            protected override GameplayCursorContainer CreateCursor() => null;
        }
    }
}
