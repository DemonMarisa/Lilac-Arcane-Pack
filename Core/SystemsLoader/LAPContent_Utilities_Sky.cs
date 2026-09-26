using LAP.Core.Graphics.LAPCustomSky;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        public static void ActiveSky<T>() where T : LAPCustomSky
        {
            if (LAPCustomSkyManager.ActiveCustomSkies.Contains(GetInstance<T>()))
                return;
            GetInstance<T>().OnActive();
            LAPCustomSkyManager.ActiveCustomSkies.Add(GetInstance<T>());
        }
    }
}
