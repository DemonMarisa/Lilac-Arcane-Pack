using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LAP.Core.Graphics.Primitives.Trail
{
    public struct TrailDrawDate(Vector2 drawPos, Color drawColor, Vector2 primitivesHeight, float primitivesHeightRot)
    {
        /// <summary>
        /// 传入的世界坐标
        /// </summary>
        public Vector2 PosDate = drawPos;
        /// <summary>
        /// 传入每个点的颜色
        /// </summary>
        public Color DrawColor = drawColor;
        /// <summary>
        /// 顶点的偏移
        /// </summary>
        public Vector2 PrimitivesOffset = primitivesHeight;
        /// <summary>
        /// 顶点偏移的整体旋转
        /// </summary>
        public float PrimitivesHeightRot = primitivesHeightRot;
    }

    public struct DrawSetting
    {
        public DrawSetting(Texture2D texture)
        {
            texture2d = texture;
            sampler = SamplerState.PointWrap;
        }
        public DrawSetting(Texture2D texture, SamplerState samplerState)
        {
            texture2d = texture;
            sampler = samplerState;
        }
        public Texture2D texture2d;
        public SamplerState sampler;
    }
}
