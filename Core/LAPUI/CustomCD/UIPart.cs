using LAP.Assets.TextureRegister;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LAP.Core.LAPUI.CustomCD
{
    public class LeftArrow
    {
        public Vector2 Position;
        public float Opacity = 1f;
        public SpriteEffects SE = SpriteEffects.FlipHorizontally;
        public Vector2 Orig;
        public Texture2D texture => LAPTextureRegister.CDBG_Edge.Value;
    }
    public class RightArrow
    {
        public Vector2 Position;
        public float Opacity = 1f;
        public SpriteEffects SE = SpriteEffects.None;
        public Vector2 Orig;
        public Texture2D texture => LAPTextureRegister.CDBG_Edge.Value;
    }
    public class MiddleBG
    {
        public Vector2 Position;
        public float Length;
        public Vector2 Orig;
        public Rectangle rectangle => new Rectangle(0, 0, (int)Length + 2, LAPTextureRegister.CDBG_Middle.Height);
        public Texture2D texture => LAPTextureRegister.CDBG_Middle.Value;
    }
}
