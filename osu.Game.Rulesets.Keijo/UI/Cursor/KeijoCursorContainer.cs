﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.Keijo.Configuration;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Keijo.UI.Cursor
{
    public class KeijoCursorContainer : GameplayCursorContainer, IKeyBindingHandler<KeijoAction>
    {
        protected override Drawable CreateCursor() => new KeijoCursor();

        protected override Container<Drawable> Content => fadeContainer;

        private readonly Container<Drawable> fadeContainer;

        private readonly Bindable<bool> showTrail = new Bindable<bool>(true);

        private readonly CursorTrail cursorTrail;

        public KeijoCursorContainer()
        {
            InternalChild = fadeContainer = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    cursorTrail = new CursorTrail { Depth = 1 }
                }
            };
        }

        [BackgroundDependencyLoader(true)]
        private void load(KeijoRulesetConfigManager config)
        {
            config?.BindWith(KeijoRulesetSetting.ShowCursorTrail, showTrail);

            showTrail.BindValueChanged(v => cursorTrail.FadeTo(v.NewValue ? 1 : 0, 200), true);
        }

        private int downCount;

        private void updateExpandedState()
        {
            if (downCount > 0)
                (ActiveCursor as KeijoCursor)?.Expand();
            else
                (ActiveCursor as KeijoCursor)?.Contract();
        }

        public bool OnPressed(KeijoAction action)
        {
            switch (action)
            {
                case KeijoAction.LeftButton:
                case KeijoAction.RightButton:
                    downCount++;
                    updateExpandedState();
                    break;
            }

            return false;
        }

        public bool OnReleased(KeijoAction action)
        {
            switch (action)
            {
                case KeijoAction.LeftButton:
                case KeijoAction.RightButton:
                    if (--downCount == 0)
                        updateExpandedState();
                    break;
            }

            return false;
        }

        public override bool HandlePositionalInput => true; // OverlayContainer will set this false when we go hidden, but we always want to receive input.

        protected override void PopIn()
        {
            fadeContainer.FadeTo(1, 300, Easing.OutQuint);
            ActiveCursor.ScaleTo(1, 400, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            fadeContainer.FadeTo(0.05f, 450, Easing.OutQuint);
            ActiveCursor.ScaleTo(0.8f, 450, Easing.OutQuint);
        }
    }
}