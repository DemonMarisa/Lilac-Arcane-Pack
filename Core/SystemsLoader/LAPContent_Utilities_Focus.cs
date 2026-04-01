using LAP.Content.Particles;
using LAP.Core.NetCode.NetUtilities;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

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
        /// 增加专注值的方法，later为true时为统一集中到指定恢复
        /// </summary>
        /// <param name="player"></param>
        /// <param name="amount"></param>
        /// <param name="later"></param>
        /// <returns></returns>
        public static void RestoreFocus(this Player player, int amount)
        {
            int regen = (int)(amount * player.LAP().FocusRegenMult);
            player.LAP().statFocus += regen;
            if (Main.myPlayer == player.whoAmI)
                player.FocusEffect(regen);
        }
        public static void FocusEffect(this Player player, int amount)
        {
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.SkyBlue, amount);
            if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer)
                player.SyncedRFocus(amount);
        }
        public static void SetUseFocus(this Player player, int Time)
        {
            player.LAP().UseFocus = Time;
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
        public static float FocusRatio(this Player player)
        {
            return (float)player.LAP().statFocus / (float)player.LAP().statFocusMax2;
        }
        public static int StatFocus(this Player player)
        {
            return player.LAP().statFocus;
        }
        public static int StatFocusMax2(this Player player)
        {
            return player.LAP().statFocusMax2;
        }
        public static int StatFocusMax(this Player player)
        {
            return player.LAP().statFocusMax;
        }
        public static int FocusRegen(this Player player)
        {
            return player.LAP().FocusRegen;
        }
        public static float FocusCost(this Player player)
        {
            return player.LAP().FocusCost;
        }
    }
}
