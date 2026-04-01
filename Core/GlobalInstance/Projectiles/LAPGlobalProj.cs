using LAP.Core.AnimationHandle;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Projectiles
{
    public partial class LAPGlobalProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool OnceHitEffect = true;
        public bool FirstFrame = true;
        public AniHelper aniHelper = new AniHelper(5);
        public Vector2[] ai_vector2 = new Vector2[5];
        public override void AI(Projectile projectile)
        {
        }
        public override void PostAI(Projectile projectile)
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
