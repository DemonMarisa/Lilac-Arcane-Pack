using LAP.Assets.TextureRegister;
using LAP.Core.ParticleSystem;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LAP.Content.Particles
{
    public class StarLine : BaseParticle
    {
        public int TextureStyle;
        public float rotSpeed;
        public float BeginScale;
        public override int UseBlendStateID => BlendStateID.Additive;
        public StarLine(Vector2 position, float rot, Color color, int lifetime, float scale, float opacity)
        {
            Position = position;
            Rotation = rot;
            DrawColor = color;
            Lifetime = lifetime;
            BeginScale = scale;
            Scale = scale;
            Opacity = opacity;
        }
        public override void OnSpawn()
        {
            TextureStyle = Main.rand.Next(0, 3);
            rotSpeed = Main.rand.NextBool() ? -0.005f : 0.005f;
        }
        public override void Update()
        {
            Rotation += rotSpeed;
            if (LifetimeRatio < 0.5f)
            {
                Scale = MathHelper.Lerp(0f, BeginScale, EasingHelper.EaseOutCubic(LifetimeRatio * 2));
            }
            else
            {
                float progress = LifetimeRatio - 0.5f;
                Scale = MathHelper.Lerp(BeginScale, 0f, EasingHelper.EaseInCubic(progress * 2));
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (TextureStyle == 0)
            {
                Texture2D texture = LAPTextureRegister.StarLine1.Value;
                spriteBatch.Draw(texture, Position - Main.screenPosition, null, DrawColor * Opacity, Rotation, texture.Size() / 2, Scale, SpriteEffects.None, 0);
            }
            if (TextureStyle == 1)
            {
                Texture2D texture = LAPTextureRegister.StarLine2.Value;
                spriteBatch.Draw(texture, Position - Main.screenPosition, null, DrawColor * Opacity, Rotation, texture.Size() / 2, Scale, SpriteEffects.None, 0);
            }
            if (TextureStyle == 2)
            {
                Texture2D texture = LAPTextureRegister.StarLine3.Value;
                spriteBatch.Draw(texture, Position - Main.screenPosition, null, DrawColor * Opacity, Rotation, texture.Size() / 2, Scale, SpriteEffects.None, 0);
            }
        }
    }
}
