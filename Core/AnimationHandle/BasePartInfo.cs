using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LAP.Core.AnimationHandle
{
    public struct BasePartInfo(Texture2D texture, Vector2 position, Vector2 velocity, float rotation, Vector2 origin)
    {
        public Texture2D Texture = texture;
        public Vector2 Position = position;
        public Vector2 Velocity = velocity;
        public float Rotation = rotation;
        public Vector2 Origin = origin;
    }
}
