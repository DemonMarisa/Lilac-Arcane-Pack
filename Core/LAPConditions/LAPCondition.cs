using System;
using Terraria;
using Terraria.Localization;

namespace LAP.Core.LAPConditions
{
    public class LAPCondition
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
        public static readonly Condition DownedAllMechBoss = Create("DownedAllMechBoss", () => NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3);
    }
}
