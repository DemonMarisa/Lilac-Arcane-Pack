using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using LAP.Core.ParticleSystem;
using LAP.Core.Utilities;
using LAP.Core.Enums;
using LAP.Assets.TextureRegister;

namespace LAP.Content.Particles
{
    public class CrossGlow : BaseParticle
    {
        public override int UseBlendStateID => BlendStateID.Additive;
        public float BeginScale;
        public SpriteEffects se = SpriteEffects.None;
        public bool UseFadeIn = true;
        public CrossGlow(Vector2 position, Vector2 velocity, Color color, int lifetime, float opacity, float scale)
        {
            Position = position;
            Velocity = velocity;
            DrawColor = color;
            Lifetime = lifetime;
            Opacity = opacity;
            Scale = scale;
            BeginScale = scale;
        }
        public CrossGlow(Vector2 position, Vector2 velocity, Color color, int lifetime, float opacity, float scale, bool useFadeIn)
        {
            Position = position;
            Velocity = velocity;
            DrawColor = color;
            Lifetime = lifetime;
            Opacity = opacity;
            Scale = scale;
            BeginScale = scale;
            UseFadeIn = useFadeIn;
        }
        public override void OnSpawn()
        {
            if (Main.rand.NextBool())
                se = SpriteEffects.FlipHorizontally;

            if (UseFadeIn)
                Scale = BeginScale;
        }
        public override void Update()
        {
            if (LifetimeRatio < 0.5f)
            {
                if (UseFadeIn)
                    Scale = MathHelper.Lerp(0f, BeginScale, EasingHelper.EaseOutCubic(LifetimeRatio * 2));
            }
            else
            {
                float progress = LifetimeRatio - 0.5f;
                Scale = MathHelper.Lerp(BeginScale, 0f, EasingHelper.EaseOutCubic(progress * 2));
            }
        }
        // 这里采样没有问题，他贴图就是这样
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = LAPTextureRegister.CrossGlow.Value;
            spriteBatch.Draw(texture, Position - Main.screenPosition, null, DrawColor * Opacity, Rotation, texture.Size() / 2, Scale, se, 0f);
        }
    }
}
