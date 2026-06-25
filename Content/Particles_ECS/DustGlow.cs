using LAP.Assets.TextureRegister;
using LAP.Core.ParticleSystem_ECS;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LAP.Content.Particles_ECS
{
    public class DustGlow : ParticleBehaviors
    {
        public override void OnSpawn(ref ParticleData data)
        {
            data.aifloat0 = data.Scale;
        }
        public override void Update(ref ParticleData data)
        {
            data.Scale = MathHelper.Lerp(data.aifloat0, 0f, EasingHelper.EaseInCubic(data.LifetimeRatio));
            data.Velocity *= 0.9f;
            data.aifloat1 *= 0.9f;
            data.Velocity = data.Velocity.RotatedBy(data.aifloat1);
        }
        public override void Draw(ref ParticleData data)
        {
            Texture2D texture = LAPTextureRegister.DustGlow.Value;
            Main.spriteBatch.Draw(texture, data.Position - Main.screenPosition, null, data.DrawColor, data.Rotation, texture.Size() / 2, data.Scale, SpriteEffects.None, 0);
            Main.spriteBatch.Draw(texture, data.Position - Main.screenPosition, null, data.DrawColor, data.Rotation, texture.Size() / 2, data.Scale, SpriteEffects.None, 0);
        }
    }
}
