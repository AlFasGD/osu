// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Keijo.Objects;
using osu.Game.Rulesets.Keijo.Objects.Drawables;
using osu.Game.Rulesets.UI;
using static osu.Game.Input.Handlers.ReplayInputHandler;

namespace osu.Game.Rulesets.Keijo.Mods
{
    public class KeijoModRelax : ModRelax, IApplicableFailOverride, IUpdatableByPlayfield, IApplicableToDrawableRuleset<KeijoHitObject>
    {
        public override string Description => @"You don't need to click. Give your clicking/tapping fingers a break from the heat of things.";
        public override Type[] IncompatibleMods => base.IncompatibleMods.Append(typeof(KeijoModAutopilot)).ToArray();

        public bool AllowFail => false;

        public void Update(Playfield playfield)
        {
            bool requiresHold = false;
            bool requiresHit = false;

            const float relax_leniency = 3;

            foreach (var drawable in playfield.HitObjectContainer.AliveObjects)
            {
                if (!(drawable is DrawableKeijoHitObject osuHit))
                    continue;

                double time = osuHit.Clock.CurrentTime;
                double relativetime = time - osuHit.HitObject.StartTime;

                if (time < osuHit.HitObject.StartTime - relax_leniency) continue;

                if (osuHit.HitObject is IHasEndTime hasEnd && time > hasEnd.EndTime || osuHit.IsHit)
                    continue;

                requiresHit |= osuHit is DrawableHitRegion && osuHit.IsHovered && osuHit.HitObject.HitWindows.CanBeHit(relativetime);
                requiresHold |= osuHit is DrawableSlider slider && (slider.Ball.IsHovered || osuHit.IsHovered) || osuHit is DrawableSpinner;
            }

            if (requiresHit)
            {
                addAction(false);
                addAction(true);
            }

            addAction(requiresHold);
        }

        private bool wasHit;
        private bool wasLeft;

        private KeijoInputManager osuInputManager;

        private void addAction(bool hitting)
        {
            if (wasHit == hitting)
                return;

            wasHit = hitting;

            var state = new ReplayState<KeijoAction>
            {
                PressedActions = new List<KeijoAction>()
            };

            if (hitting)
            {
                state.PressedActions.Add(wasLeft ? KeijoAction.LeftButton : KeijoAction.RightButton);
                wasLeft = !wasLeft;
            }

            state.Apply(osuInputManager.CurrentState, osuInputManager);
        }

        public void ApplyToDrawableRuleset(DrawableRuleset<KeijoHitObject> drawableRuleset)
        {
            // grab the input manager for future use.
            osuInputManager = (KeijoInputManager)drawableRuleset.KeyBindingInputManager;
            osuInputManager.AllowUserPresses = false;
        }
    }
}
