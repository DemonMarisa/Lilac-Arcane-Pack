using LAP.Assets.TextureRegister;
using LAP.Core.GlobalInstance.Players;
using LAP.Core.ParticleSystem;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;

namespace LAP.Content.Particles
{
    public class TrailGlowBall_T : BaseParticle
    {
        public override int UseBlendStateID => BlendStateID.Additive;
        public float BeginScale;
        public List<Vector2> OldPos = [];
        public float TSpeed = 1f;
        public float TToward = 1f;
        public float TAngle = 1f;
        public int SeedOffset = 0;
        public TrailGlowBall_T(Vector2 position, Color color, int lifetime, float scale, float tSpeed, float tToward, float tAngle)
        {
            Position = position;
            DrawColor = color;
            Lifetime = lifetime;
            Scale = scale;
            BeginScale = scale;
            TSpeed = tSpeed;
            TToward = tToward;
            TAngle = tAngle;
        }
        public override void OnSpawn()
        {
            SeedOffset = Main.rand.Next(0, 100000);
        }

        public override void Update()
        {
            if (TSpeed != 0)
            {
                Vector2 idealVelocity = -Vector2.UnitY.RotatedBy(MathHelper.Lerp(TToward - TAngle, TToward + TAngle, (float)Math.Sin(Time / 36f + SeedOffset) * 0.5f + 0.5f)) * TSpeed;
                float movementInterpolant = MathHelper.Lerp(0.01f, 0.1f, Utils.GetLerpValue(0, Lifetime, Time, true));
                Velocity = Vector2.Lerp(Velocity, idealVelocity, movementInterpolant);
                Velocity = Velocity.SafeNormalize(-Vector2.UnitY) * TSpeed;
            }

            Scale = MathHelper.Lerp(BeginScale, 0f, EasingHelper.EaseOutCubic(LifetimeRatio));

            OldPos.Add(Position);
            if (OldPos.Count > 8)
                OldPos.RemoveAt(0);
            Position += Velocity;
            OldPos.Add(Position);
            if (OldPos.Count > 8)
                OldPos.RemoveAt(0);
            Position = Position + Velocity;
            OldPos.Add(Position);
            if (OldPos.Count > 8)
                OldPos.RemoveAt(0);
            Position += Velocity;
            OldPos.Add(Position);
            if (OldPos.Count > 8)
                OldPos.RemoveAt(0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < OldPos.Count; i++)
            {
                Texture2D texture = LAPTextureRegister.SmallGlowBall.Value;
                spriteBatch.Draw(texture, OldPos[i] - Main.screenPosition, null, DrawColor * Opacity, Rotation, texture.Size() / 2, Scale, SpriteEffects.None, 0);
            }
        }
    }
}
