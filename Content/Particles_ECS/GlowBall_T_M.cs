using LAP.Assets.TextureRegister;
using LAP.Core.ParticleSystem_ECS;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;

namespace LAP.Content.Particles_ECS
{
    public class GlowBall_T_M : ParticleBehaviors
    {
        public override void OnSpawn(ref ParticleData data)
        {
            data.aifloat2 = data.Scale;
            data.aiint0 = Main.rand.Next(0, 100000);
        }
        public override void Update(ref ParticleData data)
        {
            float Speed = data.aifloat0;
            if (Speed != 0)
            {
                Vector2 idealVelocity = -Vector2.UnitY.RotatedBy(MathHelper.Lerp(-MathHelper.TwoPi, MathHelper.TwoPi, (float)Math.Sin(data.Time / 36f + data.aiint0) * 0.5f + 0.5f)) * Speed;
                float movementInterpolant = MathHelper.Lerp(0.01f, 0.25f, Utils.GetLerpValue(0, data.Lifetime / 2, data.Time, true));
                data.Velocity = Vector2.Lerp(data.Velocity, idealVelocity, movementInterpolant);
                data.Velocity = data.Velocity.SafeNormalize(-Vector2.UnitY) * Speed;
            }
            data.Velocity *= 0.9f;
            data.Scale = MathHelper.Lerp(data.aifloat2, 0, EasingHelper.EaseOutCubic(data.LifetimeRatio));
        }
        public override void Draw(ref ParticleData data)
        {
            Texture2D texture = LAPTextureRegister.MediumGlowBall.Value;
            Main.spriteBatch.Draw(texture, data.Position - Main.screenPosition, null, data.DrawColor, data.Rotation, texture.Size() / 2, data.Scale, SpriteEffects.None, 0);
        }
    }
}
