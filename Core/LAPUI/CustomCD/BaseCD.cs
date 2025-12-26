using LAP.Assets.TextureRegister;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LAP.Core.LAPUI.CustomCD
{
    public abstract class BaseCD : ModType
    {
        public bool Buff = true;
        public bool DeBuff = false;
        public bool CD = false;
        public bool Info = false;
        public int Type = 0;
        public int MaxTime = 0;
        public int Time = 0;
        public virtual string TexturePath => (GetType().Namespace + "." + GetType().Name).Replace('.', '/');
        public virtual LocalizedText DisplayName()
        {
           return LocalizedText.Empty;
        }
        public virtual void OnRegister()
        {

        }
        public virtual bool CanTickDown()
        {
            return true;
        }
        public virtual SoundStyle? BeginSound => null;
        public virtual SoundStyle? EndSound => null;
        public virtual bool CleanOnDeath()
        {
            return true;
        }
        public virtual bool PreDrawTime(DynamicSpriteFont MGRFont)
        {
            return true;
        }
        public virtual void OnSpawn(Player player) { }
        public virtual void Update(Player player) { }
        public virtual bool PreDraw()
        {
            return true;
        }
        // 这里这样分是因为做了合批Bloom
        public virtual bool PreDrawBloom()
        {
            return true;
        }
        public virtual void PostDraw()
        {
            Texture2D texture = CustomCDManger.CDTexture[Type].Value;
            Main.spriteBatch.Draw(texture, DrawPosition, null, Color.White, 0f, texture.Size() / 2, 1f, SpriteEffects.None, 0f);
        }
        public virtual void OnComplete(Player player) { }
        protected sealed override void Register()
        {
            Type = CustomCDManger.CDCollection.Count;
            OnRegister();
            if (!CustomCDManger.CDCollection.Contains(this))
                CustomCDManger.CDCollection.Add(this);
            CustomCDManger.CDTexture.Add(ModContent.Request<Texture2D>(TexturePath, AssetRequestMode.ImmediateLoad));
        }
        // 以下是用于绘制的内容
        public float Opactiy;
        public float Scale;
        public int AniProgress;
        public bool MouseHover;
        public bool BeginFadeOut;
        public Vector2 DrawPosition;
        public virtual Rectangle OverLayerRec => new Rectangle(0, 0, CDTexture_OverLayer.Width, 30 + (int)((CDTexture_OverLayer.Height - 30) * (Time / (float)MaxTime)));
        public virtual Texture2D CDTexture => LAPTextureRegister.SingleCD.Value;
        public virtual Texture2D CDTexture_Bloom => LAPTextureRegister.SingleCD_Bloom.Value;
        public virtual Texture2D CDTexture_OverLayer => LAPTextureRegister.SingleCD_OverLayer.Value;
        /// <summary>
        /// 在Player中调用
        /// </summary>
        public virtual void FadeIn()
        {
            if (BeginFadeOut)
            {
                if (AniProgress > 0)
                    AniProgress--;
            }
            else
            {
                if (AniProgress < 30)
                    AniProgress++;
            }
            Opactiy = MathHelper.Lerp(0f, 1f, (float)AniProgress / 30f);
        }
        /// <summary>
        /// 只会在UpdateUI中调用
        /// </summary>
        public virtual void UpdateHover()
        {
            if (MouseHover)
                Scale = MathHelper.Lerp(Scale, 1.1f, 0.2f);
            else
                Scale = MathHelper.Lerp(Scale, 1f, 0.2f);
        }
        public virtual BaseCD Clone()
        {
            return (BaseCD)MemberwiseClone();
        }
    }
}
