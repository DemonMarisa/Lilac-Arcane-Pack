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
        // public static readonly Condition FirstEnterWorld = Create("HasEnterWorld", () => MiscConditions.HasEnterWorld);
        public static readonly Condition DownedBOC = Create("DownedBOC", () => DownedBoss.DownedBOC);
        public static readonly Condition DownedEOW = Create("DownedEOW", () => DownedBoss.DownedEOW);
        public static readonly Condition DownedBloodMoon = Create("DownedBloodMoon", () => DownedBoss.DownedBloodMoon);
    }
}
