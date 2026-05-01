using LAP.Assets.TextureRegister;
using LAP.Core.ParticleSystem_ECS;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LAP.Content.Particles_ECS
{
    public class GlowLozenge : ParticleBehaviors
    {
        public override void OnSpawn(ref ParticleData data)
        {
            data.aifloat0 = data.Scale;
            data.Rotation = data.Velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void Update(ref ParticleData data)
        {
            data.Scale = MathHelper.Lerp(data.aifloat0, 0f, EasingHelper.EaseOutCubic(data.LifetimeRatio));
            data.Velocity *= 0.9f;
        }
        public override void Draw(ref ParticleData data)
        {
            Texture2D texture = LAPTextureRegister.Lozenge_Glow.Value;
            Main.spriteBatch.Draw(texture, data.Position - Main.screenPosition, null, data.DrawColor, data.Rotation, texture.Size() / 2, data.Scale, SpriteEffects.None, 0);
        }
    }
}
