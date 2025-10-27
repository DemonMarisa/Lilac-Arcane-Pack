using LAP.Assets.Effects;
using LAP.Assets.TextureRegister;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.UI.Chat;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        /// <summary>
        /// 将当前渲染目标设置为提供的渲染目标。
        /// </summary>
        /// <param name="rt">要交换到的渲染目标</param>
        public static bool SwapToTarget(this RenderTarget2D rt)
        {
            GraphicsDevice gD = Main.graphics.GraphicsDevice;
            SpriteBatch spriteBatch = Main.spriteBatch;

            if (Main.gameMenu || Main.dedServ || spriteBatch is null || rt is null || gD is null)
                return false;

            gD.SetRenderTarget(rt);
            gD.Clear(Color.Transparent);
            return true;
        }

        public static void BaseProjPreDraw(this Projectile proj, Texture2D texture, Color lightColor, float rotOffset = 0f, float scale = 1f)
        {
            Vector2 drawPosition = proj.Center - Main.screenPosition;
            float drawRotation = proj.rotation + (proj.spriteDirection == -1 ? MathHelper.Pi : 0f);
            Vector2 rotationPoint = texture.Size() / 2f;
            SpriteEffects flipSprite = (proj.spriteDirection * Main.player[proj.owner].gravDir == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Main.spriteBatch.Draw(texture, drawPosition, null, lightColor, drawRotation + rotOffset, rotationPoint, proj.scale * Main.player[proj.owner].gravDir * scale, flipSprite, 0f);
        }

        public static void FastApplyEdgeMeltsShader(float Opacity, Vector2 TextureSize, Color color, float EdgeWidth = 0.01f, int Pass = 0)
        {
            LAPShaderRegister.EdgeMeltsShader.Parameters["progress"].SetValue(Opacity);
            LAPShaderRegister.EdgeMeltsShader.Parameters["InPutTextureSize"].SetValue(TextureSize);
            LAPShaderRegister.EdgeMeltsShader.Parameters["EdgeColor"].SetValue(color.ToVector4());
            LAPShaderRegister.EdgeMeltsShader.Parameters["EdgeWidth"].SetValue(EdgeWidth);
            LAPShaderRegister.EdgeMeltsShader.CurrentTechnique.Passes[Pass].Apply();
        }

        public static void SetRasterizerState()
        {
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            rasterizerState.FillMode = FillMode.WireFrame;
            Main.instance.GraphicsDevice.RasterizerState = rasterizerState;
        }

        public static bool OutOffScreen(Vector2 pos)
        {
            if (pos.X < Main.screenPosition.X - Main.screenWidth / 2)
                return true;
            
            if (pos.Y < Main.screenPosition.Y - Main.screenHeight / 2)
                return true;
            
            if (pos.X > Main.screenPosition.X + Main.screenWidth * 1.5f)
                return true;
            if (pos.Y > Main.screenPosition.Y + Main.screenHeight * 1.5f)
                return true;
            
            return false;
        }
        public static bool OutOffScreen(Vector2 pos, float areamult = 1f)
        {
            float halfwidth = Main.screenWidth / 2;
            float halfheight = Main.screenHeight / 2;
            if (pos.X < Main.screenPosition.X - halfwidth * areamult)
                return true;

            if (pos.Y < Main.screenPosition.Y - halfheight * areamult)
                return true;

            if (pos.X > Main.screenPosition.X + Main.screenWidth + halfwidth * areamult)
                return true;
            if (pos.Y > Main.screenPosition.Y + Main.screenHeight + halfheight * areamult)
                return true;

            return false;
        }
        #region shader
        public static void ReSetToBeginShader()
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public static void ReSetToBeginShader(BlendState blendState)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, blendState, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public static void ReSetToBeginShader(BlendState blendState, Matrix matrix)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, blendState, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, matrix);
        }
        public static void ReSetToBeginShader(BlendState blendState, SamplerState samplerState)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, blendState, samplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public static void ReSetToBeginShader(BlendState blendState, SamplerState samplerState, Matrix matrix)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, blendState, samplerState, DepthStencilState.None, Main.Rasterizer, null, matrix);
        }
        public static void ReSetToEndShader()
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
        }


        public static void ReSetToBeginUI(BlendState blendState)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, blendState, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);
        }

        public static void ReSetToEndUI()
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);
        }
        public static void ReSetToEndShader(BlendState blendState)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, blendState, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
        }
        #endregion
        public static void UpDateFrame(int horizontalFrames, int verticalFrames, ref int FrameX, ref int FrameY)
        {
            if (FrameX < horizontalFrames - 1)
                FrameX++;
            else
            {
                FrameX = 0;
                if (FrameY < verticalFrames - 1)
                    FrameY++;
                else
                    FrameY = 0;
            }
        }

        public static void DrawCube(Vector2 pos)
        {
            Main.spriteBatch.Draw(LAPTextureRegister.WhiteCube.Value, pos - Main.screenPosition, null,Color.White, 0, LAPTextureRegister.WhiteCube.Size() / 2, 1, SpriteEffects.None, default);
        }

        public static Color LerpColor(Color begin, Color end)
        {
            return Color.Lerp(begin, end, Main.rand.NextFloat(0, 1));
        }
        public static Color LerpColor(Color begin, Color end, float progress)
        {
            return Color.Lerp(begin, end, progress);
        }

        public static Vector2 ScreenCenter()
        {
            return new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
        }

        /// <summary>
        /// 检查一个角度是否在目标角度中心的扇形区域内。
        /// 考虑到跨越 0/2π 边界的情况。
        /// </summary>
        public static bool IsAngleInSector(float checkAngle, float centerAngle, float halfSectorWidth)
        {
            // 将所有角度都相对 centerAngle 移动，使 centerAngle 变为 0
            float relativeAngle = checkAngle - centerAngle;
            // 规范化相对角度到 (-π, π] 范围内
            while (relativeAngle > MathHelper.Pi)
            {
                relativeAngle -= MathHelper.TwoPi;
            }
            while (relativeAngle <= -MathHelper.Pi)
            {
                relativeAngle += MathHelper.TwoPi;
            }
            // 检查是否在 +/- 半扇区宽度内
            return Math.Abs(relativeAngle) <= halfSectorWidth;
        }
        #region 更好的文字换行
        /// <summary>
        /// 用于自动换行的方法
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="font">字体</param>
        /// <param name="maxWidth">一行最多多少像素</param>
        /// <param name="maxLines">最多多少航</param>
        /// <param name="lineAmount">关联的右侧显示文本</param>
        public static string[] BetterWordwrapString(string text, DynamicSpriteFont font, int maxWidth, int maxLines, out int lineAmount)
        {
            // 创建数组array
            string[] array = new string[maxLines];

            int Lines = 0;

            // 按换行拆分后存入list
            // 把list1拆分后再按空格拆分存入list2
            List<string> list = [.. text.Split('\n')];
            List<string> list2 = [.. list[0].Split(' ')];

            // 最多处理maxLines - 1个段落
            for (int i = 1; i < list.Count && i < maxLines; i++)
            {
                list2.Add("\n");
                list2.AddRange(list[i].Split(' '));
            }

            // 遍历list2，分四种情况处理

            // flag为标记新行开始用
            bool flag = true;
            while (list2.Count > 0)
            {

                string text2 = list2[0];
                string text3 = " ";
                if (list2.Count == 1)
                    text3 = "";

                // 如果遇到换行符
                if (text2 == "\n")
                {
                    // 强制换行并增加行号
                    // 若超过最大行数则终止处理

                    array[Lines++] += text2;
                    flag = true;

                    if (Lines >= maxLines)
                        break;

                    list2.RemoveAt(0);
                }
                else if (flag)
                {
                    // 逐字符拼接直到超出宽度
                    // 将剩余部分重新插入列表开头

                    if (font.MeasureString(text2).X > (float)maxWidth)
                    {
                        // 拆分单词逻辑

                        string text4 = text2[0].ToString() ?? "";
                        int num2 = 1;
                        while (font.MeasureString(text4 + text2[num2]).X <= (float)maxWidth)
                        {
                            text4 += text2[num2++];
                        }

                        array[Lines++] = text4;
                        if (Lines >= maxLines)
                            break;

                        list2.RemoveAt(0);
                        list2.Insert(0, text2.Substring(num2));
                    }
                    else
                    {
                        ref string reference = ref array[Lines];
                        reference = reference + text2 + text3;
                        flag = false;
                        list2.RemoveAt(0);
                    }
                }
                else if (font.MeasureString(array[Lines] + text2).X > (float)maxWidth)
                {
                    Lines++;
                    if (Lines >= maxLines)
                        break;

                    flag = true;
                }
                else
                {
                    ref string reference2 = ref array[Lines];
                    reference2 = reference2 + text2 + text3;
                    flag = false;
                    list2.RemoveAt(0);
                }

            }

            lineAmount = Lines;
            if (lineAmount == maxLines)
                lineAmount--;

            return array;
        }
        #endregion
        #region 绘制文字
        public static void DrawTextNoLine(string textContent, Vector2 DrawPos,Vector2 ResolutionScalefloat, Vector2 offset, float scale, Color TextColor, Color TextOutLineColor, float maxWidth = 0f, float lineSpacing = 1.2f)
        {
            // 获取字体引用
            DynamicSpriteFont font = FontAssets.MouseText.Value;

            // 自动换行处理
            List<string> wrappedLines = new List<string>();

            if (maxWidth > 0)
            {
                // 计算实际可用宽度（考虑缩放）
                float actualMaxWidth = maxWidth / ResolutionScalefloat.X;
                wrappedLines = BetterWordwrapString(textContent, font, (int)actualMaxWidth, 999, out _).Where(line => !string.IsNullOrEmpty(line)).ToList();
            }
            // 计算基准行高
            float baseLineHeight = font.LineSpacing * scale * lineSpacing;
            // 计算起始位置（屏幕中心 + 偏移量）
            Vector2 startPosition = DrawPos + offset;

            for (int i = 0; i < wrappedLines.Count; i++)
            {
                string line = wrappedLines[i];
                if (string.IsNullOrEmpty(line))
                    continue;

                // 计算当前行位置
                Vector2 linePosition = new Vector2(
                    startPosition.X,
                    startPosition.Y + (baseLineHeight * i)
                );

                // 计算文本尺寸
                Vector2 textSize = ChatManager.GetStringSize(font, line, new Vector2(scale));

                for (int j = 0; j < 4; j++)
                {
                    ChatManager.DrawColorCodedString(Main.spriteBatch, font, line, linePosition, TextOutLineColor, 0f, new Vector2(textSize.X / 2f, 0f),
                        ResolutionScalefloat * scale, maxWidth, false);
                }

                ChatManager.DrawColorCodedString(Main.spriteBatch, font, line, linePosition, TextColor, 0f, new Vector2(textSize.X / 2f, 0f),
                    ResolutionScalefloat * scale, maxWidth, false);
            }
        }
        #endregion

        public static Color SetAlphaToZero(this Color set)
        {
            set.A = 0;
            return set;
        }
    }
}
