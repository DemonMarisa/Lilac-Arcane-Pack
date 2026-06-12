using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LAP.Core.Graphics.Primitives.Trail
{
    public readonly struct VertexPositionColorTexture2D(Vector2 position, Color color, Vector3 textureCoordinates) : IVertexType
    {
        public readonly Vector2 Position = position;

        public readonly Color Color = color;
        // 用 Vector3 来存储纹理坐标，第三个分量可以用来存储额外的数据
        public readonly Vector3 TextureCoordinates = textureCoordinates;
        public VertexDeclaration VertexDeclaration => VertexDeclaration2D;

        public static readonly VertexDeclaration VertexDeclaration2D = new(
        [
            new(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
            new(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
            new(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
        ]);
    }
}
