using LAP.Assets.TextureRegister;
using LAP.Core.ParticleSystem_ECS;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;

namespace LAP.Content.Particles_ECS
{
    public class TrailGlowBall_E : ParticleBehaviors
    {
        public override int MaxOldData => 8;
        public override int ExtraUpdate => 2;
        public override void OnSpawn(ref ParticleData particleDate)
        {
            base.OnSpawn(ref particleDate);
        }
        public override void Update(ref ParticleData particleDate)
        {
            particleDate.Scale = MathHelper.Lerp(particleDate.aifloat0, 0f, EasingHelper.EaseOutCubic(particleDate.LifetimeRatio));
            if (!particleDate.ExtraUpdate)
            {
                if (particleDate.aibool0)
                    particleDate.Velocity *= 0.9f;
                else
                    particleDate.Velocity *= 1.03f;
            }
        }
        public override unsafe void Draw(ref ParticleData particleDate)
        {
            for (int i = 0; i < MaxOldData; i++)
            {
                Texture2D texture = LAPTextureRegister.SmallGlowBall.Value;
                Main.spriteBatch.Draw(texture, particleDate.GetOldPos(i) - Main.screenPosition, null, particleDate.DrawColor * particleDate.Opacity, 0, texture.Size() / 2, particleDate.Scale, SpriteEffects.None, 0);
            }
        }
    }
}
