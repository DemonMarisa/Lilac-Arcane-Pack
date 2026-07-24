using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace LAP.Core.Utilities
{
    public class LAPRandom : ILoadable
    {
        public static UnifiedRandom Random = new UnifiedRandom();
        void ILoadable.Load(Mod mod)
        {
        }

        void ILoadable.Unload()
        {
        }
        #region 随机与数学工具
        /// <summary>
        /// 使用 Box-Muller 变换生成符合正态分布的随机数
        /// 结果大多集中在 0 附近，范围大致在 -1 到 1 之间
        /// </summary>
        public static float GaussianRandom()
        {
            double u1 = 1.0 - Main.rand.NextDouble(); // 避免 u1 为 0 导致 Log 报错
            double u2 = 1.0 - Main.rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(MathHelper.TwoPi * u2);
            // 将其压缩到大致 -1 到 1 的范围，并限制极值
            return MathHelper.Clamp((float)randStdNormal * 0.3f, -1f, 1f);
        }
        #endregion
    }
}
