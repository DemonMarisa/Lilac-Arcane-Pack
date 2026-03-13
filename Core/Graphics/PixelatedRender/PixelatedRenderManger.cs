using LAP.Assets.Effects;
using LAP.Core.Enums;
using LAP.Core.Graphics.RenderTargetsManager;
using LAP.Core.MiscDate;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.PixelatedRender
{
    public class PixelatedRenderManger : ModSystem
    {
        public static bool BeginDrawProj = false;
        public static int BeforePlayerTargetIndex;
        public static int BeforeDustTargetIndex;

        public static List<IPixelatedRenderer> BeforePlayers = [];
        public static bool BeginDrawBeforePlayers = false;
        public static List<IPixelatedRenderer> BeforeDusts = [];
        public static bool BeginDrawBeforeDusts = false;

        public static int BeforePlayerTargetIndex_Add;
        public static int BeforeDustTargetIndex_Add;

        public static List<IPixelatedRenderer> BeforePlayers_Add = [];
        public static bool BeginDrawBeforePlayers_Add = false;
        public static List<IPixelatedRenderer> BeforeDusts_Add = [];
        public static bool BeginDrawBeforeDusts_Add = false;
        // public static Matrix PixelRenderMatrix;
        public override void Load()
        {
            if (Main.dedServ)
                return;
            RT2DManager.RequestScreenSizeRT2D(out BeforePlayerTargetIndex);
            RT2DManager.RequestScreenSizeRT2D(out BeforeDustTargetIndex);
            RT2DManager.RequestScreenSizeRT2D(out BeforePlayerTargetIndex_Add);
            RT2DManager.RequestScreenSizeRT2D(out BeforeDustTargetIndex_Add);
            On_Main.CheckMonoliths += PrepareRenderTarget;
        }
        public override void Unload()
        {
            if (Main.dedServ)
                return;
            On_Main.CheckMonoliths -= PrepareRenderTarget;
        }
        public static void PrepareRenderTarget(On_Main.orig_CheckMonoliths orig)
        {
            if (Main.dedServ || Main.gameMenu)
            {
                orig();
                return;
            }
            orig();
            // 收集所有接口的信息
            BeforePlayers.Clear();
            BeforeDusts.Clear();
            BeforePlayers_Add.Clear();
            BeforeDusts_Add.Clear();
            if (BeginDrawProj)
            {
                // 检查所有弹幕，如果弹幕继承了接口，那就会把这个添加到对应图层表单中
                foreach (Projectile projectile in Main.ActiveProjectiles)
                {
                    if (projectile.ModProjectile != null && projectile.ModProjectile is IPixelatedRenderer pRPlayer)
                    {
                        if (pRPlayer.LayerToRenderTo.HasFlag(DrawLayer.BeforePlayer) && pRPlayer.BlendState == BlendState.AlphaBlend)
                            BeforePlayers.Add(pRPlayer);
                        if (pRPlayer.LayerToRenderTo.HasFlag(DrawLayer.BeforeDusts) && pRPlayer.BlendState == BlendState.AlphaBlend)
                            BeforeDusts.Add(pRPlayer);
                        if (pRPlayer.LayerToRenderTo.HasFlag(DrawLayer.BeforePlayer) && pRPlayer.BlendState == BlendState.Additive)
                            BeforePlayers_Add.Add(pRPlayer);
                        if (pRPlayer.LayerToRenderTo.HasFlag(DrawLayer.BeforeDusts) && pRPlayer.BlendState == BlendState.Additive)
                            BeforeDusts_Add.Add(pRPlayer);
                    }
                }
                // 收集到绘制到玩家图层前的才绘制
                if (BeforePlayers.Count != 0)
                {
                    DrawToRenderTarget(RT2DManager.RT2D_ScreenSize[BeforePlayerTargetIndex], BeforePlayers, BlendState.AlphaBlend);
                    BeginDrawBeforePlayers = true;// 打一个可以绘制出来玩家层的标记
                }
                if (BeforeDusts.Count != 0)
                {
                    DrawToRenderTarget(RT2DManager.RT2D_ScreenSize[BeforeDustTargetIndex], BeforeDusts, BlendState.AlphaBlend);
                    BeginDrawBeforeDusts = true;
                }
                if (BeforePlayers_Add.Count != 0)
                {
                    DrawToRenderTarget(RT2DManager.RT2D_ScreenSize[BeforePlayerTargetIndex_Add], BeforePlayers_Add, BlendState.Additive);
                    BeginDrawBeforePlayers_Add = true;
                }
                if (BeforeDusts_Add.Count != 0)
                {
                    DrawToRenderTarget(RT2DManager.RT2D_ScreenSize[BeforeDustTargetIndex_Add], BeforeDusts_Add, BlendState.Additive);
                    BeginDrawBeforeDusts_Add = true;
                }
                Main.graphics.GraphicsDevice.SetRenderTarget(null);
                BeginDrawProj = false;
            }
        }
        public static void DrawToRenderTarget(RenderTarget2D renderTarget, List<IPixelatedRenderer> pixelPrimitives, BlendState blendState)
        {
            renderTarget.SwapToTarget();
            if (pixelPrimitives.Count != 0)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, blendState, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null);
                foreach (var pixelPrimitiveDrawer in pixelPrimitives)
                    pixelPrimitiveDrawer.RenderPixelated(Main.spriteBatch);
                Main.spriteBatch.End();
            }
        }
        public static void DrawTarget_BeforePlayers(On_Main.orig_DrawPlayers_AfterProjectiles orig, Main self)
        {
            // 只有当前面标记启用时才会尝试画出
            if (BeginDrawBeforePlayers)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
                Effect effect = LAPShaderRegister.Pixelation.Value;
                effect.Parameters["uTargetResolution"].SetValue(LAPInfo.ScreenSize / 2);
                effect.CurrentTechnique.Passes[0].Apply();
                Main.spriteBatch.Draw(RT2DManager.RT2D_ScreenSize[BeforePlayerTargetIndex], Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
                BeginDrawBeforePlayers = false;
            }
            if (BeginDrawBeforePlayers_Add)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
                Effect effect = LAPShaderRegister.Pixelation.Value;
                effect.Parameters["uTargetResolution"].SetValue(LAPInfo.ScreenSize / 2);
                effect.CurrentTechnique.Passes[0].Apply();
                Main.spriteBatch.Draw(RT2DManager.RT2D_ScreenSize[BeforePlayerTargetIndex_Add], Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
                BeginDrawBeforePlayers_Add = false;
            }
            orig(self);
        }
        public static void DrawTarget_BeforeDust(On_Main.orig_DrawDust orig, Main self)
        {            
            // 只有当前面标记启用时才会尝试画出
            if (BeginDrawBeforeDusts)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
                Effect effect = LAPShaderRegister.Pixelation.Value;
                effect.Parameters["uTargetResolution"].SetValue(LAPInfo.ScreenSize / 2);
                effect.CurrentTechnique.Passes[0].Apply();
                Main.spriteBatch.Draw(RT2DManager.RT2D_ScreenSize[BeforeDustTargetIndex], Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
                BeginDrawBeforeDusts = false;
            }
            if (BeginDrawBeforeDusts_Add)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
                Effect effect = LAPShaderRegister.Pixelation.Value;
                effect.Parameters["uTargetResolution"].SetValue(LAPInfo.ScreenSize / 2);
                effect.CurrentTechnique.Passes[0].Apply();
                Main.spriteBatch.Draw(RT2DManager.RT2D_ScreenSize[BeforeDustTargetIndex_Add], Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
                BeginDrawBeforeDusts_Add = false;
            }
            orig(self);
        }
    }
}
