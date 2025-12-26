using LAP.Core.LAPUI.CustomCD;
using LAP.Core.MetaBallsSystem;
using Terraria.ModLoader;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        public static int MetaBallType<T>() where T : BaseMetaBall => ModContent.GetInstance<T>()?.Type ?? 0;
        public static int CDType<T>() where T : BaseCD => ModContent.GetInstance<T>()?.Type ?? 0;
    }
}
