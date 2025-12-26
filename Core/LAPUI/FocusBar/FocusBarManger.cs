using LAP.Assets.Effects;
using LAP.Assets.Fonts;
using LAP.Content.Configs;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static LAP.Core.LAPUI.FocusBar.FocusBarPart;

namespace LAP.Core.LAPUI.FocusBar
{
    public class FocusBarManger : ModSystem
    {
        public static bool UseFocus = false;
        public static RenderTarget2D FocusBarTarget;
        public static Vector2 TargetSize = new Vector2(600, 300);
        public static bool CanDrawFocusBar = true;
        public static BarLeftBG barLeftBG = new BarLeftBG();
        public static BarRightBG barRightBG = new BarRightBG();
        public static BarMiddleBG barMiddleBG = new BarMiddleBG();
        public static BarTopPattern barTopPattern = new BarTopPattern();
        public static BarBottomPattern barBottomPattern = new BarBottomPattern();
        public static Bar bar = new Bar();
        public static float BarRatio;
        public static float OldBarRatio;
        public static float OldFocus;// 记录上一帧的专注值
        public static int ReduceTimer;// 记录消耗后延迟多少帧，用于提示消耗了多少
        public static int ReduceTimerMax = 30;
        public static Vector2 FinalDrawPos = new Vector2(1345, 48);
        public override void Load()
        {
            if (Main.dedServ)
                return;
            Main.QueueMainThreadAction(() =>
            {
                FocusBarTarget = new RenderTarget2D(Main.graphics.GraphicsDevice, (int)TargetSize.X, (int)TargetSize.Y);
            });
            On_Main.CheckMonoliths += PrepareRenderTarget;
            InitializeCDUI();
            BarRatio = 1f;
        }
        public static void InitializeCDUI()
        {
            if (FocusBarTarget is null)
                return;
            barLeftBG.Position = TargetSize / 2 - new Vector2(156, 0);
            barLeftBG.Orig = barLeftBG.texture.Size() / 2f;
            barLeftBG.SE = SpriteEffects.None;

            bar.Position = TargetSize / 2 - new Vector2(158, 0.1f);
            bar.Orig = new Vector2(0, bar.texture.Height / 2);

            barRightBG.Position = TargetSize / 2 + new Vector2(156, 0);
            barRightBG.Orig = barRightBG.texture.Size() / 2f;
            barRightBG.SE = SpriteEffects.FlipHorizontally;
            barMiddleBG.Position = barLeftBG.Position;
            barMiddleBG.Orig = new Vector2(0, barMiddleBG.texture.Height / 2);
            barTopPattern.Position = TargetSize / 2 + new Vector2(0, -26); ;
            barTopPattern.Orig = barTopPattern.texture.Size() / 2f;
            barBottomPattern.Position = TargetSize / 2 + new Vector2(0, 26); ;
            barBottomPattern.Orig = barBottomPattern.texture.Size() / 2f;
        }
        public override void Unload()
        {
            barLeftBG = null;
            barRightBG = null;
            barMiddleBG = null;
            barTopPattern = null;
            barBottomPattern = null;
            if (Main.dedServ)
                return;
            On_Main.CheckMonoliths -= PrepareRenderTarget;
            Main.QueueMainThreadAction(() =>
            {
                FocusBarTarget?.Dispose();
                FocusBarTarget = null;
            });
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (Main.dedServ)
                return;
            if (!LAPUIConfig.Instance.DrawFocusBar)
                return;
            if (ReduceTimer > 0)
                ReduceTimer--;
            if (OldFocus > Main.LocalPlayer.StatFocus())
            {
                ReduceTimer = 30;
            }
            float TargetRatio = Main.LocalPlayer.FocusRatio();
            BarRatio = MathHelper.Lerp(BarRatio, TargetRatio, 0.2f);
            if (ReduceTimer <= 0)
                OldBarRatio = MathHelper.Lerp(OldBarRatio, TargetRatio, 0.2f);
            Vector2 DrawPos = FinalDrawPos + new Vector2(LAPUIConfig.Instance.FocusBarOffsetX, LAPUIConfig.Instance.FocusBarOffsetY);
            Rectangle mouserec = new Rectangle((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 4, 4);
            Rectangle barRec = Utils.CenteredRectangle(DrawPos, new Vector2(320, 30));
            if (mouserec.Intersects(barRec))
            {
                LocalizedText DisplayName = Language.GetText("Mods.LAP.MouseMessage.Focus");
                LocalizedText Shift = Language.GetText("Mods.LAP.MouseMessage.ShiftTag");
                LocalizedText FocusRegen = Language.GetText("Mods.LAP.MouseMessage.FocusRegen");
                LocalizedText FocusIntroduction = Language.GetText("Mods.LAP.MouseMessage.FocusIntroduction");
                LocalizedText NoUseFocusText = Language.GetText("Mods.LAP.MouseMessage.NoUseFocusText");
                LocalizedText TestInfo = Language.GetText("Mods.LAP.MouseMessage.TestInfo");
                string show = $"{DisplayName}" + Main.LocalPlayer.LAP().statFocus + "/" + Main.LocalPlayer.LAP().statFocusMax2;
                string Regen = $"{FocusRegen}{Main.LocalPlayer.FocusRegen()} FP/s";
                string Test = $"{TestInfo}";
                if (Main.keyState.IsKeyDown(Keys.LeftShift))
                {
                    if (UseFocus)
                        Main.instance.MouseText($"{show}\n" + $"{Regen}\n" + $"{FocusIntroduction}\n" + TestInfo);
                    else
                        Main.instance.MouseText($"{show}\n" + $"{Regen}\n" + $"{FocusIntroduction}\n" + $"{NoUseFocusText}\n" + TestInfo);
                }
                else
                {
                    Main.instance.MouseText($"{show}\n" + $"{Regen}\n" + $"{Shift}\n");
                }
            }
            OldFocus = Main.LocalPlayer.StatFocus();
        }
        public static void PrepareRenderTarget(On_Main.orig_CheckMonoliths orig)
        {
            if (Main.dedServ || Main.gameMenu)
            {
                orig();
                return;
            }
            orig();
            if (!LAPUIConfig.Instance.DrawFocusBar)
                return;
            LAPUtilities.SwapToTarget(FocusBarTarget);
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null);

            Main.spriteBatch.Draw(barLeftBG.texture, barLeftBG.Position, null, Color.White, 0, barLeftBG.Orig, 0.15f, barLeftBG.SE, 0f);
            Main.spriteBatch.Draw(barRightBG.texture, barRightBG.Position, null, Color.White, 0, barRightBG.Orig, 0.15f, barRightBG.SE, 0f);
            float Length = barRightBG.Position.X - barLeftBG.Position.X;// 间距像素
            float ScaleMult = Length / barMiddleBG.texture.Width;
            Main.spriteBatch.Draw(barMiddleBG.texture, barMiddleBG.Position, null, Color.White, 0, barMiddleBG.Orig, new Vector2(ScaleMult, 0.15f), 0, 0f);

            Effect effect = LAPShaderRegister.Fill.Value;
            effect.CurrentTechnique.Passes[0].Apply();
            Main.spriteBatch.Draw(barTopPattern.texture, barTopPattern.Position, null, Color.Silver, 0, barTopPattern.Orig, 0.2f, 0, 0f);
            Main.spriteBatch.Draw(barBottomPattern.texture, barBottomPattern.Position, null, Color.Silver, 0, barBottomPattern.Orig, 0.2f, 0, 0f);

            Effect effect2 = LAPShaderRegister.FocusBar.Value;
            effect2.Parameters["BeginColor"].SetValue(Color.OrangeRed.ToVector4());
            effect2.Parameters["EndColor"].SetValue(Color.Orange.ToVector4());
            effect2.CurrentTechnique.Passes[0].Apply();
            Vector2 leftPos = TargetSize / 2 - new Vector2(158, 0);
            Vector2 rightPos = TargetSize / 2 + new Vector2(159, 0);
            float Length2 = rightPos.X - leftPos.X;// 间距像素
            float ScaleMult2 = Length2 / bar.texture.Width;
            Rectangle Oldrec = new Rectangle(0, 0, (int)(bar.texture.Width * OldBarRatio), bar.texture.Height);
            Main.spriteBatch.Draw(bar.texture, bar.Position, Oldrec, Color.White, 0, bar.Orig, new Vector2(ScaleMult2, 0.035f), 0, 0f);

            effect2.Parameters["BeginColor"].SetValue(Color.RoyalBlue.ToVector4());
            effect2.Parameters["EndColor"].SetValue(Color.SkyBlue.ToVector4());
            effect2.CurrentTechnique.Passes[0].Apply();
            Rectangle rec = new Rectangle(0, 0, (int)(bar.texture.Width * BarRatio), bar.texture.Height);
            Main.spriteBatch.Draw(bar.texture, bar.Position, rec, Color.White, 0, bar.Orig, new Vector2(ScaleMult2, 0.035f), 0, 0f);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null);

            Main.spriteBatch.End();
            Main.graphics.GraphicsDevice.SetRenderTargets(null);
        }
        public static void DrawBar()
        {
            if (Main.dedServ)
                return;
            if (!LAPUIConfig.Instance.DrawFocusBar)
                return;
            Vector2 DrawPos = FinalDrawPos + new Vector2(LAPUIConfig.Instance.FocusBarOffsetX, LAPUIConfig.Instance.FocusBarOffsetY);
            Main.spriteBatch.Draw(FocusBarTarget, DrawPos, null, Color.White, 0, FocusBarTarget.Size() / 2, 1f, 0, 0f);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (!LAPUIConfig.Instance.DrawFocusBar)
                return;
            int mouseIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
            if (mouseIndex != -1)
            {
                layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("LAP Focus Bar UI", delegate ()
                {
                    DrawBar();
                    return true;
                }, InterfaceScaleType.UI));
            }
        }
    }
}
