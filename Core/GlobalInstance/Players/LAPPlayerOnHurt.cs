using LAP.Content.Configs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (ExternalDR != 1f)
                modifiers.FinalDamage *= ExternalDR;
            if (LAPConfig.Instance.PlayerInjuryMult != 1f)
                modifiers.SourceDamage *= LAPConfig.Instance.PlayerInjuryMult;
        }
    }
}
