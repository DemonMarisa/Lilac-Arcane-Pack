using LAP.Core.IDSets;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Projectiles
{
    public partial class LAPGlobalProj : GlobalProjectile
    {
        // 用于元素箭袋的是否分裂
        public bool canSplit = true;
        // 此弹幕是否来自于武器战技
        public bool isWeaponSkillProj = false;
        public override void SetDefaults(Projectile projectile)
        {

        }
        public override bool PreAI(Projectile projectile)
        {
            if (FirstFrame)
            {
                if (LAPIDSet.CantSplitProj.Contains(projectile.type) || LAPIDSet.HeldProj.Contains(projectile.type) || projectile.minion || 
                    !projectile.friendly || projectile.hostile || projectile.damage < 5)
                    canSplit = false;
            }
            return true;
        }
    }
}
