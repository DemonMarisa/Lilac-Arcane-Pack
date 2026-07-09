using LAP.Assets.TextureRegister;
using LAP.Core.Enums;
using LAP.Core.Graphics.DeepGlow;
using LAP.Core.ParticleSystem;
using LAP.Core.ParticleSystem_ECS;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;

namespace LAP.Content.Particles_ECS
{
    public class Lightning01 : ParticleBehaviors
    {
        public override void OnSpawn(ref ParticleData particle)
        {
            particle.aiint0 = Main.rand.Next(0, 4);
            particle.aiint1 = Main.rand.Next(0, 2);
        }
        public override void Update(ref ParticleData particle)
        {
            particle.Opacity = MathHelper.Lerp(1f, 0f, EasingHelper.EaseOutCubic(particle.LifetimeRatio));
            particle.Opacity = MathF.Pow(particle.Opacity, 0.5f);
        }
        public override void Draw(ref ParticleData particle)
        {
            Texture2D texture = LAPTextureRegister.Lightning01.Value;
            Rectangle frame = texture.Frame(4, 2, particle.aiint0, particle.aiint1);
            Vector2 origin = frame.Size() * 0.5f;
            Main.spriteBatch.Draw(texture, particle.Position - Main.screenPosition, frame, Color.White * particle.Opacity, particle.Rotation, origin, particle.Scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture, particle.Position - Main.screenPosition, frame, particle.DrawColor * particle.Opacity, particle.Rotation, origin, particle.Scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture, particle.Position - Main.screenPosition, frame, particle.DrawColor * particle.Opacity, particle.Rotation, origin, particle.Scale, SpriteEffects.None, 0f);
            if (particle.aibool0)
            {
                ParticleData data = particle;
                DeepGlow.SubmitCustomGlow(() =>
                {
                    Main.spriteBatch.Draw(texture, data.Position - Main.screenPosition, frame, Color.White * data.Opacity, data.Rotation, origin, data.Scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(texture, data.Position - Main.screenPosition, frame, data.DrawColor * data.Opacity, data.Rotation, origin, data.Scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(texture, data.Position - Main.screenPosition, frame, data.DrawColor * data.Opacity, data.Rotation, origin, data.Scale, SpriteEffects.None, 0f);
                });
            }
        }
    }
    public class Lightning02 : ParticleBehaviors
    {
        public override void Update(ref ParticleData particle)
        {
            particle.Opacity = MathHelper.Lerp(1f, 0f, EasingHelper.EaseOutCubic(particle.LifetimeRatio));
        }
        public override void Draw(ref ParticleData particle)
        {
            Texture2D texture = LAPTextureRegister.Lightning02.Value;
            Main.spriteBatch.Draw(texture, particle.Position - Main.screenPosition, null, particle.DrawColor * particle.Opacity, particle.Rotation, texture.Size() / 2, particle.Scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture, particle.Position - Main.screenPosition, null, particle.DrawColor * particle.Opacity, particle.Rotation, texture.Size() / 2, particle.Scale, SpriteEffects.None, 0f);
            if (particle.aibool0)
            {
                ParticleData data = particle;
                DeepGlow.SubmitCustomGlow(() =>
                {
                    Main.spriteBatch.Draw(texture, data.Position - Main.screenPosition, null, data.DrawColor * data.Opacity, data.Rotation, texture.Size() / 2, data.Scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(texture, data.Position - Main.screenPosition, null, data.DrawColor * data.Opacity, data.Rotation, texture.Size() / 2, data.Scale, SpriteEffects.None, 0f);
                });
            }
        }
    }
    public class Lightning03 : ParticleBehaviors
    {
        public override void OnSpawn(ref ParticleData particle)
        {
            particle.aiint0 = Main.rand.Next(0, 2);
            particle.aiint1 = Main.rand.Next(0, 2);
        }
        public override void Update(ref ParticleData particle)
        {
            particle.Opacity = MathHelper.Lerp(1f, 0f, EasingHelper.EaseInCubic(particle.LifetimeRatio));
        }
        public override void Draw(ref ParticleData particle)
        {
            Texture2D texture = LAPTextureRegister.Lightning03.Value;
            Rectangle frame = texture.Frame(2, 2, particle.aiint0, particle.aiint1);
            Vector2 origin = frame.Size() * 0.5f;
            Main.spriteBatch.Draw(texture, particle.Position - Main.screenPosition, frame, Color.White * particle.Opacity, particle.Rotation, origin, particle.Scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture, particle.Position - Main.screenPosition, frame, particle.DrawColor * particle.Opacity, particle.Rotation, origin, particle.Scale, SpriteEffects.None, 0f);
            if (particle.aibool0)
            {
                ParticleData data = particle;
                DeepGlow.SubmitCustomGlow(() =>
                {
                    Main.spriteBatch.Draw(texture, data.Position - Main.screenPosition, frame, data.DrawColor * data.Opacity, data.Rotation, origin, data.Scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(texture, data.Position - Main.screenPosition, frame, data.DrawColor * data.Opacity, data.Rotation, origin, data.Scale, SpriteEffects.None, 0f);
                });
            }
        }
    }
}
