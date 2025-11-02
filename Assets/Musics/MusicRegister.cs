using Terraria.ModLoader;

namespace LAP.Assets.Musics
{
    public class MusicRegister : ModSystem
    {
        public static string MainThemeMagnoliaPath = "Assets/Musics/Misc/TestBGM";
        public static string SliencePath = "Assets/Musics/Misc/Silence";
        public override void Load()
        {
            MusicLoader.AddMusic(Mod, SliencePath);
            MusicLoader.AddMusic(Mod, MainThemeMagnoliaPath);
        }
    }
}
