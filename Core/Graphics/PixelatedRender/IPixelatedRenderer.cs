using LAP.Core.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace LAP.Core.Graphics.PixelatedRender
{
    /// <summary>
    /// 只支持BeforePlayers与BeforeDusts图层
    /// </summary>
    public interface IPixelatedRenderer
    {
        BlendState BlendState => BlendState.AlphaBlend;
        DrawLayer LayerToRenderTo => DrawLayer.BeforeDusts;
        void RenderPixelated(SpriteBatch spriteBatch);
    }
}
