// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Game.Rulesets.Osu.UI.Cursor;
using osu.Game.Rulesets.UI;
using osu.Game.Screens.Play;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Osu.UI
{
    public class KeijoResumeOverlay : ResumeOverlay
    {
        private KeijoClickToResumeCursor clickToResumeCursor;

        private GameplayCursorContainer localCursorContainer;

        public override CursorContainer LocalCursor => State == Visibility.Visible ? localCursorContainer : null;

        protected override string Message => "Click the orange cursor to resume";

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(clickToResumeCursor = new KeijoClickToResumeCursor { ResumeRequested = Resume });
        }

        public override void Show()
        {
            base.Show();
            clickToResumeCursor.ShowAt(GameplayCursor.ActiveCursor.Position);

            if (localCursorContainer == null)
                Add(localCursorContainer = new KeijoCursorContainer());
        }

        public override void Hide()
        {
            localCursorContainer?.Expire();
            localCursorContainer = null;

            base.Hide();
        }

        protected override bool OnHover(HoverEvent e) => true;

        public class KeijoClickToResumeCursor : KeijoCursor, IKeyBindingHandler<KeijoAction>
        {
            public override bool HandlePositionalInput => true;

            public Action ResumeRequested;

            public KeijoClickToResumeCursor()
            {
                RelativePositionAxes = Axes.Both;
            }

            protected override bool OnHover(HoverEvent e)
            {
                updateColour();
                return base.OnHover(e);
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                updateColour();
                base.OnHoverLost(e);
            }

            public bool OnPressed(KeijoAction action)
            {
                switch (action)
                {
                    case KeijoAction.LeftButton:
                    case KeijoAction.RightButton:
                        if (!IsHovered) return false;

                        this.ScaleTo(new Vector2(2), TRANSITION_TIME, Easing.OutQuint);

                        ResumeRequested?.Invoke();
                        return true;
                }

                return false;
            }

            public bool OnReleased(KeijoAction action) => false;

            public void ShowAt(Vector2 activeCursorPosition) => Schedule(() =>
            {
                updateColour();
                this.MoveTo(activeCursorPosition);
                this.ScaleTo(new Vector2(4)).Then().ScaleTo(Vector2.One, 1000, Easing.OutQuint);
            });

            private void updateColour()
            {
                this.FadeColour(IsHovered ? Color4.White : Color4.Orange, 400, Easing.OutQuint);
            }
        }
    }
}
