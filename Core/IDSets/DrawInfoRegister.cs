using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace LAP.Core.IDSets
{
    public partial class LAPIDSet : ModSystem
    {        
        /// <summary>
        /// 记录弹幕的XY帧数，因为原版默认只有Y，所以单独弄一个XY的，这样也便于做多人同步，不需要每次都在弹幕中更新
        /// </summary>
        public static Dictionary<int, Point> ProjFrame = [];
        public static void UnloadProjFrame()
        {
            ProjFrame = [];
        }
    }
}
