using Terraria.ModLoader;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        public static int GetMusicID(this Mod mod, string Path)
        {
            return MusicLoader.GetMusicSlot(mod, Path);
        }
    }
}
