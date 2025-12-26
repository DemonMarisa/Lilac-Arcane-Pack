using Terraria.ModLoader;

namespace LAP.Assets.TextureRegister
{
    public partial class LAPTextureRegister : ModSystem
    {
        public static string Exp33Path => "LAP/Assets/TextureRegister/Expedition33";
        public static Tex2DWithPath SiMengBlade { get; private set; }
        public static Tex2DWithPath SiMengBlade_A { get; private set; }
        public static void LoadExp33Texture()
        {
            SiMengBlade = new Tex2DWithPath($"{Exp33Path}/SiMengBlade");
            SiMengBlade_A = new Tex2DWithPath($"{Exp33Path}/SiMengBlade_A");
        }
        public static void UnloadExp33Textures()
        {
            SiMengBlade = null;
            SiMengBlade_A = null;
        }
    }
}
