using LAP.Core.GlobalInstance.Players.DashSystem;
using LAP.Core.NetCode.NetUtilities;
using System;
using Terraria;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        /// <summary>
        /// 立刻结束当前冲刺，并用新的冲刺覆盖
        /// </summary>
        /// <param name="player"></param>
        public static void ImmediatelyDash(this Player player, int BeginDashID)
        {
            // 必须本地调用
            if (player.whoAmI != Main.myPlayer)
                return;
            LAPDashPlayer LAPPlayer = player.GetModPlayer<LAPDashPlayer>();
            LAPPlayer.ImmediatelyDashID = BeginDashID;
        }
        public static void SetLAPDash(this Player player, int DashID)
        {
            player.GetModPlayer<LAPDashPlayer>().CurDashID = DashID;
        }
    }
}
