﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.States;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using System;

namespace osu.Game.Screens.Edit.Screens.Setup.Components
{
    public class SetupRadioButton : ClickableContainer, IHasCurrentValue<bool>, IHasAccentColour
    {
        private readonly OsuSetupRadioButtonSwitch radioButtonSwitch;
        private readonly OsuSpriteText radioButtonLabel;

        public event Action<SetupRadioButton> RadioButtonClicked;

        public void TriggerRadioButtonClicked(SetupRadioButton sender)
        {
            RadioButtonClicked?.Invoke(sender);
        }

        public const float BUTTON_SIZE = 20;

        private Color4 osuColourBlue;

        public string LabelText
        {
            get => radioButtonLabel.Text;
            set
            {
                radioButtonLabel.Text = value;
                Width = BUTTON_SIZE + 5 + radioButtonLabel.DrawWidth; // Fix radio button sizing according to label text
            }
        }

        public SetupRadioButton()
        {
            Size = new Vector2(BUTTON_SIZE + 5, BUTTON_SIZE);

            Children = new Drawable[]
            {
                radioButtonSwitch = new OsuSetupRadioButtonSwitch
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                },
                radioButtonLabel = new OsuSpriteText
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    X = BUTTON_SIZE + 5,
                }
            };

            Current.ValueChanged += a => radioButtonSwitch.Current.Value = a;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour osuColour)
        {
            osuColourBlue = osuColour.Blue;
        }

        protected override bool OnClick(InputState state)
        {
            Current.Value = true;
            TriggerRadioButtonClicked(this);
            return base.OnClick(state);
        }

        protected override bool OnHover(InputState state)
        {
            radioButtonSwitch.FadeAccent(radioButtonSwitch.DefaultColour.Lighten(0.3f), 500, Easing.OutQuint);
            radioButtonLabel.FadeColour(osuColourBlue, 500, Easing.OutQuint);
            return base.OnHover(state);
        }

        protected override void OnHoverLost(InputState state)
        {
            radioButtonSwitch.FadeAccent(radioButtonSwitch.DefaultColour, 500, Easing.OutQuint);
            radioButtonLabel.FadeColour(Color4.White, 500, Easing.OutQuint);
            base.OnHoverLost(state);
        }

        public Bindable<bool> Current { get; } = new Bindable<bool>();

        private Color4 accentColour;
        public Color4 AccentColour
        {
            get => accentColour;
            set
            {
                accentColour = value;
                BorderColour = value;
            }
        }

        protected class OsuSetupRadioButtonSwitch : CircularContainer, IHasCurrentValue<bool>, IHasAccentColour
        {
            private readonly Container radioButtonContainer;

            private const float border_thickness = 4.5f;
            private const float switch_padding = 1.25f;

            private Color4 defaultColour;
            public Color4 DefaultColour
            {
                get => defaultColour;
                set
                {
                    defaultColour = value;
                    AccentColour = value.Lighten(IsHovered ? 0.3f : 0);
                }
            }

            public OsuSetupRadioButtonSwitch()
            {
                Box innerFill;

                Size = new Vector2(BUTTON_SIZE);

                BorderColour = Color4.White;
                BorderThickness = border_thickness;

                AlwaysPresent = true;

                Children = new Drawable[]
                {
                    radioButtonContainer = new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Masking = true,
                        Child = innerFill = new Box
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(0)
                        }
                    }
                };

                Current.ValueChanged += newValue =>
                {
                    if (newValue)
                    {
                        const float new_size = BUTTON_SIZE - border_thickness - switch_padding;
                        radioButtonContainer.TransformTo(nameof(CornerRadius), new_size / 2, 500, Easing.OutQuint);
                        radioButtonContainer.ResizeTo(new_size, 500, Easing.OutQuint);
                        innerFill.ResizeTo(new_size, 500, Easing.OutQuint);
                    }
                    else
                    {
                        radioButtonContainer.TransformTo(nameof(CornerRadius), 0f, 500, Easing.OutQuint);
                        radioButtonContainer.ResizeTo(0, 500, Easing.OutQuint);
                        innerFill.ResizeTo(0, 500, Easing.OutQuint);
                    }
                };
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                DefaultColour = colours.BlueDark;
                radioButtonContainer.Colour = AccentColour;
            }

            public Bindable<bool> Current { get; } = new Bindable<bool>();

            private Color4 accentColour;
            public Color4 AccentColour
            {
                get => accentColour;
                set
                {
                    accentColour = value;
                    BorderColour = value;
                }
            }
        }
    }
}
