using LAP.Assets.TextureRegister;
using LAP.Core.ParticleSystem;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;

namespace LAP.Content.Particles
{
    public class TrailGlowBall : BaseParticle
    {
        public override int UseBlendStateID => BlendStateID.Additive;
        public float BeginScale;
        public List<Vector2> OldPos = [];
        public bool UseSlowDown = false;
        public TrailGlowBall(Vector2 position, Vector2 velocity, Color color, int lifetime, float scale)
        {
            Position = position;
            Velocity = velocity;
            DrawColor = color;
            Lifetime = lifetime;
            Scale = scale;
            BeginScale = scale;
        }
        public TrailGlowBall(Vector2 position, Vector2 velocity, Color color, int lifetime, float scale, bool useSlowDown)
        {
            Position = position;
            Velocity = velocity;
            DrawColor = color;
            Lifetime = lifetime;
            Scale = scale;
            BeginScale = scale;
            UseSlowDown = useSlowDown;
        }
        public override void Update()
        {
            Scale = MathHelper.Lerp(BeginScale, 0f, EasingHelper.EaseOutCubic(LifetimeRatio));

            OldPos.Add(Position);
            if (OldPos.Count > 8)
                OldPos.RemoveAt(0);
            Position = Position + Velocity;
            OldPos.Add(Position);
            if (OldPos.Count > 8)
                OldPos.RemoveAt(0);
            Position = Position + Velocity;
            OldPos.Add(Position);
            if (OldPos.Count > 8)
                OldPos.RemoveAt(0);
            if (UseSlowDown)
                Velocity *= 0.9f;
            else
                Velocity *= 1.03f;
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
