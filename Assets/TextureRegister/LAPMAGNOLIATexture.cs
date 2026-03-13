using Terraria.ModLoader;

namespace LAP.Assets.TextureRegister
{
    public partial class LAPTextureRegister : ModSystem
    {
        public static string MAGNOLIAPath => "LAP/Assets/TextureRegister/MAGNOLIA";
        public static Tex2DWithPath BladeM { get; private set; }
        public static Tex2DWithPath Fire { get; private set; }
        public static Tex2DWithPath Cloud512_G { get; private set; }
        public static Tex2DWithPath MFlower { get; private set; }
        public static Tex2DWithPath Shockwave_01 { get; private set; }
        public static void LoadMAGNOLIATextures()
        {
            BladeM = new Tex2DWithPath($"{MAGNOLIAPath}/BladeM");
            Fire = new Tex2DWithPath($"{MAGNOLIAPath}/Fire");
            Cloud512_G = new Tex2DWithPath($"{MAGNOLIAPath}/Cloud512_G");
            MFlower = new Tex2DWithPath($"{MAGNOLIAPath}/M_Flower");
            Shockwave_01 = new Tex2DWithPath($"{MAGNOLIAPath}/Shockwave_01");
        }
        public static void UnloadMAGNOLIATextures()
        {
            BladeM = null;
            Fire = null;
            Cloud512_G = null;
            MFlower = null;
            Shockwave_01 = null;
        }
    }
}
