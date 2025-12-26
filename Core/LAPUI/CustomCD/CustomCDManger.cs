using LAP.Assets.Effects;
using LAP.Assets.Fonts;
using LAP.Assets.TextureRegister;
using LAP.Content.Configs;
using LAP.Core.SystemsLoader;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace LAP.Core.LAPUI.CustomCD
{
    public class CustomCDManger : ModSystem
    {
        public static List<Asset<Texture2D>> CDTexture = [];
        public static List<BaseCD> CDCollection = [];
        // 只在本地渲染，其它玩家的仅用于同步效果
        public static List<BaseCD> ActiveCD => Main.LocalPlayer.LAPCD().ActiveCD;
        public static RenderTarget2D CDRT2D;
        public static RenderTarget2D GassBlurRT2D;
        public static float GlobalOpacity = 1f;// 全局透明度
        public static int FadeInOut;// 背景的淡入淡出
        public static int MaxFade = 30;// 最大淡入淡出时间
        public static LeftArrow leftArrow = new LeftArrow();
        public static RightArrow rightArrow = new RightArrow();
        public static MiddleBG middleBG = new MiddleBG();
        public static int CDXSpacing = 64;// 每个CD之间的间隔
        public static int AllCDY = 264;// CD距离RT2D顶端的距离
        public static BaseCD HoverCD;// 储存当前鼠标悬停的CD
        public static Vector2 FinalDrawPos = Vector2.Zero;
        #region 初始化UI
        public override void Load()
        {
            if (Main.dedServ)
                return;
            InitializeCDUI();
            Main.QueueMainThreadAction(() =>
            {
                CDRT2D = new RenderTarget2D(Main.graphics.GraphicsDevice, 1920, 540);
                GassBlurRT2D = new RenderTarget2D(Main.graphics.GraphicsDevice, 1920, 540);
            });
            On_Main.CheckMonoliths += PrepareRenderTarget;
        }
        public override void OnWorldLoad()
        {
            if (Main.dedServ)
                return;
            InitializeCDUI();
        }
        public static void InitializeCDUI()
        {
            MaxFade = 30;
            GlobalOpacity = 1f;
            leftArrow.Position = new Vector2(60, AllCDY);
            leftArrow.Orig = new Vector2(LAPTextureRegister.CDBG_Edge.Width, LAPTextureRegister.CDBG_Edge.Height / 2);
            rightArrow.Position = new Vector2(51.5f, AllCDY);
            rightArrow.Orig = new Vector2(0, LAPTextureRegister.CDBG_Edge.Height / 2);
            middleBG.Position = new Vector2(60, AllCDY);
            middleBG.Orig = new Vector2(0, LAPTextureRegister.CDBG_Middle.Height / 2);
        }
        #endregion
        #region 卸载
        public override void Unload()
        {
            if (Main.dedServ)
                return;
            Main.QueueMainThreadAction(() =>
            {
                CDRT2D?.Dispose();
                CDRT2D = null;
                GassBlurRT2D?.Dispose();
                GassBlurRT2D = null;
            });
            leftArrow = null;
            rightArrow = null;
            middleBG = null;
            CDCollection.Clear();
            CDTexture.Clear();
            On_Main.CheckMonoliths -= PrepareRenderTarget;
        }
        #endregion
        #region 整体UI更新
        public override void UpdateUI(GameTime gameTime)
        {
            // 只在本地调用
            if (Main.LocalPlayer.whoAmI != Main.myPlayer)
                return;
            AllCDY = (int)MathHelper.Lerp(AllCDY, 166, 0.15f);
            // 这些都是对绘制的更新，其它更新内容扔到Player中的Update
            UpdateFadeInOut();
            UpDateBG();
            UpdateCDPart();
            HoverCD = null;
            for (int i = 0; i < ActiveCD.Count; i++)
            {
                ActiveCD[i].MouseHover = false;
                BaseCD cd = ActiveCD[i];
                Vector2 ThisDrawPos = cd.DrawPosition;
                Vector2 ActiveBuffCount = Vector2.UnitY * 50 * MathF.Ceiling(Main.LocalPlayer.CountBuffs() / 11f);
                if (!LAPUIConfig.Instance.CDChangePosByBuffCount)
                    ActiveBuffCount = Vector2.Zero;
                Vector2 ThisRealDrawPos = ThisDrawPos + Vector2.Zero + new Vector2(LAPUIConfig.Instance.CDUIOffsetX, LAPUIConfig.Instance.CDUIOffsetY) + ActiveBuffCount;
                if (Main.MouseScreen.Distance(ThisRealDrawPos) < 30)
                {
                    HoverCD = cd;
                    ActiveCD[i].MouseHover = true;
                }
                ActiveCD[i].UpdateHover();
            }
            if (HoverCD is not null)
            {
                Main.instance.MouseText(HoverCD.DisplayName().ToString());
            }
        }
        #region 更新CD部件
        public static void UpdateCDPart()
        {            
            // 第一个CD的位置
            Vector2 targetPos = new Vector2(24, AllCDY);
            for (int i = 0; i < ActiveCD.Count; i++)
            {
                targetPos.X = 56 + CDXSpacing * i;// 每一个CD的目标位置
                ActiveCD[i].DrawPosition = Vector2.Lerp(ActiveCD[i].DrawPosition, targetPos, 0.15f);
            }
        }
        #endregion
        #region 更新淡入淡出
        public static void UpdateFadeInOut()
        {
            if (ActiveCD.Count == 0)
            {
                if (FadeInOut > 0)
                    FadeInOut--;
            }
            else
            {
                if (FadeInOut < MaxFade)
                    FadeInOut++;
            }
            GlobalOpacity = MathHelper.Lerp(0, 1f, (float)FadeInOut / MaxFade);
        }
        #endregion
        #region 更新箭头
        public static void UpDateBG()
        {
            // 计算最右侧的箭头应该在哪
            Vector2 RightTarget = new Vector2(51.5f, AllCDY);// 这些是我手动挑选的
            RightTarget.X += CDXSpacing * (ActiveCD.Count - 1);// 这里-1是为了正确的取到偏移，因为已经自带了一定的相对偏移
            if (RightTarget.X < 51.5f)// 限制在最低51.5f像素
                RightTarget.X = 51.5f;
            // 对右侧箭头进行插值平滑运动
            rightArrow.Position = Vector2.Lerp(rightArrow.Position, RightTarget, 0.2f);
            // 计算最左侧的箭头应该在哪
            Vector2 LeftTarget = new Vector2(60, AllCDY);// 这些是我手动挑选的
            leftArrow.Position = Vector2.Lerp(leftArrow.Position, LeftTarget, 0.2f);
            // 计算中央的背景在哪
            Vector2 MiddleTarget = new Vector2(60, AllCDY);// 这些是我手动挑选的
            middleBG.Position = Vector2.Lerp(middleBG.Position, MiddleTarget, 0.2f);
            // 计算左侧箭头到右侧箭头的长度
            float TotalLength = rightArrow.Position.X - leftArrow.Position.X;// 根据左侧箭头的位置计算整体背景条的长度
            middleBG.Length = (int)TotalLength;
        }
        #endregion
        #endregion
        #region 进行离屏渲染
        public static void PrepareRenderTarget(On_Main.orig_CheckMonoliths orig)
        {
            if (Main.dedServ || Main.gameMenu)
            {
                orig();
                return;
            }
            orig();
            #region 预渲染常规RT2D
            LAPUtilities.SwapToTarget(CDRT2D);
            #region 绘制底部
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null);
            // 绘制左侧箭头
            Main.spriteBatch.Draw(leftArrow.texture, leftArrow.Position, null, Color.White * 0.95f, 0, leftArrow.Orig, 0.2f, leftArrow.SE, 0f);
            // 绘制中间背景箭头
            Main.spriteBatch.Draw(middleBG.texture, middleBG.Position, middleBG.rectangle, Color.White * 0.95f, 0, middleBG.Orig, new Vector2(1f, 0.202f), SpriteEffects.None, 0f);
            // 绘制最右侧的箭头
            Main.spriteBatch.Draw(rightArrow.texture, rightArrow.Position, null, Color.White * 0.95f, 0, rightArrow.Orig, 0.2f, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            #endregion
            #region 绘制发光
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null);
            for (int i = 0; i < ActiveCD.Count; i++)
            {
                Effect shader = LAPShaderRegister.CDUIMeltShader.Value;
                shader.Parameters["NoiseTextureSize"].SetValue(new Vector2(0.5f, 0.5f));
                shader.Parameters["progress"].SetValue(ActiveCD[i].Opactiy);
                shader.CurrentTechnique.Passes[0].Apply();
                Main.graphics.GraphicsDevice.Textures[1] = LAPTextureRegister.FireNoise.Value;
                if (ActiveCD[i].PreDrawBloom())
                {
                    Main.spriteBatch.Draw(ActiveCD[i].CDTexture_Bloom, ActiveCD[i].DrawPosition, null, Color.White, 0, ActiveCD[i].CDTexture_Bloom.Size() / 2, 0.25f * ActiveCD[i].Scale, SpriteEffects.None, 0f);
                }
            }
            Main.spriteBatch.End();
            #endregion
            #region 绘制CD与时间
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null);
            // 绘制每一个CD的剩余时间
            DynamicSpriteFont MGRFont = LAPFontsRegister.MGRFonts.Value;
            for (int i = 0; i < ActiveCD.Count; i++)
            {
                Effect shader = LAPShaderRegister.CDUIMeltShader.Value;
                shader.Parameters["NoiseTextureSize"].SetValue(new Vector2(0.5f, 0.5f));
                shader.Parameters["progress"].SetValue(ActiveCD[i].Opactiy);
                shader.CurrentTechnique.Passes[0].Apply();
                Main.graphics.GraphicsDevice.Textures[1] = LAPTextureRegister.FireNoise.Value;
                if (ActiveCD[i].PreDraw())
                {
                    Main.spriteBatch.Draw(ActiveCD[i].CDTexture, ActiveCD[i].DrawPosition, null, Color.White, 0, ActiveCD[i].CDTexture.Size() / 2, 0.25f * ActiveCD[i].Scale, SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(ActiveCD[i].CDTexture_OverLayer, ActiveCD[i].DrawPosition, ActiveCD[i].OverLayerRec, Color.White, 0, ActiveCD[i].CDTexture.Size() / 2, 0.25f * ActiveCD[i].Scale, SpriteEffects.None, 0f);
                }
                if (ActiveCD[i].PreDrawTime(MGRFont))
                {
                    int thisCdRemin = ActiveCD[i].Time;
                    thisCdRemin = thisCdRemin / 60;
                    if (ActiveCD[i].Time == 0)
                        thisCdRemin = -1;
                    string Count = $"{thisCdRemin + 1}";
                    Vector2 stringsize = ChatManager.GetStringSize(MGRFont, Count, Vector2.One);
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, MGRFont, Count, ActiveCD[i].DrawPosition + new Vector2(0, 24), Color.White, 0f, stringsize / 2, new Vector2(0.4f));
                }
                ActiveCD[i].PostDraw();
            }
            Main.spriteBatch.End();
            #endregion
            #endregion
            #region 准备高斯模糊的RT2D
            // 准备进行高斯模糊
            LAPUtilities.SwapToTarget(GassBlurRT2D);
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null);
            Effect shader2 = LAPShaderRegister.CDUIMeltShader.Value;
            shader2.Parameters["NoiseTextureSize"].SetValue(new Vector2(3f, 1f));
            shader2.Parameters["progress"].SetValue(GlobalOpacity);
            shader2.CurrentTechnique.Passes[0].Apply();
            Main.graphics.GraphicsDevice.Textures[1] = LAPTextureRegister.FireNoise.Value;
            Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointClamp;
            Main.spriteBatch.Draw(CDRT2D, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            #endregion
            Main.graphics.GraphicsDevice.SetRenderTargets(null);
        }
        #endregion
        public void DrawAllCD()
        {
            if (Main.playerInventory)
                return;
            Vector2 ActiveBuffCount = Vector2.UnitY * 50 * MathF.Ceiling(Main.LocalPlayer.CountBuffs() / 11f);
            if (!LAPUIConfig.Instance.CDChangePosByBuffCount)
                ActiveBuffCount = Vector2.Zero;
            Vector2 TargetDrawPos = Vector2.Zero + new Vector2(LAPUIConfig.Instance.CDUIOffsetX, LAPUIConfig.Instance.CDUIOffsetY) + ActiveBuffCount;
            Vector2 orig = Vector2.Zero;
            if (LAPUIConfig.Instance.CDUIOrigMode == 2)
            {
                float ToCenterLength = middleBG.Length / 2;
                orig = Vector2.UnitX * ToCenterLength;
            }
            if (LAPUIConfig.Instance.CDUIOrigMode == 3)
            {
                float ToCenterLength = middleBG.Length;
                orig = Vector2.UnitX * ToCenterLength;
            }
            FinalDrawPos = Vector2.Lerp(FinalDrawPos, TargetDrawPos, 0.2f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
            // 将UI的RT2D画出
            Effect shader = LAPShaderRegister.GassBlur.Value;
            shader.Parameters["TargetSize"].SetValue(GassBlurRT2D.Size() * 2f);
            shader.CurrentTechnique.Passes[0].Apply();
            Main.spriteBatch.Draw(GassBlurRT2D, FinalDrawPos, null, Color.White, 0, orig, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(GassBlurRT2D, FinalDrawPos, null, Color.White, 0, orig, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(GassBlurRT2D, FinalDrawPos, null, Color.White, 0, orig, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(GassBlurRT2D, FinalDrawPos, null, Color.White, 0, orig, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(GassBlurRT2D, FinalDrawPos, null, Color.White, 0, orig, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(GassBlurRT2D, FinalDrawPos, null, Color.White, 0, orig, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
            Main.spriteBatch.Draw(GassBlurRT2D, FinalDrawPos, null, Color.White, 0, orig, 1f, SpriteEffects.None, 0f);
            LAPUtilities.ReSetToEndUI();
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
            if (mouseIndex != -1)
            {
                if (LAPConfig.Instance.DeBugInfo)
                {
                    layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("LAP CDDebug Info UI", delegate ()
                    {
                        DrawDebugInfo();
                        return true;
                    }, InterfaceScaleType.UI));
                }
                if (GlobalOpacity != 0)
                {
                    layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("LAP CD Info UI", delegate ()
                    {
                        DrawAllCD();
                        return true;
                    }, InterfaceScaleType.UI));
                }
            }
        }
        public static void DrawDebugInfo()
        {
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            string particleCount = $"当前CD总数: {ActiveCD.Count}";
            Vector2 stringsize = ChatManager.GetStringSize(font, particleCount, Vector2.One);
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, particleCount, LAPUtilities.ScreenCenter_Top() + new Vector2(0, 96), Color.White, 0f, stringsize / 2, new Vector2(1f));
            string cdCount = $"注册了多少cd: {CDCollection.Count}";
            Vector2 cdCountsize = ChatManager.GetStringSize(font, cdCount, Vector2.One);
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, cdCount, LAPUtilities.ScreenCenter_Top() + new Vector2(0, 128), Color.White, 0f, cdCountsize / 2, new Vector2(1f));
        }
    }
}
