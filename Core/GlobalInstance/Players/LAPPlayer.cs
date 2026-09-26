using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        // 外围的玩家伤害减免
        public float ExternalDR = 1f;
        public float DamageMult = 1;// 在PostUpdateMisc里增加
        // 用于向上向下冲刺禁用羽落
        public int NoSlowFall = 0;
        // 无限飞行
        public bool InfiniteFlight = false;
        /// <summary>
        /// 用于加算的翅膀飞行时间百分比
        /// </summary>
        public float WingTimeMaxMult = 1f;
        /// <summary>
        /// 用于加算完后最终计算乘数
        /// </summary>
        public float PostWingTimeMaxMult = 1f;
        /// <summary>
        /// 额外受击无敌帧
        /// </summary>
        public int ExImmuneTime = 0;
        /// <summary>
        /// 治疗药水乘数
        /// </summary>
        public float healingPotionMult = 1f;
        public void ResetMainMiscFlag()
        {
            if (NoSlowFall > 0)
                NoSlowFall--;
            WingTimeMaxMult = 1f;
            PostWingTimeMaxMult = 1f;
            ExImmuneTime = 0;
            ExternalDR = 1f;
            healingPotionMult = 1f;
        }
        public void ResetDRandDamage()
        {
            DamageMult = 1;
        }
        public void UpdatePlayerMainBuff_PostUpdateMisc()
        {
            Player.GetDamage<GenericDamageClass>() *= DamageMult;
            Player.wingTimeMax = (int)(Player.wingTimeMax * WingTimeMaxMult);
            Player.wingTimeMax = (int)(Player.wingTimeMax * PostWingTimeMaxMult);
        }
        public void UpdateInfiniteFlight_PostUpdate()
        {
            if (InfiniteFlight)
            {
                Player.wingTime = Player.wingTimeMax;
                InfiniteFlight = false;
            }
        }
    }
}
