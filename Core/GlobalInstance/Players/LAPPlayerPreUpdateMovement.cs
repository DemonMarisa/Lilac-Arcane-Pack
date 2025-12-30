using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public override void PreUpdateMovement()
        {
            if (NoSlowFall > 0)
            {
                Player.slowFall = false;
                Player.gravity = 1f;
            }
        }
    }
}
