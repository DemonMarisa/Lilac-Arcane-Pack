using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public override void ResetEffects()
        {
            ResetMainMiscFlag();
            ResetDRandDamage();
            ResetFocusStats_ResetEffect();
        }
    }
}
