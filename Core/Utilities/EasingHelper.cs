using Microsoft.Xna.Framework;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LAP.Core.Utilities
{
    public static class EasingHelper
    {
        /// <summary>
        /// 缓动函数工具类
        ///  变量 t 表示 0（动画开始）到 1（动画结束）范围内的值。
        /// 详见 https://easings.net/zh-cn
        /// </summary>            
        // 二次缓入缓出
        public static float EaseInOutQuad(float t)
            => t < 0.5f ? 2f * t * t : 1f - (-2f * t + 2f) * (-2f * t + 2f) / 2f;
        // 指数缓出
        public static float EaseOutExpo(float t)
            => t == 1f ? 1f : 1f - MathF.Pow(2f, -10f * t);
        // 指数缓入缓出
        public static float EaseInOutExpo(float t)
            => t < 0.5f ? 2 * t * t : 1 - MathF.Pow(-2 * t + 2, 2) / 2;
        public static float EaseInCubic(float t)
            => t * t * t;
        public static float EaseOutCubic(float t)
            => 1 - MathF.Pow(1 - t, 3);
        public static float EaseOutBack(float t)
        {
            if (t == 1)
                return 1;
            const float c1 = 1.70158f;
            const float c3 = c1 + 1;

            return 1 + c3 * MathF.Pow(t - 1, 3) + c1 * MathF.Pow(t - 1, 2);
        }
        public static float EaseInBack(float t)
        {
            if (t == 1)
                return 1;
            const float c1 = 1.70158f;
            const float c3 = c1 + 1;

            return c3 * t * t * t - c1 * t * t;
        }
        public static float EaseOutElastic(float t)
        {
            if (t == 0)
                return 0;
            if (t == 1)
                return 1;
            const float c4 = MathHelper.TwoPi / 3f;
            return 1 + MathF.Pow(2f, -10f * t) * MathF.Sin((t - c4) * c4);
        }
        public static float EaseInElastic(float t)
        {
            if (t == 0)
                return 0;
            if (t == 1)
                return 1;
            const float c4 = MathHelper.TwoPi / 3f;
            return -MathF.Pow(2, 10 * t - 10) * MathF.Sin((t * 10f - 10.75f) * c4);
        }
    }
    public static class BezierEaseHelper
    {
        /// <summary>
        /// 一维贝塞尔曲线，我觉得没什么用处
        /// </summary>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float Bezier1D(float y1, float y2, float t)
        {
            float invT = 1f - t;
            return 3f * invT * invT * t * y1 + 3f * invT * t * t * y2 + t * t * t;
        }
        /// <summary>
        /// 贝塞尔曲线计算函数，给定四个点和一个t值，返回曲线上对应的点
        /// </summary>
        /// <param name="p0">起点</param>
        /// <param name="p1">控制p0</param>
        /// <param name="p2">控制p3</param>
        /// <param name="p3">终点</param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector2 BezierCurve(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
        {
            float invT = 1f - t;
            float invT2 = invT * invT;
            float invT3 = invT2 * invT;
            float t2 = t * t;
            float t3 = t2 * t;
            return invT3 * p0 + 3f * invT2 * t * p1 + 3f * invT * t2 * p2 + t3 * p3;
        }
        // 因为不能直接通过X映射到Y，所以需要通过二分法查找对应的t值，再计算Y值
        /// <summary>
        /// 贝塞尔曲线缓动函数，开销更大，但是用于一些更高级的动画曲线需求
        /// 传入与传出的点不得超过 (0,0) 和 (1,1) 的范围，否则会出现Bug
        /// </summary>
        /// <param name="p1">控制点1的坐标 (0-1范围内)</param>
        /// <param name="p2">控制点2的坐标 (0-1范围内)</param>
        /// <param name="timeX">当前的线性时间进度 (0.0f - 1.0f)</param>
        /// <returns>动画在这个时间点应该到达的 Y 值</returns>
        public static float BezierSmooth(Vector2 p1, Vector2 p2, float timeX)
        {
            p1.X = MathHelper.Clamp(p1.X, 0f, 1f);
            p2.X = MathHelper.Clamp(p2.X, 0f, 1f);
            timeX = MathHelper.Clamp(timeX, 0f, 1f);
            // 1. 通过当前的线性时间 timeX，反求出对应的曲线参数 t
            float t = FindVForU(p1.X, p2.X, timeX);
            // 2. 将求得的 t 代入 Y 轴方程，得出真正的动画进度 Y
            return CalculateBezierCoordinate(p1.Y, p2.Y, t);
        }
        // P0=0, P3=1 时的简化公式
        private static float CalculateBezierCoordinate(float p1, float p2, float t)
        {
            float invT = 1f - t;
            return 3f * invT * invT * t * p1 + 3f * invT * t * t * p2 + t * t * t;
        }
        // 二分法查找，根据给定的 X 寻找对应的 t
        private static float FindVForU(float p1x, float p2x, float targetX)
        {
            float lower = 0f;
            float upper = 1f;
            float t = targetX; // 初始猜测值设为 targetX
            for (int i = 0; i < 8; i++)
            {
                float currentX = CalculateBezierCoordinate(p1x, p2x, t);
                // 精度达到0.001就够了
                if (Math.Abs(currentX - targetX) < 0.001f)
                    return t;
                if (currentX < targetX)
                    lower = t;
                else
                    upper = t;
                t = (upper + lower) * 0.5f;
            }
            return t;
        }
    }
}
