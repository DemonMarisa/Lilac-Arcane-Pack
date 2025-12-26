using LAP.Core.LAPConditions;
using LAP.Core.MiscDate;
using Stubble.Core.Classes;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
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
        public int BaseFocusRegen = 2;
        public float OutBattleFocusRegenMult = 5f;
        // 用于储存专注值的恢复，以FocusRegen / 60获取的每帧应该恢复多少，当超过1时，增加1点专注值，并减少相应的池子
        public float FocusRegenPool = 0;
        /// <summary>
        /// 战技消耗的百分比
        /// </summary>
        public float FocusCost = 1f;
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
        }
        public void UpdateMaxFocus_PostUpdateMisc()
        {
            if (NPC.downedBoss3)
                statFocusMax2 += 50;
            if (Main.hardMode)
                statFocusMax2 += 50;
            if (LAPCondition.DownedAllMechBoss.IsMet())
                statFocusMax2 += 50;
            if (NPC.downedPlantBoss)
                statFocusMax2 += 50;
            if (NPC.downedGolemBoss)
                statFocusMax2 += 100;
            if (NPC.downedMoonlord)
                statFocusMax2 += 100;
        }
        public void UpdateMaxFocusRenge_PostUpdateMisc()
        {
            if (NPC.downedBoss3)
                FocusRegen += 1;
            if (Main.hardMode)
                FocusRegen += 1;
            if (LAPCondition.DownedAllMechBoss.IsMet())
                FocusRegen += 1;
            if (NPC.downedPlantBoss)
                FocusRegen += 1;
            if (NPC.downedGolemBoss)
                FocusRegen += 2;
            if (NPC.downedMoonlord)
                FocusRegen += 2;
        }
        public void RegenFocus_PostUpdateMisc()
        {
            float ThisFrameRegen = FocusRegen / 60f;
            if (!LAPInfo.AnyBossHere)
                ThisFrameRegen = ThisFrameRegen * OutBattleFocusRegenMult;
            FocusRegenPool += ThisFrameRegen;
            if (FocusRegenPool >= 1f)
            {
                int Regen = (int)FocusRegenPool - 1;
                statFocus += Regen;
                FocusRegenPool -= Regen;
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
