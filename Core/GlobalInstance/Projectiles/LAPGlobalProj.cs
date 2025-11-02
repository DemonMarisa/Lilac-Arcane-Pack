using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Projectiles
{
    public partial class LAPGlobalProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool OnceHitEffect = true;
        public bool FirstFrame = true;
        public override void AI(Projectile projectile)
        {
            if (FirstFrame)
            {
                FirstFrame = false;
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            OnceHitEffect = false;
        }
    }
}
