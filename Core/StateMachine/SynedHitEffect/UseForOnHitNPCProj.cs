using LAP.Assets.TextureRegister;
using LAP.Content;
using LAP.Content.Particles;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LAP.Core.StateMachine.SynedHitEffect
{
    public class UseForOnHitNPCProj : ModProjectile, ILocalizedModType
    {
        public override LocalizedText DisplayName => LAPUtilities.GetItemName<TestItem>();
        public int ID => (int)Projectile.ai[0];
        public override string Texture => LAPTextureRegister.InvisibleTexturePath;
        public Player Owner => Main.player[Projectile.owner];
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.timeLeft = 20;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            if (Projectile.LAP().FirstFrame)
            {
                BaseHitEffect effect = HitEffectManager.HitEffect[ID];
                effect.HitEffect(Projectile, Projectile.GetSource_FromThis(), Owner);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
        public override void OnKill(int timeLeft)
        {
        }
    }
}
