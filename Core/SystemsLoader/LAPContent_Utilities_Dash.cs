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
                LAPPlayer.DashTime = 0;
                LAPPlayer.DashDelay = 0;
                LAPPlayer.BeginDash = false;
                BasePlayerDash ActiveDash = LAPPlayer.CurDash;
                ActiveDash?.OnDashEnd(player);
            }
            LAPPlayer.OverideCurDashID = BeginDashID;
            BasePlayerDash newDash = LAPDashPlayer.DashCollection[BeginDashID];
            LAPPlayer.CurDash = newDash.Clone();
            LAPPlayer.CurDash.OnDashStart(player);
            LAPPlayer.DashTime = LAPPlayer.CurDash.DashTime(player);
            player.SetImmuneTimeForAllTypes(LAPPlayer.CurDash.ImmuneTime(player));
        }
        public static void SetLAPDash(this Player player, int DashID)
        {
            player.GetModPlayer<LAPDashPlayer>().CurDashID = DashID;
        }
    }
}
