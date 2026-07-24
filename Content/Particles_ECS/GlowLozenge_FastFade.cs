using LAP.Assets.TextureRegister;
using LAP.Core.ParticleSystem_ECS;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LAP.Content.Particles_ECS
{
    public class GlowLozenge_FastFade : ParticleBehaviors
    {
        public override void OnSpawn(ref ParticleData data)
        {
            data.aifloat0 = data.Scale;
            data.Rotation = data.Velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void Update(ref ParticleData data)
        {
            data.Scale = MathHelper.Lerp(data.aifloat0, 0f, EasingHelper.EaseOutCubic(data.LifetimeRatio));
            data.Velocity *= 1.05f;
        }
        public override void Draw(ref ParticleData data)
        {
            Texture2D texture = LAPTextureRegister.Lozenge_Glow.Value;
            Main.spriteBatch.Draw(texture, data.Position - Main.screenPosition, null, data.DrawColor, data.Rotation, texture.Size() / 2,new Vector2(data.Scale2.X * data.Scale, data.Scale2.Y), SpriteEffects.None, 0);
        }
    }
}
