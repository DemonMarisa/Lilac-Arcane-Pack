using LAP.Assets.TextureRegister;
using LAP.Core.ParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using LAP.Core.Enums;

namespace LAP.Content.Particles
{
    public class CampSmoke : BaseParticle
    {
        public override int UseBlendStateID =>  BlendStateID.Additive;
        public CampSmoke(Vector2 position, Vector2 velocity, Color color, int lifetime, float Rot, float opacity, float scale)
        {
            Position = position;
            Velocity = velocity;
            DrawColor = color;
            Lifetime = lifetime;
            Rotation = Rot;
            Opacity = opacity;
            Scale = scale;
        }
        public float RotOffset = 0;
        public override string Texture => LAPTextureRegister.CampSmoke.Path;
        public override void OnSpawn()
        {
            RotOffset = Main.rand.NextFloat(-0.15f, 0.15f);
        }
        public override void Update()
        {
            Velocity *= 0.9f;
            Opacity = MathHelper.Lerp(Opacity, MathHelper.Lerp(Opacity, 0, 0.3f), 0.12f);
            Rotation += RotOffset;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            float brightness = (float)Math.Pow(Lighting.Brightness((int)(Position.X / 16f), (int)(Position.Y / 16f)), 0.15);
            Texture2D texture = LAPTextureRegister.CampSmoke.Value;
            Vector2 origin = texture.Size() * 0.5f;
            spriteBatch.Draw(texture, Position - Main.screenPosition, null, DrawColor * brightness * Opacity, Rotation, origin, Scale, 0, 0f);
        }
    }
}
