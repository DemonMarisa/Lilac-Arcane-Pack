using LAP.Core.Graphics.LAPPostProgress;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        public static void ActivePostProgress<T>() where T : LAPPostProgress
        {
            if (LAPPostProgressManager.ActivePostProgress.Contains(GetInstance<T>()))
                return;
            GetInstance<T>().OnActive();
            LAPPostProgressManager.ActivePostProgress.Add(GetInstance<T>());
        }
    }
}
