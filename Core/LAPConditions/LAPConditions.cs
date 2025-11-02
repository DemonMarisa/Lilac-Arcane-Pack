using System;
using Terraria;
using Terraria.Localization;

namespace LAP.Core.LAPConditions
{
    public class LAPConditions
    {
        public static Condition Create(string key, Func<bool> predicate)
        {
            return new Condition(
                Language.GetText($"Mods.LAP.Condition.{key}"),
                predicate
            );
        }
        public static readonly Condition FirstEnterWorld = Create("FirstEnterWorld", () => CIDownedBossSystem.DownedBOC);
        public static readonly Condition DownedBOC = Create("DownedBOC", () => CIDownedBossSystem.DownedBOC);
        public static readonly Condition DownedEOW = Create("DownedEOW", () => CIDownedBossSystem.DownedEOW);
        public static readonly Condition DownedBloodMoon = Create("DownedBloodMoon", () => CIDownedBossSystem.DownedBloodMoon);
    }
}
