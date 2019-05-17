// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.Legacy;
using osu.Game.Configuration;
using osu.Game.Graphics;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Configuration;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Osu.Beatmaps;
using osu.Game.Rulesets.Osu.Configuration;
using osu.Game.Rulesets.Osu.Difficulty;
using osu.Game.Rulesets.Osu.Edit;
using osu.Game.Rulesets.Osu.Mods;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.Replays;
using osu.Game.Rulesets.Osu.UI;
using osu.Game.Rulesets.Replays.Types;
using osu.Game.Rulesets.UI;
using osu.Game.Scoring;
using System.Collections.Generic;

namespace osu.Game.Rulesets.Osu
{
    public class KeijoRuleset : Ruleset
    {
        public override DrawableRuleset CreateDrawableRulesetWith(WorkingBeatmap beatmap, IReadOnlyList<Mod> mods) => new DrawableKeijoRuleset(this, beatmap, mods);
        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) => new KeijoBeatmapConverter(beatmap);
        public override IBeatmapProcessor CreateBeatmapProcessor(IBeatmap beatmap) => new KeijoBeatmapProcessor(beatmap);

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[]
        {
            new KeyBinding(InputKey.Z, KeijoAction.LeftButton),
            new KeyBinding(InputKey.X, KeijoAction.RightButton),
            new KeyBinding(InputKey.MouseLeft, KeijoAction.LeftButton),
            new KeyBinding(InputKey.MouseRight, KeijoAction.RightButton),
        };

        public override IEnumerable<Mod> ConvertLegacyMods(LegacyMods mods)
        {
            if (mods.HasFlag(LegacyMods.Nightcore))
                yield return new KeijoModNightcore();
            else if (mods.HasFlag(LegacyMods.DoubleTime))
                yield return new KeijoModDoubleTime();

            if (mods.HasFlag(LegacyMods.Autopilot))
                yield return new KeijoModAutopilot();

            if (mods.HasFlag(LegacyMods.Autoplay))
                yield return new KeijoModAutoplay();

            if (mods.HasFlag(LegacyMods.Easy))
                yield return new KeijoModEasy();

            if (mods.HasFlag(LegacyMods.Flashlight))
                yield return new KeijoModFlashlight();

            if (mods.HasFlag(LegacyMods.HalfTime))
                yield return new KeijoModHalfTime();

            if (mods.HasFlag(LegacyMods.HardRock))
                yield return new KeijoModHardRock();

            if (mods.HasFlag(LegacyMods.Hidden))
                yield return new KeijoModHidden();

            if (mods.HasFlag(LegacyMods.NoFail))
                yield return new KeijoModNoFail();

            if (mods.HasFlag(LegacyMods.Perfect))
                yield return new KeijoModPerfect();

            if (mods.HasFlag(LegacyMods.Relax))
                yield return new KeijoModRelax();

            if (mods.HasFlag(LegacyMods.SpunOut))
                yield return new KeijoModSpunOut();

            if (mods.HasFlag(LegacyMods.SuddenDeath))
                yield return new KeijoModSuddenDeath();

            if (mods.HasFlag(LegacyMods.Target))
                yield return new KeijoModTarget();

            if (mods.HasFlag(LegacyMods.TouchDevice))
                yield return new KeijoModTouchDevice();
        }

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.DifficultyReduction:
                    return new Mod[]
                    {
                        new KeijoModEasy(),
                        new KeijoModNoFail(),
                        new MultiMod(new KeijoModHalfTime(), new KeijoModDaycore()),
                        new KeijoModSpunOut(),
                    };

                case ModType.DifficultyIncrease:
                    return new Mod[]
                    {
                        new KeijoModHardRock(),
                        new MultiMod(new KeijoModSuddenDeath(), new KeijoModPerfect()),
                        new MultiMod(new KeijoModDoubleTime(), new KeijoModNightcore()),
                        new KeijoModHidden(),
                        new MultiMod(new KeijoModFlashlight(), new KeijoModBlinds()),
                    };

                case ModType.Conversion:
                    return new Mod[]
                    {
                        new KeijoModTarget(),
                    };

                case ModType.Automation:
                    return new Mod[]
                    {
                        new MultiMod(new KeijoModAutoplay(), new ModCinema()),
                        new KeijoModRelax(),
                        new KeijoModAutopilot(),
                    };

                case ModType.Fun:
                    return new Mod[]
                    {
                        new KeijoModTransform(),
                        new KeijoModWiggle(),
                        new KeijoModGrow(),
                        new MultiMod(new ModWindUp<KeijoHitObject>(), new ModWindDown<KeijoHitObject>()),
                    };

                default:
                    return new Mod[] { };
            }
        }

        public override Drawable CreateIcon() => new SpriteIcon { Icon = OsuIcon.RulesetKeijo };

        public override DifficultyCalculator CreateDifficultyCalculator(WorkingBeatmap beatmap) => new KeijoDifficultyCalculator(this, beatmap);

        public override PerformanceCalculator CreatePerformanceCalculator(WorkingBeatmap beatmap, ScoreInfo score) => new KeijoPerformanceCalculator(this, beatmap, score);

        public override HitObjectComposer CreateHitObjectComposer() => new KeijoHitObjectComposer(this);

        public override string Description => "osu!keijo";

        public override string ShortName => "keijo";

        public override RulesetSettingsSubsection CreateSettings() => new KeijoSettingsSubsection(this);

        public override IConvertibleReplayFrame CreateConvertibleReplayFrame() => new KeijoReplayFrame();

        public override IRulesetConfigManager CreateConfig(SettingsStore settings) => new KeijoRulesetConfigManager(settings, RulesetInfo);

        public KeijoRuleset(RulesetInfo rulesetInfo = null)
            : base(rulesetInfo)
        {
        }
    }
}
