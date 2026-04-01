using LAP.Assets.TextureRegister;
using LAP.Core.Enums;
using LAP.Core.ParticleSystem;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LAP.Content.Particles
{
    public class ProjAbsorbGlowBall : BaseParticle
    {
        public override int UseBlendStateID => BlendStateID.Additive;
        public ProjAbsorbGlowBall(Vector2 position, Color color, int lifetime, float scale, float rot, float rotSpeed, int Owner, int Length, Vector2 offset)
        {
            Position = position;
            DrawColor = color;
            Lifetime = lifetime;
            Scale = scale;
            Rotation = rot;
            RotSpeed = rotSpeed;
            OwnerID = Owner;
            DrawLength = Length;
            Offset = offset;
        }
        public int OwnerID;
        public float RotSpeed;
        public float DrawLengthOffset;
        public float DrawLength;
        public Vector2 Offset;
        public Projectile Owner => Main.projectile[OwnerID];
        public override void Update()
        {
            Velocity = Vector2.Zero;
            Position = Owner.Center + Offset.RotatedBy(Owner.rotation);
            Rotation += RotSpeed;
            DrawLengthOffset = MathHelper.Lerp(DrawLength, 0, EasingHelper.EaseOutCubic(LifetimeRatio));
            if (LifetimeRatio < 0.5f)
                Opacity = MathHelper.Lerp(0f, 1f, EasingHelper.EaseOutCubic(LifetimeRatio * 2));
            else
                Opacity = MathHelper.Lerp(2f, 0f, EasingHelper.EaseInCubic(LifetimeRatio));
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = LAPTextureRegister.SmallGlowBall.Value;
            spriteBatch.Draw(texture, Position - Main.screenPosition + new Vector2(DrawLengthOffset, 0).RotatedBy(Rotation), null, Color.White * Opacity, 0, texture.Size() / 2, Scale * 0.5f, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, Position - Main.screenPosition + new Vector2(DrawLengthOffset, 0).RotatedBy(Rotation), null, DrawColor * Opacity, 0, texture.Size() / 2, Scale, SpriteEffects.None, 0);
        }
    }
}
