using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public override void PostHurt(Player.HurtInfo info)
        {
            if (ExImmuneTime != 0)
            {
                if (info.CooldownCounter != ImmunityCooldownID.General)
                    Player.hurtCooldowns[info.CooldownCounter] += ExImmuneTime;
                else
                    Player.immuneTime += ExImmuneTime;
            }
        }
    }
}
