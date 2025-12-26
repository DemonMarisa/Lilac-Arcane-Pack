using LAP.Assets.TextureRegister;
using LAP.Core.ParticleSystem;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LAP.Content.Particles
{
    public class PointLight : BaseParticle
    {
        public float BeginScale;
        public override int UseBlendStateID => BlendStateID.Additive;
        public PointLight(Vector2 Pos, Vector2 vel, Color drawColor, int life, float scale)
        {
            Position = Pos;
            Velocity = vel;
            DrawColor = drawColor;
            Lifetime = life;
            BeginScale = scale;
        }
        public override void Update()
        {
            Velocity *= 0.8f;
            Scale = MathHelper.Lerp(BeginScale, 0, EasingHelper.EaseInCubic(LifetimeRatio));
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = LAPTextureRegister.PointLight.Value;
            spriteBatch.Draw(texture, Position - Main.screenPosition, null, DrawColor * Opacity, Rotation, texture.Size() / 2, Scale, SpriteEffects.None, 0);
        }
    }
}
