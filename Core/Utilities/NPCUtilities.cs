using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        public static bool CheckType<T>(this NPC npc) where T : ModNPC
        {
            if (npc.type == ModContent.NPCType<T>())
                return true;

            return false;
        }
    }
}
