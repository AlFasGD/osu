// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Rulesets.Keijo.Objects;
using osu.Game.Rulesets.Keijo.Objects.Drawables.Pieces;
using osuTK;

namespace osu.Game.Rulesets.Keijo.Edit.Blueprints.Spinners.Components
{
    public class SpinnerPiece : HitObjectPiece
    {
        private readonly Spinner spinner;
        private readonly CircularContainer circle;
        private readonly RingPiece ring;

        public SpinnerPiece(Spinner spinner)
            : base(spinner)
        {
            this.spinner = spinner;

            Origin = Anchor.Centre;

            RelativeSizeAxes = Axes.Both;
            FillMode = FillMode.Fit;
            Size = new Vector2(1.3f);

            InternalChildren = new Drawable[]
            {
                circle = new CircularContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Alpha = 0.5f,
                    Child = new Box { RelativeSizeAxes = Axes.Both }
                },
                ring = new RingPiece
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            Colour = colours.Yellow;

            PositionBindable.BindValueChanged(_ => updatePosition(), true);
        }

        private void updatePosition() => Position = spinner.PositionAt(0); // TODO: Fix this?

        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => circle.ReceivePositionalInputAt(screenSpacePos);
    }
}
