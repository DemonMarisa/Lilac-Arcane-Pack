using LAP.Core.MetaBallsSystem;
using Terraria.ModLoader;

namespace UCA.Core.SystemsLoader
{
    public static class UCAMetaBall
    {
        public static int MetaBallType<T>() where T : BaseMetaBall => ModContent.GetInstance<T>()?.Type ?? 0;
    }
}
