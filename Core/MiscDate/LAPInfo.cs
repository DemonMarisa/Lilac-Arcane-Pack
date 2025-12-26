using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.MiscDate
{
    public class LAPInfo : ModSystem
    {
        public static bool AnyBossHere = false;
        public override void PreUpdateWorld()
        {
            AnyBossHere = false;
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && npc.boss)
                {
                    AnyBossHere = true;
                    return;
                }
            }
        }
    }
}
