using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using LAP.Core.ParticleSystem;
using LAP.Assets.TextureRegister;
using LAP.Core.Enums;

namespace LAP.Content.Particles
{
    public class Fire : BaseParticle
    {
        public override int UseBlendStateID => BlendStateID.Additive;
        public Fire(Vector2 position, Vector2 velocity, Color color, int lifetime, float Rot, float opacity, float scale)
        {
            Position = position;
            Velocity = velocity;
            DrawColor = color;
            Lifetime = lifetime;
            Rotation = Rot;
            Opacity = opacity;
            Scale = scale;
        }
        public override void OnSpawn()
        {
        }

        public override void Update()
        {
            Velocity *= 0.9f;
            Opacity = MathHelper.Lerp(Opacity, MathHelper.Lerp(Opacity, 0, 0.3f), 0.12f);
        }
        // 这里采样没有问题，他贴图就是这样
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = LAPTextureRegister.Fire.Value;
            Rectangle frame = texture.Frame(8, 8, (int)(LifetimeRatio * 64) % 8, (int)(LifetimeRatio * 8));
            Vector2 origin = frame.Size() * 0.5f;
            spriteBatch.Draw(texture, Position - Main.screenPosition, frame, DrawColor * Opacity, Rotation, origin, Scale, 0, 0f);
        }
    }
}
