using Terraria;
using Terraria.ID;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        public static void SendProjSync(int Index)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
                return;
            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, Index);
        }
    }
}
