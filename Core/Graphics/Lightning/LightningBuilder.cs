using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.Lightning
{
    public partial class LightningBuilder : ModSystem
    {
        public static List<Vector2> _pathBuffer = [];
        public static bool HasAnyLightning;
        public const int MaxLightning = 1000;
        public static LAPLightning[] LAPLightnings = new LAPLightning[MaxLightning];
        public override void Load()
        {
            for (int i = 0; i < MaxLightning; i++)
            {
                LAPLightnings[i] = new LAPLightning { WhoAmI = i, Active = false };
            }
        }
        public override void Unload()
        {
            for (int i = 0; i < MaxLightning; i++)
            {
                LAPLightnings[i] = null;
            }
        }
        public override void PostUpdateDusts()
        {
            if (HasAnyLightning)
            {
                HasAnyLightning = false;
                for (int i = 0; i < MaxLightning; i++)
                {
                    if (LAPLightnings[i].Active)
                    {
                        HasAnyLightning = true;
                        LAPLightnings[i].Update();
                    }
                }
            }
        }
        public static LAPLightning SpawnLightning(Vector2 Begin, Vector2 End, Color color, Color glowcolor, float strength, float width, int lifetime, int generations)
        {
            for (int i = 0; i < LAPLightnings.Length; i++)
            {
                if (!LAPLightnings[i].Active)
                {
                    LAPLightnings[i].xScale = 0f;
                    LAPLightnings[i].Active = true;
                    LAPLightnings[i].lifeTime = 0;
                    LAPLightnings[i].MaxlifeTime = lifetime;
                    LAPLightnings[i].Width = width;
                    LAPLightnings[i].Color = color;
                    LAPLightnings[i].GlowColor = glowcolor;
                    LAPLightnings[i].Nodes = GeneratePath(Begin, End, strength, generations);
                    LAPLightnings[i].RandomFlowOffset = Main.rand.NextFloat(0f, 100f);
                    HasAnyLightning = true;
                    return LAPLightnings[i];
                }
            }
            return null;
        }
        #region 生成路径
        /// <summary>
        /// 生成路径
        /// </summary>
        private static List<Vector2> GeneratePath(Vector2 BeginPoint, Vector2 EndPoint, float strength, int generations)
        {
            List<Vector2> points = new List<Vector2>();
            points.Add(BeginPoint);
            points.Add(EndPoint);

            float currentStrength = strength; // 用于在迭代中衰减的强度

            for (int gen = 0; gen < generations; gen++)
            {
                _pathBuffer.Clear();
                _pathBuffer.Add(points[0]);
                for (int i = 0; i < points.Count - 1; i++)
                {
                    Vector2 start = points[i];
                    Vector2 end = points[i + 1];
                    Vector2 mid = (start + end) * 0.5f;

                    // 计算该线段的法线向量（垂直方向）
                    Vector2 direction = Vector2.Normalize(end - start);
                    Vector2 normal = new Vector2(-direction.Y, direction.X);

                    // 沿着法线方向进行随机偏移，而不是在一个圆内随机
                    float offset = Main.rand.NextFloat(-currentStrength, currentStrength);
                    mid += normal * offset;

                    _pathBuffer.Add(mid);
                    _pathBuffer.Add(end);
                }
                points = [.. _pathBuffer];
                currentStrength *= 0.6f;
            }
            return points;
        }
        #endregion
    }
}
