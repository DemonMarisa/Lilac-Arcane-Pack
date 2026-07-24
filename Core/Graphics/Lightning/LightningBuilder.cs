using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.Lightning
{
    public struct LightningSetting(Vector2 begin, Vector2 end, Color color, float strength, float width, int lifetime, int generationsStep, float branchChance, int maxBranchGenerations,
        float distanceProtect = 100, float strengthDecay = 0.6f, float maxBranchAllowedDistance = 50f)
    {
        // 起点
        public Vector2 Begin = begin;
        // 终点
        public Vector2 End = end;
        // 辉光的颜色
        public Color color = color;
        // 扭曲强度
        public float strength = strength;
        // 宽度
        public float width = width;
        // 闪电宽度
        public int lifetime = lifetime;
        // 生成多少个节点
        public int GenerationsStep = generationsStep;
        // 分支生成概率
        public float BranchChance = branchChance;
        // 分支生成最大步进
        public int MaxBranchGenerations = maxBranchGenerations;
        // 主闪电每次迭代的强度衰减系数，范围为 0~1，越小衰减越快
        public float StrengthDecay = strengthDecay;
        // 分支允许偏离主干的最大距离
        public float MaxBranchAllowedDistance = maxBranchAllowedDistance;
        // 分支生成距离保护，确保两个分支不会距离太近
        public float DistanceProtect = distanceProtect;
    }
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
        public static LAPLightning GetFreeSlot()
        {
            for (int i = 0; i < LAPLightnings.Length; i++)
            {
                if (!LAPLightnings[i].Active)
                    return LAPLightnings[i];
            }
            return null;
        }
        public static LAPLightning SpawnLightning(in LightningSetting lightningSetting)
        {
            LAPLightning lightning = GetFreeSlot();
            if (lightning == null) 
                return null; // 超过上限
            lightning.Reset();
            lightning.Active = true;
            lightning.MaxlifeTime = lightningSetting.lifetime;
            lightning.Width = lightningSetting.width;
            lightning.Color = lightningSetting.color;
            lightning.Nodes = GeneratePath(lightningSetting);
            lightning.GenerateCache();
            HasAnyLightning = true;
            return lightning;
        }
        #region 生成路径
        /// <summary>
        /// 生成路径
        /// </summary>
        public static List<LightningNode> GeneratePath(in LightningSetting lightningSetting)
        {
            List<Vector2> points = new List<Vector2>();
            points.Add(lightningSetting.Begin);
            points.Add(lightningSetting.End);

            float currentStrength = lightningSetting.strength; // 用于在迭代中衰减的强度
            int generations = lightningSetting.GenerationsStep;
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

                    // 沿着法线方向进行随机偏移
                    float offset = Main.rand.NextFloat(-currentStrength, currentStrength);
                    mid += normal * offset + Main.rand.NextVector2Circular(15, 15);

                    _pathBuffer.Add(mid);
                    _pathBuffer.Add(end);
                }
                points = [.. _pathBuffer];
                currentStrength *= lightningSetting.StrengthDecay;
            }
            // 转换为LightningNode列表
            List<LightningNode> lightning = [];
            for (int i = 0; i < points.Count; i++)
            {
                float progressAlongPath = (float)i / lightning.Count;
                float currentWidth = lightningSetting.width * MathHelper.Lerp(1f, 0.2f, progressAlongPath);
                LightningNode node = new LightningNode(points[i], currentWidth, null);
                lightning.Add(node);
            }
            Vector2 PreSpawnPoint = Vector2.Zero;
            for (int i = 1; i < points.Count - 1;i++)
            {
                LightningNode node = lightning[i];
                if (Main.rand.NextFloat() < lightningSetting.BranchChance)
                {
                    if (PreSpawnPoint != Vector2.Zero && lightning[i].Position.Distance(PreSpawnPoint) < lightningSetting.DistanceProtect)
                        continue;
                    List<Vector2> childrens = SpawnLightningChildren(i, points, lightningSetting);
                    node.Children = childrens;
                    lightning[i] = node;
                    PreSpawnPoint = node.Position;
                }
            }
            return lightning;
        }
        #endregion
        public static List<Vector2> SpawnLightningChildren(int index, IReadOnlyList<Vector2> lightning, in LightningSetting lightningSetting)
        {
            List<Vector2> childrens = new List<Vector2>();
            // 太靠近末端不生成
            if (index >= lightning.Count - 2)
                return childrens;
            Vector2 startPos = lightning[index];
            childrens.Add(startPos);
            // 获取主干在当前节点的局部方向
            Vector2 trunkDir = LAPUtilities.GetVector2(startPos, lightningSetting.End);
            // 初始偏转角度
            float divergenceAngle = LAPRandom.GaussianRandom() * MathHelper.ToRadians(35);
            // 获取向量
            Vector2 currentDir = trunkDir.RotatedBy(divergenceAngle);
            Vector2 currentPos = startPos;
            // 决定分支步数，按主干剩余步数的一定比例，避免分支比主干长
            int remainingTrunkNodes = lightning.Count - index;
            int branchSteps = (int)(remainingTrunkNodes * Main.rand.NextFloat(0.2f, 0.4f));
            if (branchSteps < 3)
                return childrens; // 分支太短则舍弃
            // 估算生成步长
            float stepSize = Vector2.Distance(lightningSetting.Begin, lightningSetting.End) / lightning.Count;
            // 预先计算主干的向量信息和最大允许偏离半径
            Vector2 trunkVec = lightningSetting.End - lightningSetting.Begin;
            Vector2 trunkDirNorm = Vector2.Normalize(trunkVec);
            // 允许分支偏离主干的最大距离
            float maxAllowedDistance = lightningSetting.MaxBranchAllowedDistance;
            for (int i = 0; i < branchSteps; i++)
            {
                // 计算当前点指向最终目标的理想方向
                Vector2 dirToTarget = Vector2.Normalize(lightningSetting.End - currentPos);
                // 收敛权重：随着分支向外延伸，权重变大，强迫分支末端向终点弯曲收拢
                float convergeWeight = (float)i / branchSteps;
                // 混合当前方向与目标方向。0.5f为最大收敛强度，可以根据需求微调
                currentDir = Vector2.Normalize(Vector2.Lerp(currentDir, dirToTarget, convergeWeight * 0.5f));
                // 添加随机扰动，让分支保持电弧的曲折感
                float jitterAngle = LAPRandom.GaussianRandom() * MathHelper.ToRadians(15);
                currentDir = currentDir.RotatedBy(jitterAngle);
                // 计算当前点在主干方向上的投影长度
                Vector2 offsetFromBegin = currentPos - lightningSetting.Begin;
                float projection = Vector2.Dot(offsetFromBegin, trunkDirNorm);
                // 找到主干轴线上距离当前点最近的坐标点
                Vector2 closestPointOnTrunk = lightningSetting.Begin + trunkDirNorm * projection;
                // 计算当前点到主干的距离，以及指向主干的向量
                Vector2 toTrunkVec = closestPointOnTrunk - currentPos;
                float distanceToTrunk = toTrunkVec.Length();
                // 如果距离超出了允许的最大偏离阈值
                if (distanceToTrunk > maxAllowedDistance)
                {
                    // 算出向内拉回的方向
                    Vector2 inwardDir = toTrunkVec / distanceToTrunk; // 归一化
                    // 计算超出的程度 (越远拉力越大，限制在 0~1 之间)
                    // 这里的 maxAllowedDistance * 0.5f 是一个缓冲带宽度，决定了拉力增加的平滑度
                    float pullStrength = MathHelper.Clamp((distanceToTrunk - maxAllowedDistance) / (maxAllowedDistance * 0.5f), 0f, 1f);
                    // 强制修正currentDir，覆盖掉刚才可能导致它继续向外的随机扰动
                    // 0.6f是为了保留一点点向前的动量，避免闪电生硬地折成直角
                    currentDir = Vector2.Normalize(Vector2.Lerp(currentDir, inwardDir, pullStrength * 0.4f));
                }
                // 步进并记录节点
                currentPos += currentDir * stepSize + Main.rand.NextVector2Circular(9, 9);
                childrens.Add(currentPos);
            }
            return childrens;
        }
    }
}
