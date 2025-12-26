using LAP.Assets.TextureRegister;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LAP.Core.LAPUI.FocusBar
{
    public class FocusBarPart
    {
        public class BarLeftBG
        {
            public Vector2 Position;
            public SpriteEffects SE = SpriteEffects.None;
            public Vector2 Orig;
            public Texture2D texture => LAPTextureRegister.FocusUIEdge.Value;
        }
        public class BarRightBG
        {
            public Vector2 Position;
            public SpriteEffects SE = SpriteEffects.FlipHorizontally;
            public Vector2 Orig;
            public Texture2D texture => LAPTextureRegister.FocusUIEdge.Value;
        }
        public class BarMiddleBG
        {
            public Vector2 Position;
            public Vector2 Orig;
            public Texture2D texture => LAPTextureRegister.FocusUIMiddle.Value;
        }
        public class Bar
        {
            public Vector2 Position;
            public Vector2 Orig;
            public Texture2D texture => LAPTextureRegister.BigWhiteCube.Value;
        }
        public class BarTopPattern
        {
            public Vector2 Position;
            public float Length;
            public Vector2 Orig;
            public Texture2D texture => LAPTextureRegister.TopPattern.Value;
        }
        public class BarBottomPattern
        {
            public Vector2 Position;
            public float Length;
            public Vector2 Orig;
            public Texture2D texture => LAPTextureRegister.BottomPattern.Value;
        }
    }
}
