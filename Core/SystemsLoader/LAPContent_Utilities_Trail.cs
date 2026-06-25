using LAP.Core.Graphics.Primitives.Trail;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        /// <summary>
        /// 只需要传入位置就可以画拖尾
        /// </summary>
        /// <param name="OldPos"></param>
        /// <param name="offset">每一个点的offset</param>
        /// <param name="color"></param>
        /// <param name="height">统一的高度</param>
        /// <param name="setting"></param>
        public static void AutoRotTrail(IReadOnlyList<Vector2> OldPos, Vector2 offset, Color color, float height, DrawSetting setting)
        {
            List<Vector2> getPos = [];
            for (int i = 0; i < OldPos.Count; i++)
            {
                if (OldPos[i] != Vector2.Zero)
                {
                    getPos.Add(OldPos[i]);
                }
            }
            List<TrailDrawData> trailDrawData = [];
            float rot = 0;
            for (int i = 0; i < getPos.Count; i++)
            {
                if (i < getPos.Count - 1)
                    rot = LAPUtilities.GetVector2(getPos[i], getPos[i + 1]).ToRotation();
                trailDrawData.Add(new TrailDrawData(getPos[i] + offset - Main.screenPosition, color, height, rot));
            }
            TrailRender.RenderTrail(trailDrawData, setting);
        }
        /// <summary>
        /// 只需要传入位置就可以画拖尾
        /// </summary>
        /// <param name="OldPos"></param>
        /// <param name="offset">每一个点的offset</param>
        /// <param name="color"></param>
        /// <param name="height">与标准一致，但是可以单独控制每一个的高度</param>
        /// <param name="setting"></param>
        public static void AutoRotTrail(IReadOnlyList<Vector2> OldPos, Vector2 offset, Color color, IReadOnlyList<float> height, DrawSetting setting)
        {
            List<Vector2> getPos = [];
            for (int i = 0; i < OldPos.Count; i++)
            {
                if (OldPos[i] != Vector2.Zero)
                {
                    getPos.Add(OldPos[i]);
                }
            }
            List<TrailDrawData> trailDrawData = [];
            float rot = 0;
            for (int i = 0; i < getPos.Count; i++)
            {
                if (i < getPos.Count - 1)
                    rot = LAPUtilities.GetVector2(getPos[i], getPos[i + 1]).ToRotation();
                trailDrawData.Add(new TrailDrawData(getPos[i] + offset - Main.screenPosition, color, height[i], rot));
            }
            TrailRender.RenderTrail(trailDrawData, setting);
        }
        // 这两个数组一定要保证长度相同，否则会越界
        public static void DrawTrail(IReadOnlyList<Vector2> OldPos, IReadOnlyList<float> OldRots, Vector2 offset, Color color, float height, DrawSetting setting)
        {
            List<Vector2> getPos = [];
            List<float> getRots = [];
            for (int i = 0; i < OldPos.Count; i++)
            {
                if (OldPos[i] != Vector2.Zero)
                {
                    getPos.Add(OldPos[i]);
                    getRots.Add(OldRots[i]);
                }
            }
            List<TrailDrawData> trailDrawData = [];
            for (int i = 0; i < getPos.Count; i++)
            {
                trailDrawData.Add(new TrailDrawData(OldPos[i] + offset - Main.screenPosition, color, height, getRots[i]));
            }
            TrailRender.RenderTrail(trailDrawData, setting);
        }
        public static void DrawTrail(IReadOnlyList<Vector2> OldPos, IReadOnlyList<float> OldRots, Vector2 offset, Color color, IReadOnlyList<float> height, DrawSetting setting)
        {
            List<Vector2> getPos = [];
            List<float> getRots = [];
            for (int i = 0; i < OldPos.Count; i++)
            {
                if (OldPos[i] != Vector2.Zero)
                {
                    getPos.Add(OldPos[i]);
                    getRots.Add(OldRots[i]);
                }
            }
            List<TrailDrawData> trailDrawData = [];
            for (int i = 0; i < getPos.Count; i++)
            {
                trailDrawData.Add(new TrailDrawData(OldPos[i] + offset - Main.screenPosition, color, height[i], getRots[i]));
            }
            TrailRender.RenderTrail(trailDrawData, setting);
        }
    }
}
