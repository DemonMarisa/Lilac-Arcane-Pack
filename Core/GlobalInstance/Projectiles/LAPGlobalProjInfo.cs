using LAP.Core.IDSets;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Projectiles
{
    public partial class LAPGlobalProj : GlobalProjectile
    {
        // 用于元素箭袋的是否分裂
        public bool canSplit = true;
        // 检查是否是手持弹幕的标志
        public bool isHeldProj = false;
        // 此弹幕是否来自于武器战技
        public bool isWeaponSkillProj = false;
        public override void SetDefaults(Projectile projectile)
        {
            // 检查是否可以分裂，查找到对应的值就不分裂
            if (LAPIDSet.CantSplitProj.Contains(projectile.type) || projectile.minion || !projectile.friendly || projectile.hostile || projectile.damage < 5)
                canSplit = false;
        }
    }
}
