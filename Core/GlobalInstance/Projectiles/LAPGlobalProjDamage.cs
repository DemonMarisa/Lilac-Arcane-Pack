using LAP.Core.CrossModSupports;
using LAP.Core.GlobalInstance.Items;
using LAP.Core.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Projectiles
{
    public partial class LAPGlobalProj : GlobalProjectile
    {
        public bool UseBoost;
        public float FatherMult;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (!CrossModSupport.UseCICalStatInflation)
                return;
            // 如果是弹幕出弹幕也应用增伤
            if (source is EntitySource_Parent Parent && Parent.Entity is Projectile proj && proj.TryGetGlobalProjectile(out LAPGlobalProj gp) && gp.UseBoost)
            {

                UseBoost = true;
                FatherMult = gp.FatherMult;
            }

            // 检查生成弹幕的源物品有没有应用增伤
            if (source is EntitySource_ItemUse iu && iu.Item.TryGetGlobalItem(out LAPGlobalItem gi) && gi.UseCICalStatInflation)
            {
                UseBoost = true;
                FatherMult = iu.Item.LAP().GetCalculatedDamageMult();
            }
        }
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (projectile.DamageType == DamageClass.Summon && UseBoost)
            {
                modifiers.FinalDamage *= FatherMult;
            }
        }
    }
}
