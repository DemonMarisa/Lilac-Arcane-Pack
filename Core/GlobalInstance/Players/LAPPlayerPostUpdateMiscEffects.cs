using LAP.Content.Configs;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public override void PostUpdateMiscEffects()
        {
            Player.GetDamage<GenericDamageClass>() *= DamageMult;
            UpdatePlayerAttackMult();
            UpdateMaxFocusRenge_PostUpdateMisc();
            UpdateMaxFocus_PostUpdateMisc();
            RegenFocus_PostUpdateMisc();
        }
        // 更新设置里的伤害增幅
        public void UpdatePlayerAttackMult()
        {
            if (LAPConfig.Instance.PlayerAttackMult != 1f)
                Player.GetDamage<GenericDamageClass>() *= LAPConfig.Instance.PlayerAttackMult;
        }
    }
}
