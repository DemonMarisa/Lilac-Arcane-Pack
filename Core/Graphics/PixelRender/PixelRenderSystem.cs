using LAP.Core.Graphics.DrawNode;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.PixelRender
{
    public class PixelRenderSystem : ModSystem
    {
        public static RenderTarget2D PreparePixelationTarget_AfterDust;
        public static RenderTarget2D PixelationTarget_AfterDust;

        public static RenderTarget2D PreparePixelationTarget_BeforeDust;
        public static RenderTarget2D PixelationTarget_BeforeDust;

        public static bool CurrentlyRendering;
        #region Loading
        public override void Load()
        {
            On_Main.CheckMonoliths += DrawToTargets;

            if (Main.dedServ)
                return;

            Main.QueueMainThreadAction(() =>
            {
                PreparePixelationTarget_AfterDust = new RenderTarget2D(Main.instance.GraphicsDevice, Main.screenWidth, Main.screenHeight);
                PixelationTarget_AfterDust = new RenderTarget2D(Main.instance.GraphicsDevice, Main.screenWidth / 2, Main.screenHeight / 2);

                PreparePixelationTarget_BeforeDust = new RenderTarget2D(Main.instance.GraphicsDevice, Main.screenWidth, Main.screenHeight);
                PixelationTarget_BeforeDust = new RenderTarget2D(Main.instance.GraphicsDevice, Main.screenWidth / 2, Main.screenHeight / 2);
            });
        }

        public override void Unload()
        {
            On_Main.CheckMonoliths -= DrawToTargets;

            if (Main.dedServ)
                return;

            Main.QueueMainThreadAction(() =>
            {
                PreparePixelationTarget_AfterDust.Dispose();
                PixelationTarget_AfterDust.Dispose();
                PreparePixelationTarget_BeforeDust.Dispose();
                PixelationTarget_BeforeDust.Dispose();

                PreparePixelationTarget_AfterDust = null;
                PixelationTarget_AfterDust = null;
                PreparePixelationTarget_BeforeDust = null;
                PixelationTarget_BeforeDust = null;
            });
        }
        #endregion
        #region 进行离屏渲染
        public static void DrawToTargets(On_Main.orig_CheckMonoliths orig)
        {
            if (Main.gameMenu)
            {
                orig();
                return;
            }

            orig();

            CurrentlyRendering = true;

            DrawNodeToRenderTarget();
            Main.instance.GraphicsDevice.SetRenderTarget(null);

            CurrentlyRendering = false;
        }

        #region 渲染dust图层前
        public static void DrawNodeToRenderTarget()
        {
            if (NodeManager.ActivePixelNode.Count == 0)
                return;
            // 这样绘制可以避免来回切换分辨率导致的矩阵变换问题
            // 切换目标到全分辨率
            PreparePixelationTarget_AfterDust.SwapToTarget();

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null);
            for (int i = 0; i < NodeManager.ActivePixelNode.Count; i++)
            {
                if (NodeManager.ActivePixelNode[i].Layer == DrawLayer.AfterDust)
                    NodeManager.ActivePixelNode[i].Draw(Main.spriteBatch);
            }
            Main.spriteBatch.End();

            // 切换目标到全分辨率
            PreparePixelationTarget_BeforeDust.SwapToTarget();

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null);
            for (int i = 0; i < NodeManager.ActivePixelNode.Count; i++)
            {
                if (NodeManager.ActivePixelNode[i].Layer == DrawLayer.BeforeDust)
                    NodeManager.ActivePixelNode[i].Draw(Main.spriteBatch);
            }
            Main.spriteBatch.End();

            // 将全分辨率目标缩小50%画出来
            PixelationTarget_AfterDust.SwapToTarget();

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null);
            Main.spriteBatch.Draw(PreparePixelationTarget_AfterDust, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            Main.spriteBatch.End();

            // 将全分辨率目标缩小50%画出来
            PixelationTarget_BeforeDust.SwapToTarget();

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null);
            Main.spriteBatch.Draw(PreparePixelationTarget_BeforeDust, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
        }
        #endregion
        #endregion
        #region 最终画出
        public static void DrawTarget_Dust(On_Main.orig_DrawDust orig, Main self)
        {
            if (NodeManager.ActivePixelNode.Count != 0)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
                Main.spriteBatch.Draw(PixelationTarget_BeforeDust, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
            }

            orig(self);
                       
            if (NodeManager.ActivePixelNode.Count != 0)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
                Main.spriteBatch.Draw(PixelationTarget_AfterDust, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
            }
        }
        #endregion
    }
}
