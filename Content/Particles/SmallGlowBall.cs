using LAP.Assets.TextureRegister;
using LAP.Core.ParticleSystem;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;

namespace LAP.Content.Particles
{
    public class SmallGlowBall : BaseParticle
    {
        public override int UseBlendStateID => BlendStateID.Additive;
        public float Speed = 5f;
        public int SeedOffset = 0;
        public float BeginScale = 1f;
        public SmallGlowBall(Vector2 position, Vector2 vel, Color color, int lifetime, float scale, float speed)
        {
            Position = position;
            Velocity = vel;
            DrawColor = color;
            Lifetime = lifetime;
            Scale = scale;
            BeginScale = scale;
            Speed = speed;
        }
        public override void OnSpawn()
        {
            SeedOffset = Main.rand.Next(0, 100000);
        }
        public override void Update()
        {
            if (Speed != 0)
            {
                Vector2 idealVelocity = -Vector2.UnitY.RotatedBy(MathHelper.Lerp(-MathHelper.TwoPi, MathHelper.TwoPi, (float)Math.Sin(Time / 36f + SeedOffset) * 0.5f + 0.5f)) * Speed;
                float movementInterpolant = MathHelper.Lerp(0.01f, 0.25f, Utils.GetLerpValue(0, Lifetime / 2, Time, true));
                Velocity = Vector2.Lerp(Velocity, idealVelocity, movementInterpolant);
                Velocity = Velocity.SafeNormalize(-Vector2.UnitY) * Speed;
            }
            Velocity *= 0.9f;
            Scale = MathHelper.Lerp(BeginScale, 0, EasingHelper.EaseOutCubic(LifetimeRatio));
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = LAPTextureRegister.SmallGlowBall.Value;
            spriteBatch.Draw(texture, Position - Main.screenPosition, null, DrawColor * Opacity, Rotation, texture.Size() / 2, Scale, SpriteEffects.None, 0);
        }
    }
}
