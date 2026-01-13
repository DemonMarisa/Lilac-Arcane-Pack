using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public override void PostUpdateRunSpeeds()
        {
            if (NoSlowFall > 0)
            {
                Player.slowFall = false;
                Player.maxFallSpeed = 10000;
                Player.GoingDownWithGrapple = true;
            }
        }
    }
}
