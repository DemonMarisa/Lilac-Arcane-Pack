using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public override void ResetEffects()
        {
            ResetDRandDamage();
            ResetFocusStats_ResetEffect();
        }
    }
}
