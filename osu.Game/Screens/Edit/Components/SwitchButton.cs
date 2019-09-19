﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Game.Graphics;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Edit.Components
{
    public class SwitchButton : CompositeDrawable, IHasCurrentValue<bool>, IHasAccentColour
    {
        private readonly Box fill;
        private readonly Container switchContainer;
        private readonly CircularContainer circularContainer;

        private const float border_thickness = 4.5f;
        private const float padding = 1.25f;

        private readonly BindableBool current = new BindableBool();

        public Bindable<bool> Current
        {
            get => current;
            set { }
        }

        private Color4 enabledColour;

        public Color4 EnabledColour
        {
            get => enabledColour;
            set
            {
                if (Current.Value)
                    AccentColour = value;
                enabledColour = value;
            }
        }

        private Color4 disabledColour;

        public Color4 DisabledColour
        {
            get => disabledColour;
            set
            {
                if (!Current.Value)
                    AccentColour = value;
                disabledColour = value;
            }
        }

        private Color4 accentColour;

        public Color4 AccentColour
        {
            get => accentColour;
            set
            {
                accentColour = value;
                circularContainer.BorderColour = value;
            }
        }

        public SwitchButton()
        {
            Container switchCircle;
            Size = new Vector2(45, 20);

            InternalChild = circularContainer = new CircularContainer
            {
                RelativeSizeAxes = Axes.Both,
                BorderColour = Color4.White,
                BorderThickness = border_thickness,
                Masking = true,
                Children = new Drawable[]
                {
                    fill = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        AlwaysPresent = true,
                        Alpha = 0
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Padding = new MarginPadding(border_thickness + padding),
                        Child = switchContainer = new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Child = switchCircle = new CircularContainer
                            {
                                RelativeSizeAxes = Axes.Both,
                                FillMode = FillMode.Fit,
                                Masking = true,
                                Child = new Box { RelativeSizeAxes = Axes.Both }
                            }
                        }
                    }
                }
            };

            Current.ValueChanged += newValue =>
            {
                if (newValue.NewValue)
                    switchCircle.MoveToX(switchContainer.DrawWidth - switchCircle.DrawWidth, 200, Easing.OutQuint);
                else
                    switchCircle.MoveToX(0, 200, Easing.OutQuint);
                updateHoverState();
                fill.FadeTo(newValue.NewValue ? 1 : 0, 500, Easing.OutQuint);
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            EnabledColour = colours.BlueDark;
            DisabledColour = colours.Gray3;
            switchContainer.Colour = enabledColour;
            fill.Colour = disabledColour;
        }

        protected override bool OnClick(ClickEvent e)
        {
            current.Toggle();
            return base.OnClick(e);
        }

        protected override bool OnHover(HoverEvent e)
        {
            updateHoverState();
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            updateHoverState();
            base.OnHoverLost(e);
        }

        private void updateHoverState()
        {
            this.FadeAccent((Current.Value ? enabledColour : disabledColour).Lighten(IsHovered ? 0.3f : 0), 500, Easing.OutQuint);
        }
    }
}
