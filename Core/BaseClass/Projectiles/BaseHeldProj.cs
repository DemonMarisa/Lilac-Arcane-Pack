using LAP.Core.IDSets;
using LAP.Core.SystemsLoader;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace LAP.Core.BaseClass.Projectiles
{
    public abstract class BaseHeldProj : ModProjectile, ILocalizedModType
    {
        public Player Owner => Main.player[Projectile.owner];
        public bool Active => (Owner.channel || Owner.controlUseTile) && !Owner.noItems && !Owner.CCed && !Owner.dead;
        public float DrawRotOffset = 0f;
        public Vector2 DrawPosOffset = Vector2.Zero;
        public int UseDelay = 0;
        public Vector2 RealOwnerCenter => Owner.RotatedRelativePoint(Owner.MountedCenter, true);
        public float RotAmount = 1f;
        public Vector2 PositionOffset = Vector2.Zero;
        public int frameX;
        public int frameY;
        public override void SetStaticDefaults()
        {
            Projectile.AddHeldProj();
            ExSSD();
        }
        public virtual void ExSSD()
        {
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
            ExSD();
        }
        public virtual void ExSD()
        {
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override bool PreAI()
        {
            if (Projectile.LAP().FirstFrame)
            {
                Initialize();
            }
            return ExPreAI();
        }
        public virtual void Initialize()
        {
        }
        public virtual bool ExPreAI()
        {
            return true;
        }
        public override void AI()
        {
            if (!Active)
                Projectile.Kill();
            if (UseDelay > 0)
                UseDelay--;
            SetPlayerVisuals();
            AimToMouse();
            ExAI();
        }
        public virtual void ExAI()
        {

        }
        public virtual void SetPlayerVisuals()
        {
            Projectile.SetHeldProj(Owner);
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        public virtual void AimToMouse()
        {
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.UnitX);
            Vector2 target = LAPUtilities.GetVector2(Projectile.Center , Owner.LocalMouseWorld());
            Projectile.velocity = Vector2.Lerp(Projectile.velocity, target, RotAmount);
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.Center = RealOwnerCenter + PositionOffset;
        }
        public override void PostAI()
        {
            Projectile.extraUpdates = 0;
            ExPostAI();
        }
        public virtual void ExPostAI()
        {

        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;
            float drawRotation = Projectile.rotation + (Projectile.spriteDirection == -1 ? MathHelper.Pi : 0f);
            Vector2 rotationPoint = texture.Size() / 2;
            SpriteEffects flipSprite = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if (Main.projFrames[Projectile.type] == 1)
            {
                Main.spriteBatch.Draw(texture, drawPosition + DrawPosOffset, null, lightColor, drawRotation + DrawRotOffset, rotationPoint, Projectile.scale, flipSprite, 0);
            }
            else
            {
                LAPIDSet.ProjFrame.TryGetValue(Projectile.type, out Point frame);
                Rectangle rec = texture.Frame(frame.X, frame.Y, frameX, frameY);
                rotationPoint = rec.Size() / 2;
                Main.spriteBatch.Draw(texture, drawPosition + DrawPosOffset, rec, lightColor, drawRotation + DrawRotOffset, rotationPoint, Projectile.scale, flipSprite, 0);
            }
            return false;
        }
    }
}
