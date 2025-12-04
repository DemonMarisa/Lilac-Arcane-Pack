using LAP.Content.Configs;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (ExternalDR != 0)
            {
                ExternalDR = ExternalDR / (1f + ExternalDR);
                modifiers.SourceDamage *= 1 - ExternalDR;
            }
            if (LAPConfig.Instance.PlayerInjuryMult != 1f)
                modifiers.SourceDamage *= LAPConfig.Instance.PlayerInjuryMult;
        }
    }
}
