// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Configuration;
using osu.Game.Rulesets.Configuration;

namespace osu.Game.Rulesets.Keijo.Configuration
{
    public class KeijoRulesetConfigManager : RulesetConfigManager<KeijoRulesetSetting>
    {
        public KeijoRulesetConfigManager(SettingsStore settings, RulesetInfo ruleset, int? variant = null)
            : base(settings, ruleset, variant)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();
            Set(KeijoRulesetSetting.SnakingInSliders, true);
            Set(KeijoRulesetSetting.SnakingOutSliders, true);
            Set(KeijoRulesetSetting.ShowCursorTrail, true);
        }
    }

    public enum KeijoRulesetSetting
    {
        SnakingInSliders,
        SnakingOutSliders,
        ShowCursorTrail
    }
}
