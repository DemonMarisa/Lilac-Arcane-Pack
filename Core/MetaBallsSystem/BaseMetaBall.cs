using LAP.Assets.Effects;
using LAP.Assets.TextureRegister;
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

        // 这个元球对应的渲染目标
        public RenderTarget2D AlphaTexture;
        // 这个元球对应的背景
        public virtual Texture2D BgTexture => LAPTextureRegister.ShadowNebula.Value;

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
            // Store this metaball instance in the personalized manager so that it can be kept track of for rendering purposes.
            if (!MetaBallManager.MetaBallCollection.Contains(this))
                MetaBallManager.MetaBallCollection.Add(this);

            Type = MetaBallManager.MetaBallCollection.Count;

            // Disallow render target creation on servers.
            if (Main.netMode == NetmodeID.Server)
                return;

            // Generate render targets.
            Main.QueueMainThreadAction(() =>
            {
                AlphaTexture?.Dispose();
                AlphaTexture = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
            });
        }

        /// <summary>
        /// 提供的绘制方法
        /// </summary>
        public virtual void PrepareRenderTarget()
        {
            Main.spriteBatch.Draw(LAPTextureRegister.WhiteCube.Value, new Vector2(960, 540), null, Color.White, 0, LAPTextureRegister.WhiteCube.Size() / 2, 10, SpriteEffects.None, 0f);
        }

        public virtual void PrepareShader()
        {

            Main.graphics.GraphicsDevice.Textures[0] = AlphaTexture;
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

            Main.graphics.GraphicsDevice.Textures[1] = BgTexture;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointClamp;

            LAPShaderRegister.MetaballShader.Parameters["renderTargetSize"].SetValue(AlphaTexture.Size());
            LAPShaderRegister.MetaballShader.Parameters["bakcGroundSize"].SetValue(BgTexture.Size());
            LAPShaderRegister.MetaballShader.Parameters["edgeColor"].SetValue(EdgeColor.ToVector4());
            LAPShaderRegister.MetaballShader.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly);

            LAPShaderRegister.MetaballShader.CurrentTechnique.Passes[0].Apply();

        }
    }
}
