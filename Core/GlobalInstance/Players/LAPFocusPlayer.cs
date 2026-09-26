
using LAP.Content.Configs;
using LAP.Core.LAPConditions;
using LAP.Core.MiscDate;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {

        /// <summary>
        /// 是否使用消耗专注值的物品，必须和FocusRegenRate一同为0才可以恢复
        /// </summary>
        public int UseFocus = 0;
        /// <summary>
        /// 当前的专注值
        /// </summary>
        public int statFocus = 100;
        /// <summary>
        /// 不要修改这个值，用statFocusMax2来给予额外的最大专注值
        /// 这个值用于常态的最大专注值，不会每帧重置
        /// </summary>
        public int statFocusMax = 100;
        /// <summary>
        /// 真正的最大专注值
        /// 会每帧重置为statFocusMax的值
        /// </summary>
        public int statFocusMax2 = 100;
        /// <summary>
        /// 专注值的恢复量，以秒为单位
        /// </summary>
        public int FocusRegen = 2;
        /// <summary>
        /// 用于设置恢复专注值的延迟，用于没使用物品，但是也不给恢复的情况
        /// </summary>
        public int FocusRegenRate = 0;
        /// <summary>
        /// 基础速度
        /// </summary>
        public int BaseFocusRegen = 2;
        /// <summary>
        /// 脱战乘数
        /// </summary>
        public float OutBattleFocusRegenMult = 5f;
        // 用于储存专注值的恢复，以FocusRegen / 60获取的每帧应该恢复多少，当超过1时，增加1点专注值，并减少相应的池子
        public float FocusRegenPool = 0;
        /// <summary>
        /// 专注值效率
        /// </summary>
        public float FocusCost = 1f;
        /// <summary>
        /// 专注值恢复效率
        /// </summary>
        public float FocusRegenMult = 1f;
        /// <summary>
        /// 最大专注值的额外增加量
        /// </summary>
        public int MaxFocusAdd = 0;
        /// <summary>
        /// 最大专注值的额外乘数
        /// </summary>
        public float MaxFocusMult = 0;
        public void SaveFP(TagCompound tag)
        {
            tag.Add("LAPStatFocus", statFocus);
            tag.Add("LAPStatFocusMax", statFocusMax);
            tag.Add("LAPStatFocusMax2", statFocusMax2);
        }
        public void ReadFP(TagCompound tag)
        {
            statFocus = tag.GetInt("LAPStatFocus");
            statFocusMax = tag.GetInt("LAPStatFocusMax");
            statFocusMax2 = tag.GetInt("LAPStatFocusMax2");
        }
        public void ResetFocusStats_ResetEffect()
        {
            statFocusMax2 = statFocusMax;
            FocusRegen = BaseFocusRegen;
            FocusCost = 1f;
            FocusRegenMult = 1f;
            if (FocusRegenRate > 0)
                FocusRegenRate--;
            if (UseFocus > 0)
                UseFocus--;
        }
        public void UpdateMaxFocus_ResetEffect()
        {
            statFocusMax2 += MaxFocusAdd;
            statFocusMax2 += (int)(statFocusMax * MaxFocusMult);
            MaxFocusAdd = 0;
            MaxFocusMult = 0;
        }
        public void UpdateMaxFocusRenge_ResetEffect()
        {
        }
        public void PostUpdateFocus()
        {
            if (LAPConfig.Instance.DeBugInfo)
                statFocus = statFocusMax2;
        }
        public void RegenFocus_PostUpdate()
        {
            if (FocusRegenRate != 0 || UseFocus != 0)
                return;
            float ThisFrameRegen = FocusRegen / 60f;
            if (!LAPInfo.AnyBossHere)
                ThisFrameRegen = ThisFrameRegen * OutBattleFocusRegenMult;

            FocusRegenPool += ThisFrameRegen * FocusRegenMult;

            int RegenHowMany = (int)FocusRegenPool;

            if (RegenHowMany >= 1)
            {
                statFocus += RegenHowMany;
                FocusRegenPool -= RegenHowMany;
            }
        }
        public void ClampMaxFocus_PostUpdate()
        {
            if (statFocus > statFocusMax2)
                statFocus = statFocusMax2;
            if (statFocus < 0)
                statFocus = 0;
        }
    }
}
