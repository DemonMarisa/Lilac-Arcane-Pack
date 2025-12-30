using Terraria.ModLoader;

namespace LAP.Assets.TextureRegister
{
    public partial class LAPTextureRegister : ModSystem
    {
        public static string MAGNOLIAPath => "LAP/Assets/TextureRegister/MAGNOLIA";
        public static Tex2DWithPath BladeM { get; private set; }
        public static Tex2DWithPath Fire { get; private set; }
        public static void LoadMAGNOLIATextures()
        {
            BladeM = new Tex2DWithPath($"{MAGNOLIAPath}/BladeM");
            Fire = new Tex2DWithPath($"{MAGNOLIAPath}/Fire");
        }
        public static void UnloadMAGNOLIATextures()
        {
            BladeM = null;
            Fire = null;
        }
    }
}
