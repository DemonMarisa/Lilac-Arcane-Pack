using LAP.Core.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LAP.Core.Graphics.Primitives.Trail
{
    // 记录轨迹点的数据
    public readonly struct TrailDrawData(Vector2 pos, Color color, float height, float rot)
    {
        public readonly Vector2 Position = pos;
        public readonly Color DrawColor = color;
        public readonly float Height = height;
        public readonly float Rotation = rot;
    }

    public struct DrawSetting(Texture2D Texture, bool smoothUV = false, int smoothSegments = -1, TrailEffects trailEffect = TrailEffects.None, SamplerState samplerState = null, Effect effect = null, int applyPass = 0, float xuvOffset = 0)
    {
        public Texture2D texture = Texture;
        public SamplerState samplerState = samplerState ?? SamplerState.LinearWrap;
        public TrailEffects trailEffect = trailEffect;
        // 是否对UV进行平滑处理，开启后会根据点之间的距离重新计算UV坐标，使纹理在拉伸时更均匀，减少明显的拉伸失真
        public bool smoothUV = smoothUV;
        // 平滑点的插值段数，默认为-1表示不进行位置平滑，设置为大于0的值会在原始点之间插入额外的点进行平滑处理，数值越大平滑效果越明显但性能开销也越大
        public int smoothSegments = smoothSegments;
        // shader
        public Effect effect = effect;
        // 需要应用的shader pass索引
        public int applyPass = applyPass;
        // 用于移动贴图
        public float xuvOffset = xuvOffset;
    }
}
