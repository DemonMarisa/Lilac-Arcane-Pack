using LAP.Assets.TextureRegister;
using LAP.Core.ParticleSystem;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LAP.Content.Particles
{
    public class Slash : BaseParticle
    {
        public Vector2 BeginScale;
        public override int UseBlendStateID => BlendStateID.NonPremult;
        public Slash(Vector2 Pos, float rot, Color drawColor, int life, Vector2 scale)
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
            Texture2D texture = LAPTextureRegister.RoughenEdgesLine.Value;
            spriteBatch.Draw(texture, Position - Main.screenPosition, null, DrawColor, Rotation, texture.Size() / 2, new Vector2(BeginScale.X, BeginScale.Y * Scale), SpriteEffects.None, 0);
            spriteBatch.Draw(texture, Position - Main.screenPosition, null, DrawColor, Rotation, texture.Size() / 2, new Vector2(BeginScale.X, BeginScale.Y * Scale), SpriteEffects.FlipHorizontally, 0);
        }
    }
}
