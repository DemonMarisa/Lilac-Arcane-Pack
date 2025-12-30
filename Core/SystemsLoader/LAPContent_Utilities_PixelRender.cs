using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        public static void ReSetToEndShader_Pixel()
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null);
        }
        public static void ReSetToBeginShader_Pixel(BlendState blendState)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, blendState, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null);
        }
    }
}
