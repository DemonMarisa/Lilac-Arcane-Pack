using LAP.Assets.TextureRegister;
using LAP.Core.MetaBallsSystem;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;

namespace LAP.Content.MetaBalls
{
    public class CircleParticle(Vector2 center, Vector2 velocity, float scale, int maxTime)
    {
        public float Scale = scale;
        public float BeginScale = scale;
        public Vector2 Velocity = velocity;
        public Vector2 Center = center;
        public int Time;
        public int MaxTime = maxTime;
        public void Update()
        {
            Time++;
            Center += Velocity;
            Velocity *= 0.9f;
            Scale = MathHelper.Lerp(BeginScale, 0f, EasingHelper.EaseOutCubic(Time / (float)MaxTime));
        }
    }
    public class NebulaMetaBall : BaseMetaBall
    {
        public static List<CircleParticle> LozengeParticles = [];
        public override Texture2D BgTexture => LAPTextureRegister.ShadowNebula.Value;
        public override Color EdgeColor => Color.DarkViolet;
        public static void SpawnLozengeParticle(Vector2 position, Vector2 velocity, float size, int maxTime) => LozengeParticles.Add(new(position, velocity, size, maxTime));
        public override bool Active()
        {
            if (LozengeParticles.Count == 0)
                return false;
            else
                return true;
        }
        public override void Update()
        {
            for (int i = 0; i < LozengeParticles.Count; i++)
            {
                LozengeParticles[i].Update();
                if (LozengeParticles[i].Time >= LozengeParticles[i].MaxTime)
                    LozengeParticles.RemoveAt(i);
            }
        }
        public override void PrepareRenderTarget()
        {
            if (LozengeParticles.Count != 0)
            {
                for (int i = 0; i < LozengeParticles.Count; i++)
                {
                    Main.spriteBatch.Draw(LAPTextureRegister.WhiteCircle.Value, LozengeParticles[i].Center - Main.screenPosition, null, Color.White, 0, LAPTextureRegister.WhiteCircle.Size() / 2, LozengeParticles[i].Scale, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
