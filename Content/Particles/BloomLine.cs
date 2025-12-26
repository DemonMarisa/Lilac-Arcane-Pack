using LAP.Assets.TextureRegister;
using LAP.Core.ParticleSystem;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LAP.Content.Particles
{
    public class BloomLine : BaseParticle
    {
        public Vector2 BeginScale;
        public override int UseBlendStateID => BlendStateID.Additive;
        public BloomLine(Vector2 Pos, float rot, Color drawColor, int life, Vector2 scale)
        {
            Position = Pos;
            DrawColor = drawColor;
            Lifetime = life;
            BeginScale = scale;
            Rotation = rot;
        }
        public override void Update()
        {
            if (LifetimeRatio < 0.5f)
            {
                Scale = MathHelper.Lerp(0, 1f, EasingHelper.EaseOutCubic(LifetimeRatio * 2));
            }
            else
            {
                float prog = LifetimeRatio - 0.5f;
                Scale = MathHelper.Lerp(1f, 0, EasingHelper.EaseInCubic(prog * 2));
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = LAPTextureRegister.BloomLine.Value;
            spriteBatch.Draw(texture, Position - Main.screenPosition, null, DrawColor * Opacity, Rotation, texture.Size() / 2, new Vector2(BeginScale.X, BeginScale.Y * Scale), SpriteEffects.None, 0);
        }
    }
}
