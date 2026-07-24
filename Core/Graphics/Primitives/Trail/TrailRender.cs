using LAP.Core.DebugSystem;
using LAP.Core.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Animations;

namespace LAP.Core.Graphics.Primitives.Trail
{
    public class TrailRender
    {
        private static Texture2D _lastTexture;
        private static SamplerState _lastSamplerState;
        public static GraphicsDevice graphicsDevice => Main.graphics.GraphicsDevice;
        private const int MaxVertex = 4096;
        private const int MaxPoints = 2048;

        private static TrailDrawData[] PointsBuffer = new TrailDrawData[MaxPoints];
        private static int PointsCount = 0;
        private static float[] DistanceBuffer = new float[MaxPoints];

        private static VertexPositionColorTexture2D[] vertexArray = new VertexPositionColorTexture2D[MaxVertex];
        private static int vertexCount = 0;

        private static DrawSetting CurrentSetting;
        public unsafe static void RenderTrail(IReadOnlyList<TrailDrawData> drawData, DrawSetting drawSetting)
        {
            if (drawData == null || drawData.Count < 2)
                return;
            CurrentSetting = drawSetting;
            Reset();
            CheckArraySize(PointsCount, drawSetting.smoothSegments);
            SmoothPoint(drawData, drawSetting.smoothSegments);
            BuildVertex();
            DrawTrail();
        }
        public static void Reset()
        {
            PointsCount = 0;
            vertexCount = 0;
        }
        public static void CheckArraySize(int rawDataCount, int segments)
        {
            // 动态扩容
            // 提前计算需要的最大点数和顶点数
            int requiredPoints = (segments > 0) ? (rawDataCount * segments) + 1 : rawDataCount;
            int requiredVertices = requiredPoints * 2;

            if (requiredPoints >= PointsBuffer.Length)
            {
                // 直接扩容到足够的大小，而不是盲目 *2
                int newPointSize = Math.Max(PointsBuffer.Length * 2, requiredPoints + 128);
                Array.Resize(ref PointsBuffer, newPointSize);
                Array.Resize(ref DistanceBuffer, newPointSize);
            }

            if (requiredVertices >= vertexArray.Length)
            {
                int newVertexSize = Math.Max(vertexArray.Length * 2, requiredVertices + 256);
                Array.Resize(ref vertexArray, newVertexSize);
            }
        }
        public static void SmoothPoint(IReadOnlyList<TrailDrawData> rawData, int segments)
        {
            if (CurrentSetting.smoothSegments > 0)
            {
                // 进行插值平滑
                for (int i = 0; i < rawData.Count - 1; i++)
                {
                    TrailDrawData p0 = rawData[Math.Max(i - 1, 0)];
                    TrailDrawData p1 = rawData[i];
                    TrailDrawData p2 = rawData[Math.Min(i + 1, rawData.Count - 1)];
                    TrailDrawData p3 = rawData[Math.Min(i + 2, rawData.Count - 1)];
                    for (int j = 0; j < segments; j++)
                    {
                        float t = (float)j / segments;
                        // 插值所有属性
                        Vector2 pos = Vector2.CatmullRom(p0.Position, p1.Position, p2.Position, p3.Position, t);
                        Color color = Color.Lerp(p1.DrawColor, p2.DrawColor, t);
                        float offset = MathHelper.Lerp(p1.Height, p2.Height, t);
                        float rot = Utils.AngleLerp(p1.Rotation, p2.Rotation, t);
                        PointsBuffer[PointsCount++] = new TrailDrawData(pos, color, offset, rot);
                    }
                }
                // 补上最后一个控制点，防止尾部截断
                PointsBuffer[PointsCount++] = rawData[^1];
            }
            else
            {
                for (int i = 0; i < rawData.Count; i++)
                    PointsBuffer[PointsCount++] = rawData[i];
            }
        }
        public static void FlipUV(ref Vector3 upUV, ref Vector3 downUV, float progress)
        {
            if (CurrentSetting.trailEffect is TrailEffects.FlipHorizontally)
            {
                upUV.X = 1f - progress;
                downUV.X = 1f - progress;
            }
            else if (CurrentSetting.trailEffect is TrailEffects.FlipVertically)
            {
                upUV.Y = 0;
                downUV.Y = 1;
            }
            else if (CurrentSetting.trailEffect is TrailEffects.FlipBoth)
            {
                upUV.X = 1f - progress;
                downUV.X = 1f - progress;
                upUV.Y = 0;
                downUV.Y = 1;
            }
        }
        public static void BuildVertex()
        {
            int pointCount = PointsCount;
            // 计算距离和UV坐标
            float totalDistance = 0f;
            if (CurrentSetting.smoothUV)
            {
                DistanceBuffer[0] = 0f;
                for (int i = 1; i < pointCount; i++)
                {
                    totalDistance += Vector2.Distance(PointsBuffer[i - 1].Position, PointsBuffer[i].Position);
                    DistanceBuffer[i] = totalDistance;
                }
            }
            // 生成顶点数据
            for (int i = 0; i < pointCount; i++)
            {
                TrailDrawData data = PointsBuffer[i];
                // 计算垂直于朝向的偏移量
                Vector2 offset = new Vector2(-(float)Math.Sin(data.Rotation), (float)Math.Cos(data.Rotation)) * data.Height;
                // 计算进度
                float progress = 0f;
                if (pointCount > 0)
                    progress = CurrentSetting.smoothUV ? (totalDistance == 0 ? 0 : DistanceBuffer[i] / totalDistance) : (float)i / (pointCount - 1);
                Vector3 upUV = new(progress + CurrentSetting.xOffset, 1f, 0f);
                Vector3 downUV = new(progress + CurrentSetting.xOffset, 0f, 0f);
                // 翻转UV
                FlipUV(ref upUV, ref downUV, progress);
                vertexArray[vertexCount++] = new VertexPositionColorTexture2D(data.Position + offset, data.DrawColor, upUV);
                vertexArray[vertexCount++] = new VertexPositionColorTexture2D(data.Position - offset, data.DrawColor, downUV);
            }
        }
        public unsafe static void DrawTrail()
        {
            // 应用shader
            Effect effect = CurrentSetting.effect;
            effect?.CurrentTechnique.Passes[CurrentSetting.applyPass].Apply();
            if (vertexCount >= 3)
            {
                graphicsDevice.Textures[0] = CurrentSetting.texture;
                graphicsDevice.SamplerStates[0] = CurrentSetting.samplerState;
                graphicsDevice.RasterizerState = RasterizerState.CullNone;
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertexArray, 0, vertexCount - 2);
            }
        }
    }
}
