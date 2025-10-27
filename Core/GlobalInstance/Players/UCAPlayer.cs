using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        // 外围的玩家伤害减免
        public float ExternalDR = 0;
        public float DamageMult = 1;
        public override void ResetEffects()
        {
            ExternalDR = 0;
            DamageMult = 1;
        }
        public override void PostUpdateMiscEffects()
        {
            Player.GetDamage<GenericDamageClass>() *= DamageMult;
        }
        public override void PostUpdate()
        {
            UpdateMouseWorld();
        }
    }
}
