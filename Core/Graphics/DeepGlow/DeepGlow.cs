using LAP.Assets.Effects;
using LAP.Core.Graphics.RenderTargetsManager;
using LAP.Core.MiscDate;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.DeepGlow
{
    public class DeepGlow : ModSystem
    {
        public static Effect GlowEffect => LAPShaderRegister.DeepGlow.Value;
        // 降采样和升采样的渲染目标缓存
        public static RenderTarget2D HightLightTarget;
        public static RenderTarget2D[] _downTargets;
        public static RenderTarget2D[] _upTargets;
        // 迭代次数，控制光晕的广度
        public static int Iterations = 5;
        public static float Threshold = 0.8f;
        public static float Intensity = 2f;
        public static float BlurRadius = 1f;
        public static Color DrawColor = Color.White;
        public static float SoftKnee = 0.25f;
        public static Queue<Action> GlowRequests = new Queue<Action>();
        // 传进来发光绘制逻辑
        public static void SubmitCustomGlow(Action drawAction)
        {
            if (Main.dedServ || drawAction == null) 
                return;
            GlowRequests.Enqueue(drawAction);
        }
        public override void Load()
        {
            if (Main.dedServ)
                return;
            BuildRenderTargets();
            On_FilterManager.EndCapture += DrawDeepGlow;
        }
        public override void Unload()
        {
            if (Main.dedServ)
                return;
            Main.QueueMainThreadAction(() =>
            {
                HightLightTarget.Dispose();
                for (int i = 0; i < _downTargets.Length; i++)
                {
                    _downTargets[i].Dispose();
                }
                for (int i = 0; i < _upTargets.Length; i++)
                {
                    _upTargets[i].Dispose();
                }
            });
            On_FilterManager.EndCapture -= DrawDeepGlow;
            GlowRequests.Clear();
        }
        #region 创建RT2D
        public static void BuildRenderTargets()
        {
            Main.QueueMainThreadAction(() =>
            {
                float width = Main.screenWidth;
                float height = Main.screenHeight;
                _downTargets = new RenderTarget2D[Iterations];
                HightLightTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, (int)width, (int)height);
                for (int i = 0; i < Iterations; i++)
                {
                    // 每迭代一次，分辨率减半
                    width = Math.Max(1, width / 2);
                    height = Math.Max(1, height / 2);
                    // 由于是做平滑模糊，SurfaceFormat.Color 即可，不需要深度缓冲
                    _downTargets[i] = new RenderTarget2D(Main.graphics.GraphicsDevice, (int)width, (int)height, false, SurfaceFormat.Color, DepthFormat.None);
                }
            });
            Main.QueueMainThreadAction(() =>
            {
                float width = Main.screenWidth;
                float height = Main.screenHeight;
                _upTargets = new RenderTarget2D[Iterations - 1];
                // 反向遍历的同时还比迭代次数少一个元素
                for (int i = Iterations - 2; i >= 0; i--)
                {
                    width = Math.Max(1, width / 2);
                    height = Math.Max(1, height / 2);
                    _upTargets[i] = new RenderTarget2D(Main.graphics.GraphicsDevice, (int)width, (int)height, false, SurfaceFormat.Color, DepthFormat.None);
                }
            });
        }
        #endregion
        // 保持RT2D
        public override void UpdateUI(GameTime gameTime)
        {
            if (Main.dedServ)
                return;
            if (RT2DManager.OldScreenSize != LAPInfo.ScreenSize)
                BuildRenderTargets();
        }
        #region 绘制DeepGlow
        public static void DrawDeepGlow(On_FilterManager.orig_EndCapture orig, FilterManager self, RenderTarget2D finalTexture, RenderTarget2D screenTarget, RenderTarget2D screenTargetSwap, Color clearColor)
        {
            if (Main.dedServ)
            {
                orig(self, finalTexture, screenTarget, screenTargetSwap, clearColor);
                return;
            }
            if (GlowRequests.Count == 0 || _downTargets == null || HightLightTarget == null || GlowEffect == null || Iterations < 2)
            {
                GlowRequests.Clear();
                orig(self, finalTexture, screenTarget, screenTargetSwap, clearColor);
                return;
            }
            // 设置初始着色器参数
            GlowEffect.Parameters["uThreshold"].SetValue(Threshold); // 阈值
            GlowEffect.Parameters["uIntensity"].SetValue(Intensity); // 泛光强度
            GlowEffect.Parameters["uBlurRadius"].SetValue(BlurRadius); // 模糊半径
            GlowEffect.Parameters["uSoftKnee"].SetValue(SoftKnee); // 软膝参数
            // 记录原始画面到 screenTargetSwap
            SaveScreenTarget(screenTarget, screenTargetSwap);
            // 获取高亮部分
            // CatchHighLight(screenTarget);
            // 绘制所有自定义光源到高亮图
            DrawCustomGlowsToHighlight();
            // 逐级降采样
            DownSampler();
            // 升采样+混合
            // 从最小的图开始往上叠
            UpSampler();
            // 将最终结果画回目标
            FinalDraw(screenTarget, screenTargetSwap);
            Main.instance.GraphicsDevice.SetRenderTarget(null);
            orig(self, finalTexture, screenTarget, screenTargetSwap, clearColor);
        }
        #endregion
        public static void SaveScreenTarget(RenderTarget2D screenTarget, RenderTarget2D screenTargetSwap)
        {
            screenTargetSwap.SwapToTarget();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);
            Main.spriteBatch.Draw(screenTarget, Vector2.Zero, Color.White);
            Main.spriteBatch.End();
        }
        // 处理所有自定义光源的方法
        public static void DrawCustomGlowsToHighlight()
        {
            if (GlowRequests.Count == 0)
                return;
            HightLightTarget.SwapToTarget();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            while (GlowRequests.Count > 0)
            {
                Action drawAction = GlowRequests.Dequeue();
                drawAction.Invoke();
            }
            Main.spriteBatch.End();
        }
        //public static void CatchHighLight(RenderTarget2D screenTarget, float Threshold = 0.5f)
        //{
        //    HightLightTarget.SwapToTarget();
        //    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);
        //    GlowEffect.CurrentTechnique.Passes["Prefilter2"].Apply();
        //    Main.spriteBatch.Draw(screenTarget, Vector2.Zero, Color.White);
        //    Main.spriteBatch.End();
        //}
        public static void DownSampler()
        {
            for (int i = 0; i < Iterations; i++)
            {
                _downTargets[i].SwapToTarget();
                // 更新像素尺寸且降采样的同时模糊一次，得到更加平滑的效果，防止后续升采样时出现块状失真
                if (i == 0)
                {
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);
                    // 以半分辨率为单位的像素尺寸
                    GlowEffect.Parameters["uTexelSize"].SetValue(new Vector2(1f / _downTargets[i].Width, 1f / _downTargets[i].Height));
                    GlowEffect.CurrentTechnique.Passes["Downsample"].Apply();
                    Main.spriteBatch.Draw(HightLightTarget, new Rectangle(0, 0, _downTargets[i].Width, _downTargets[i].Height), Color.White);
                    Main.spriteBatch.End();
                }
                else
                {
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);
                    GlowEffect.Parameters["uTexelSize"].SetValue(new Vector2(1f / _downTargets[i].Width, 1f / _downTargets[i].Height));
                    GlowEffect.CurrentTechnique.Passes["Downsample"].Apply();
                    Main.spriteBatch.Draw(_downTargets[i - 1], new Rectangle(0, 0, _downTargets[i].Width, _downTargets[i].Height), Color.White);
                    Main.spriteBatch.End();
                }
            }
        }
        public static void UpSampler()
        {
            // 从最小的图开始往上叠，开始为最小的降采样图
            RenderTarget2D currentSource = _downTargets[Iterations - 1];
            RenderTarget2D currentSource2;
            Effect effect = GlowEffect;
            for (int i = 0; i < Iterations - 1; i++)
            {
                currentSource2 = _downTargets[Iterations - 2 - i];
                if (i == 0)
                {
                    _upTargets[i].SwapToTarget();
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);
                    effect.Parameters["uTexelSize"].SetValue(new Vector2(1f / currentSource.Width, 1f / currentSource.Height));
                    effect.CurrentTechnique.Passes["Upsample"].Apply();
                    Main.spriteBatch.Draw(currentSource, new Rectangle(0, 0, _upTargets[i].Width, _upTargets[i].Height), Color.White);
                    effect.Parameters["uTexelSize"].SetValue(new Vector2(1f / _upTargets[i].Width, 1f / _upTargets[i].Height));
                    effect.CurrentTechnique.Passes["Upsample"].Apply();
                    Main.spriteBatch.Draw(currentSource2, new Rectangle(0, 0, _upTargets[i].Width, _upTargets[i].Height), Color.White);
                    Main.spriteBatch.End();
                }
                else
                {
                    _upTargets[i].SwapToTarget();
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);
                    effect.Parameters["uTexelSize"].SetValue(new Vector2(1f / currentSource.Width, 1f / currentSource.Height));
                    effect.CurrentTechnique.Passes["Upsample"].Apply();
                    Main.spriteBatch.Draw(currentSource, new Rectangle(0, 0, _upTargets[i].Width, _upTargets[i].Height), Color.White);
                    effect.Parameters["uTexelSize"].SetValue(new Vector2(1f / _upTargets[i].Width, 1f / _upTargets[i].Height));
                    effect.CurrentTechnique.Passes["Upsample"].Apply();
                    Main.spriteBatch.Draw(currentSource2, new Rectangle(0, 0, _upTargets[i].Width, _upTargets[i].Height), Color.White);
                    Main.spriteBatch.End();
                }
                currentSource = _upTargets[i];
            }
        }
        public static void FinalDraw(RenderTarget2D screenTarget, RenderTarget2D screenTargetSwap)
        {
            screenTarget.SwapToTarget();
            RenderTarget2D target = _upTargets[Iterations - 2];
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);
            Main.spriteBatch.Draw(screenTargetSwap, Vector2.Zero, Color.White);
            Main.spriteBatch.Draw(target, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), DrawColor);
            Main.spriteBatch.End();
        }
    }
}