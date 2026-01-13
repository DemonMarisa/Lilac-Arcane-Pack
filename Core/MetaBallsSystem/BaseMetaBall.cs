using LAP.Assets.Effects;
using LAP.Assets.TextureRegister;
using LAP.Core.Graphics.RenderTargetsManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.MetaBallsSystem
{
    public abstract class BaseMetaBall : ModType
    {
        public int Type = 0;
        public virtual float BGTimeMult => 1;
        // 这个元球对应的渲染目标
        public int AlphaTextureIndex;
        // 这个元球对应的背景
        public virtual Texture2D BgTexture => LAPTextureRegister.ShadowNebula.Value;

        public virtual RenderTarget2D AlphaTexture => RT2DManager.RT2D_ScreenSize[AlphaTextureIndex];

        /// <summary>
        /// 描边颜色
        /// </summary>
        public virtual Color EdgeColor => Color.White;

        /// <summary>
        /// 是否更新
        /// </summary>
        public virtual bool Active()
        {
            return false;
        }

        /// <summary>
        /// 提供的更新方法
        /// </summary>
        public virtual void Update() { }

        protected sealed override void Register()
        {
            if (!MetaBallManager.MetaBallCollection.Contains(this))
                MetaBallManager.MetaBallCollection.Add(this);

            Type = MetaBallManager.MetaBallCollection.Count;

            if (Main.netMode == NetmodeID.Server)
                return;

            RT2DManager.RequestScreenSizeRT2D(out AlphaTextureIndex); 
        }

        /// <summary>
        /// 提供的绘制方法
        /// </summary>
        public virtual void PrepareRenderTarget()
        {
            Main.spriteBatch.Draw(LAPTextureRegister.WhiteCube.Value, new Vector2(960, 540), null, Color.White, 0, LAPTextureRegister.WhiteCube.Size() / 2, 10, SpriteEffects.None, 0f);
        }

        public virtual bool PreDrawRT2D()
        {
            return true;
        }
        public virtual void PrepareShader()
        {
            Main.graphics.GraphicsDevice.Textures[0] = RT2DManager.RT2D_ScreenSize[AlphaTextureIndex];
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            Main.graphics.GraphicsDevice.Textures[1] = BgTexture;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;

            Effect shader = LAPShaderRegister.MetaballShader.Value;
            shader.Parameters["renderTargetSize"].SetValue(RT2DManager.RT2D_ScreenSize[AlphaTextureIndex].Size());
            shader.Parameters["bakcGroundSize"].SetValue(BgTexture.Size());
            shader.Parameters["edgeColor"].SetValue(EdgeColor.ToVector4());
            shader.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly);
            shader.CurrentTechnique.Passes[0].Apply();
        }
    }
}
