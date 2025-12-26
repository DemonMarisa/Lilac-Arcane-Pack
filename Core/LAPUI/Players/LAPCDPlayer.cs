using LAP.Core.LAPUI.CustomCD;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.ModLoader;

namespace LAP.Core.LAPUI.Players
{
    // 在玩家中更新Update等数据
    // 在UI全局管理中更新绘制需要的数据
    public class LAPCDPlayer : ModPlayer
    {
        public List<BaseCD> ActiveCD = [];
        public override void UpdateAutopause()
        {
            if (ActiveCD.Count == 0)
                return;
            for (int i = 0; i < ActiveCD.Count; i++)
            {
                // 在这里更新FadeInOut是因为保证别的玩家可以正常删除CD
                ActiveCD[i].FadeIn();
            }
            ActiveCD?.RemoveAll((i) =>
            {
                if (i.Time <= 0 && i.AniProgress <= 0 && i.BeginFadeOut)
                {
                    return true;
                }
                return false;
            });
        }
        public override void PostUpdateMiscEffects()
        {
            if (ActiveCD.Count == 0)
                return;
            for (int i = 0; i < ActiveCD.Count; i++)
            {
                ActiveCD[i].FadeIn();
                if (!ActiveCD[i].BeginFadeOut)
                {
                    ActiveCD[i].Update(Player);
                    if (ActiveCD[i].Time <= 0)
                    {
                        ActiveCD[i].BeginFadeOut = true;
                        ActiveCD[i].OnComplete(Player);// 当进入淡出阶段时，就判定已经完成了
                        if (ActiveCD[i].EndSound is not null)
                            SoundEngine.PlaySound(ActiveCD[i].EndSound);
                    }
                }
                if (ActiveCD[i].Time > 0 && ActiveCD[i].CanTickDown())
                    ActiveCD[i].Time--;
            }
            ActiveCD?.RemoveAll((i) =>
            {
                if (i.Time <= 0 && i.AniProgress <= 0 && i.BeginFadeOut)
                {
                    return true;
                }
                return false;
            });
        }
        public override void UpdateDead()
        {
            ActiveCD?.RemoveAll((i) =>
            {
                return i.CleanOnDeath();
            });
        }
    }
}
