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
        /// <summary>
        /// 判断传入的玩家Index是否为本地玩家
        /// </summary>
        /// <returns></returns>
        public static bool IsLocalPlayer(int PlayerIndex)
        {
            return PlayerIndex == Main.myPlayer;
        }
        public static bool IsLocalPlayer(this Projectile proj)
        {
            return proj.owner == Main.myPlayer;
        }
    }
}
