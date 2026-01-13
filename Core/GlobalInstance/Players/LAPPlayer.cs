using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        // 外围的玩家伤害减免
        public float ExternalDR = 0;
        public float DamageMult = 1;// 在PostUpdateMisc里增加
        // 用于向上向下冲刺禁用羽落
        public int NoSlowFall = 0;
        /// <summary>
        /// 用于加算的翅膀飞行时间百分比
        /// </summary>
        public float WingTimeMaxMult = 1f;
        /// <summary>
        /// 用于加算完后最终计算乘数
        /// </summary>
        public float PostWingTimeMaxMult = 1f;
        public void ResetMainMiscFlag()
        {
            if (NoSlowFall > 0)
                NoSlowFall--;
            WingTimeMaxMult = 1f;
            PostWingTimeMaxMult = 1f;
        }
        public void ResetDRandDamage()
        {
            ExternalDR = 0;
            DamageMult = 1;
        }
        public void UpdatePlayerMainBuff()
        {
            Player.GetDamage<GenericDamageClass>() *= DamageMult;
            Player.wingTimeMax = (int)(Player.wingTimeMax * WingTimeMaxMult);
            Player.wingTimeMax = (int)(Player.wingTimeMax * PostWingTimeMaxMult);
        }
    }
}
