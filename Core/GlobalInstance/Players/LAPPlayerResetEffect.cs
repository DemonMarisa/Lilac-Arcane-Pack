using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public override void ResetEffects()
        {
            ResetMainMiscFlag();
            ResetDRandDamage();
            // 重设专注值最大值恢复速度等
            ResetFocusStats_ResetEffect();
            // 更新最大专注值
            UpdateMaxFocus_ResetEffect();
            // 更新专注值恢复速度
            UpdateMaxFocusRenge_ResetEffect();
        }
    }
}
