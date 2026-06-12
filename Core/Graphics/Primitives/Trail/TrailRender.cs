using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;

namespace LAP.Core.Graphics.Primitives.Trail
{
    public class TrailRender
    {
        public static void RenderTrail(TrailDrawData[] drawData, DrawSetting drawSetting)
        {
            if (drawData.Length < 3)
                return;
            // 1. 平滑处理传入的点
            DrawTrail(drawData, drawSetting);
        }
        //private static List<TrailDrawData> SmoothTrail(TrailDrawData[] rawData, int segments)
        //{
        //    smoothPointsBuffer.Clear();
        //    // 至少需要4个点来进行完整的 Catmull-Rom 插值，为了防止越界我们需要处理首尾
        //    for (int i = 0; i < rawData.Length - 1; i++)
        //    {
        //        // 获取插值所需的 4 个控制点 (处理边界情况)
        //        TrailDrawData p0 = rawData[Math.Max(i - 1, 0)];
        //        TrailDrawData p1 = rawData[i];
        //        TrailDrawData p2 = rawData[Math.Min(i + 1, rawData.Length - 1)];
        //        TrailDrawData p3 = rawData[Math.Min(i + 2, rawData.Length - 1)];
        //        for (int j = 0; j < segments; j++)
        //        {
        //            float t = (float)j / segments;
        //            // 位置平滑插值
        //            Vector2 pos = Vector2.CatmullRom(p0.PosData, p1.PosData, p2.PosData, p3.PosData, t);
        //            // 其他属性进行线性插值 (颜色、宽度偏移、旋转)
        //            Color color = Color.Lerp(p1.DrawColor, p2.DrawColor, t);
        //            Vector2 offset = Vector2.Lerp(p1.PrimitivesOffset, p2.PrimitivesOffset, t);
        //            float rot = MathHelper.Lerp(p1.PrimitivesHeightRot, p2.PrimitivesHeightRot, t);
        //            smoothPointsBuffer.Add(new TrailDrawData(pos, color, offset, rot));
        //        }
        //    }
        //    // 加入最后一个点
        //    smoothPointsBuffer.Add(rawData[^1]);
        //    return smoothPointsBuffer;
        //}
        //private static void DrawTrail(List<TrailDrawData> drawData, DrawSetting drawSetting)
        //{
        //    vertexBuffer.Clear();

        //    // 计算总距离，用于无拉伸的 UV 映射
        //    float totalDistance = 0f;
        //    float[] distances = new float[drawData.Count];
        //    for (int i = 1; i < drawData.Count; i++)
        //    {
        //        totalDistance += Vector2.Distance(drawData[i - 1].PosData, drawData[i].PosData);
        //        distances[i] = totalDistance;
        //    }

        //    for (int i = 0; i < drawData.Count; i++)
        //    {
        //        // 基于实际距离计算 U 坐标 (0 到 1)
        //        float progress = totalDistance == 0 ? 0 : distances[i] / totalDistance;
        //        // 可以通过传入委托或函数来控制特定位置的宽度 (Tapering)，这里作为扩展预留
        //        // float widthMultiplier = (1f - progress); // 例如：尾部逐渐变尖
        //        Vector2 drawPos = drawData[i].PosData;
        //        Vector2 primitivesHeight = drawData[i].PrimitivesOffset; // * widthMultiplier 
        //        float rot = drawData[i].PrimitivesHeightRot;
        //        Color drawColor = drawData[i].DrawColor;
        //        // 预计算旋转，减少运算量
        //        Vector2 upOffset = primitivesHeight.RotatedBy(rot);
        //        Vector2 downOffset = -upOffset;
        //        // 插入上下两个顶点
        //        vertexBuffer.Add(new VertexPositionColorTexture2D(drawPos + downOffset, drawColor, new Vector3(progress, 0, 0)));
        //        vertexBuffer.Add(new VertexPositionColorTexture2D(drawPos + upOffset, drawColor, new Vector3(progress, 1, 0)));
        //    }

        //    if (vertexBuffer.Count > 3) // TriangleStrip 至少需要 3 个顶点 (这里每次加2个，至少3对即6个)
        //    {
        //        GraphicsDevice device = Main.graphics.GraphicsDevice;
        //        // 设置渲染状态
        //        RasterizerState originalRasterizer = device.RasterizerState;
        //        device.RasterizerState = RasterizerState.CullNone;
        //        device.Textures[0] = drawSetting.texture2d;
        //        device.SamplerStates[0] = drawSetting.sampler;
        //        // 转换为数组渲染，但在高频调用下，建议使用 DynamicVertexBuffer 进一步优化
        //        VertexPositionColorTexture2D[] renderArray = vertexBuffer.ToArray();
        //        device.DrawUserPrimitives(PrimitiveType.TriangleStrip, renderArray, 0, renderArray.Length - 2);
        //        // 恢复渲染状态
        //        device.RasterizerState = originalRasterizer;
        //    }
        //}
        public static void DrawTrail(TrailDrawData[] drawData, DrawSetting drawSetting)
        {
            List<VertexPositionColorTexture2D> Vertexlist = new List<VertexPositionColorTexture2D>();
            for (int i = 0; i < drawData.Length; i++)
            {
                float progress = (float)i / drawData.Length;
                // 绘制位置
                Vector2 DrawPos = drawData[i].PosData;

                // 每个片的高度与旋转
                Vector2 PrimitivesHeight = drawData[i].PrimitivesOffset;
                float PrimitivesHeightRot = drawData[i].PrimitivesHeightRot;
                Color DrawColor = drawData[i].DrawColor;

                Vertexlist.Add(new VertexPositionColorTexture2D(DrawPos - PrimitivesHeight.RotatedBy(PrimitivesHeightRot), DrawColor, new Vector3(progress, 0, 0)));
                Vertexlist.Add(new VertexPositionColorTexture2D(DrawPos + PrimitivesHeight.RotatedBy(PrimitivesHeightRot), DrawColor, new Vector3(progress, 1, 0)));
            }
            Main.graphics.GraphicsDevice.Textures[0] = drawSetting.texture2d;
            Main.graphics.GraphicsDevice.SamplerStates[0] = drawSetting.sampler;
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, Vertexlist.ToArray(), 0, Vertexlist.Count - 2);
        }
    }
}
