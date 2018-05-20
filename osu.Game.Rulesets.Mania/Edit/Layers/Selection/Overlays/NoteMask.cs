﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Allocation;
using osu.Game.Graphics;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Mania.Objects.Drawables;
using osu.Game.Rulesets.Mania.Objects.Drawables.Pieces;

namespace osu.Game.Rulesets.Mania.Edit.Layers.Selection.Overlays
{
    public class NoteMask : HitObjectMask
    {
        public NoteMask(DrawableNote note)
            : base(note)
        {
            Origin = Anchor.Centre;

            Position = note.Position;
            Size = note.Size;
            Scale = note.Scale;

            AddInternal(new NotePiece());

            note.HitObject.ColumnChanged += _ => Position = note.Position;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            Colour = colours.Yellow;
        }
    }
}
