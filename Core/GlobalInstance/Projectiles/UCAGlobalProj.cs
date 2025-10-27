using Terraria;
using Terraria.ModLoader;

namespace UCA.Core.GlobalInstance.Projectiles
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

        public override void OnHitNPC(Projectile projectile, Terraria.NPC target, Terraria.NPC.HitInfo hit, int damageDone)
        {
            OnceHitEffect = false;
        }
    }
}
