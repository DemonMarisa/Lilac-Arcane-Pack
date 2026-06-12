using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.DeepGlow
{
    public partial class DeepGlow : ModSystem
    {
        public static Queue<Action> GlowRequests_AfterProjectile = new Queue<Action>();
        public static Queue<Action> GlowRequests_AfterDust = new Queue<Action>();
        public static void Hook_AfterProjectile(On_Main.orig_DrawProjectiles orig, Main self)
        {
            orig(self);
            RenderLayerGlow(ref GlowRequests_AfterProjectile);
        }

        public static void Hook_AfterDust(On_Main.orig_DrawDust orig, Main self)
        {
            orig(self);
            RenderLayerGlow(ref GlowRequests_AfterDust);
        }
        public static void RenderLayerGlow(ref Queue<Action> action)
        {
            if (action.Count == 0)
                return;
            if (_downTargets == null || HightLightTarget == null || GlowEffect == null || Iterations < 2)
            {
                action.Clear();
                return;
            }
            // 设置着色器参数
            GlowEffect.Parameters["uThreshold"].SetValue(Threshold);
            GlowEffect.Parameters["uIntensity"].SetValue(Intensity);
            GlowEffect.Parameters["uBlurRadius"].SetValue(BlurRadius);
            GlowEffect.Parameters["uSoftKnee"].SetValue(SoftKnee);
            // 保存当前原版的RT
            var originalTargets = Main.instance.GraphicsDevice.GetRenderTargets();
            // 提取高亮并绘制
            HightLightTarget.SwapToTarget();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            while (action.Count > 0)
            {
                Action drawAction = action.Dequeue();
                drawAction.Invoke();
            }
            Main.spriteBatch.End();
            // 2. 降采样与升采样
            DownSampler();
            UpSampler();
            // 【关键改动】切回原版的渲染目标，准备把辉光混合进去
            Main.instance.GraphicsDevice.SetRenderTargets(originalTargets);
            // 3. 将这一层的 Bloom 结果加法混合到当前画面
            RenderTarget2D finalGlowTexture = _upTargets[Iterations - 2];
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);
            Main.spriteBatch.Draw(finalGlowTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), DrawColor);
            Main.spriteBatch.End();
        }
    }
}
