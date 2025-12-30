using LAP.Content.Configs;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        // 外围的玩家伤害减免
        public float ExternalDR = 0;
        public float DamageMult = 1;// 在PostUpdateMisc里增加
        public int NoSlowFall = 0;
        public override void OnConsumeAmmo(Item weapon, Item ammo)
        {

        }
        public void ResetMainMiscFlag()
        {
            if (NoSlowFall > 0)
                NoSlowFall--;
        }
        public void ResetDRandDamage()
        {
            ExternalDR = 0;
            DamageMult = 1;
        }
    }
}
