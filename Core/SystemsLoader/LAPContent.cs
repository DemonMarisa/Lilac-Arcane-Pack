using LAP.Core.GlobalInstance.Players.DashSystem;
using LAP.Core.LAPUI.CustomCD;
using LAP.Core.MetaBallsSystem;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        public static int MetaBallType<T>() where T : BaseMetaBall => GetInstance<T>()?.Type ?? 0;
        public static int CDType<T>() where T : BaseCD => GetInstance<T>()?.Type ?? 0;
        public static int DashType<T>() where T : BasePlayerDash => GetInstance<T>()?.Type ?? 0;
    }
}
