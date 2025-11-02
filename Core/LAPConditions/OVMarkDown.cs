using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.LAPConditions
{
    public class OVNPCMarkDown : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if (npc.type == NPCID.EaterofWorldsHead)
                DownedBoss.DownedEOW = true;
            if (npc.type == NPCID.BrainofCthulhu)
                DownedBoss.DownedBOC = true;
        }
    }
    public class OVPlayerMarkDown : ModSystem
    {
        public bool currentBloodMoon = false;
        public override void PostUpdateWorld()
        {
            if (DownedBoss.DownedBloodMoon)
                return;
            if (Main.bloodMoon)
                currentBloodMoon = true;
            if (!Main.bloodMoon && currentBloodMoon)
            {
                DownedBoss.DownedBloodMoon = true;
            }
        }
    }
}
