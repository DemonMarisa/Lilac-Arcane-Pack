using LAP.Core.GlobalInstance.Players.DashSystem;
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
            LAPDashPlayer LAPPlayer = player.GetModPlayer<LAPDashPlayer>();
            int curid = LAPPlayer.CurDashID;
            if (LAPDashPlayer.DashCollection.IndexInRange(curid))
            {
                BasePlayerDash ActiveDash = LAPDashPlayer.DashCollection[curid];
                LAPPlayer.DashTime = 0;
                LAPPlayer.DashDelay = 0;
                LAPPlayer.BeginDash = false;
                ActiveDash.OnDashEnd(player);
            }
            LAPPlayer.OverideCurDashID = BeginDashID;
            BasePlayerDash newDash = LAPDashPlayer.DashCollection[BeginDashID];
            newDash.OnDashStart(player);
            LAPPlayer.DashTime = newDash.DashTime(player);
            player.SetImmuneTimeForAllTypes(newDash.ImmuneTime(player));
        }
        public static void SetLAPDash(this Player player, int DashID)
        {
            player.GetModPlayer<LAPDashPlayer>().CurDashID = DashID;
        }
    }
}
