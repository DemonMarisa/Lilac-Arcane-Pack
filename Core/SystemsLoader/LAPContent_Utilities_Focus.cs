using LAP.Core.Utilities;
using Terraria;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        /// <summary>
        /// 消耗专注值的判定方法，blockQuickFocus没用，以后也许有用
        /// </summary>
        /// <param name="player"></param>
        /// <param name="amount"></param>
        /// <param name="pay"></param>
        /// <param name="blockQuickFocus"></param>
        /// <returns></returns>
        public static bool CheckFocus(this Player player, int amount, bool pay = true, bool blockQuickFocus = false)
        {
            int cost = (int)(amount * player.FocusCost());
            if (player.StatFocus() >= cost)
            {
                if (pay)
                    player.LAP().statFocus -= cost;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取真正的专注值消耗
        /// </summary>
        /// <param name="player"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static int GetRealFocusCost(this Player player, int amount)
        {
            int cost = (int)(amount * player.FocusCost());
            return cost;
        }
    }
}
