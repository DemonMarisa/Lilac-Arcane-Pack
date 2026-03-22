using LAP.Assets.TextureRegister;
using LAP.Core.MetaBallsSystem;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Threading;
using System.Collections.Generic;
using Terraria;

namespace LAP.Content.MetaBalls
{
    public class CircleParticle(Vector2 center, Vector2 velocity, float scale)
    {
        public float Scale = scale;
        public float BeginScale = scale;
        public Vector2 Velocity = velocity;
        public Vector2 Center = center;
        public void Update()
        {
            Center += Velocity;
            Velocity *= 0.9f;
            Scale *= 0.96f;
        }
    }
    public class NebulaMetaBall : BaseMetaBall
    {
        public static List<CircleParticle> Particles = [];
        public override Texture2D BgTexture => LAPTextureRegister.ShadowNebula.Value;
        public override Color EdgeColor => Color.DarkViolet;
        public static void SpawnParticle(Vector2 position, Vector2 velocity, float size) => Particles.Add(new(position, velocity, size));
        public override bool Active()
        {
            if (Particles.Count == 0)
                return false;
            else
                return true;
        }
        public override void Update()
        {
            FastParallel.For(0, Particles.Count, (j, k, callback) =>
            {
                for (int i = j; i < k; i++)
                {
                    Particles[i].Update();
                }
            });
            Particles.RemoveAll(particle =>
            {
                if (particle.Scale < 0.01f)
                {
                    return true;
                }
                return false;
            });
        }
        public override void PrepareRenderTarget()
        {
            if (Particles.Count != 0)
            {
                for (int i = 0; i < Particles.Count; i++)
                {
                    Main.spriteBatch.Draw(LAPTextureRegister.WhiteCircle.Value, Particles[i].Center - Main.screenPosition, null, Color.White, 0, LAPTextureRegister.WhiteCircle.Size() / 2, Particles[i].Scale, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
