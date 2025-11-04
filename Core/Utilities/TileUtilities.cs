using Terraria;
using Terraria.GameContent.Drawing;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        /// <summary>
        /// 判定音乐盒是否开启
        /// 因为音乐盒Xframe开启后会变为0，关闭为36
        /// </summary>
        /// <传入的Tile name="tile"></param>
        /// <returns></returns>
        public static bool MusicBoxOFF(Tile tile)
        {
            return !TileDrawing.IsVisible(tile) || tile.TileFrameX != 36;
        }
    }
}
