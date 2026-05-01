using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Projectiles
{
    public partial class LAPGlobalProj : GlobalProjectile
    {
        // 此弹幕是否来自于武器战技
        public bool isWeaponSkillProj = false;
        public override void SetDefaults(Projectile projectile)
        {

        }
    }
}
