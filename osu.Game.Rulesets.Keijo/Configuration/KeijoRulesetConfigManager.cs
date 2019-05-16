// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Configuration;
using osu.Game.Rulesets.Configuration;

namespace osu.Game.Rulesets.Osu.Configuration
{
    public class KeijoRulesetConfigManager : RulesetConfigManager<OsuRulesetSetting>
    {
        public KeijoRulesetConfigManager(SettingsStore settings, RulesetInfo ruleset, int? variant = null)
            : base(settings, ruleset, variant)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();
            Set(OsuRulesetSetting.SnakingInSliders, true);
            Set(OsuRulesetSetting.SnakingOutSliders, true);
            Set(OsuRulesetSetting.ShowCursorTrail, true);
        }
    }

    public enum OsuRulesetSetting
    {
        SnakingInSliders,
        SnakingOutSliders,
        ShowCursorTrail
    }
}
