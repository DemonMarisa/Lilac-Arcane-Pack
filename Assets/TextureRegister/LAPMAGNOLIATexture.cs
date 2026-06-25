using Terraria.ModLoader;
using static CalamityMod.Skies.ExoMechsSky;

namespace LAP.Assets.TextureRegister
{
    public partial class LAPTextureRegister : ModSystem
    {
        public static string MAGNOLIAPath => "LAP/Assets/TextureRegister/MAGNOLIA";
        public static Tex2DWithPath Aura_01 { get; private set; }
        public static Tex2DWithPath Aura_02 { get; private set; }
        public static Tex2DWithPath BladeM { get; private set; }
        public static Tex2DWithPath Flash_01 { get; private set; }
        public static Tex2DWithPath Fire { get; private set; }
        public static Tex2DWithPath Cloud512_G { get; private set; }
        public static Tex2DWithPath MFlower { get; private set; }
        public static Tex2DWithPath Shockwave_01 { get; private set; }
        public static Tex2DWithPath DustGlow { get; private set; }
        public static Tex2DWithPath DustGlow_NB { get; private set; }
        public static Tex2DWithPath Lightning01 { get; private set; }
        public static Tex2DWithPath Lightning02 { get; private set; }
        public static Tex2DWithPath Lightning03 { get; private set; }
        public static Tex2DWithPath LightPoint { get; private set; }
        public static Tex2DWithPath LightPoint_NB { get; private set; }
        public static void LoadMAGNOLIATextures()
        {
            Aura_01 = new Tex2DWithPath($"{MAGNOLIAPath}/Aura_01");
            Aura_02 = new Tex2DWithPath($"{MAGNOLIAPath}/Aura_02");
            BladeM = new Tex2DWithPath($"{MAGNOLIAPath}/BladeM");
            Fire = new Tex2DWithPath($"{MAGNOLIAPath}/Fire");
            Flash_01 = new Tex2DWithPath($"{MAGNOLIAPath}/flash_01");
            Cloud512_G = new Tex2DWithPath($"{MAGNOLIAPath}/Cloud512_G");
            MFlower = new Tex2DWithPath($"{MAGNOLIAPath}/M_Flower");
            Shockwave_01 = new Tex2DWithPath($"{MAGNOLIAPath}/Shockwave_01");
            DustGlow = new Tex2DWithPath($"{MAGNOLIAPath}/DustGlow");
            DustGlow_NB = new Tex2DWithPath($"{MAGNOLIAPath}/DustGlow_NB");
            Lightning01 = new Tex2DWithPath($"{MAGNOLIAPath}/Lightning01");
            Lightning02 = new Tex2DWithPath($"{MAGNOLIAPath}/Lightning02");
            Lightning03 = new Tex2DWithPath($"{MAGNOLIAPath}/Lightning03");
            LightPoint = new Tex2DWithPath($"{MAGNOLIAPath}/LightPoint");
            LightPoint_NB = new Tex2DWithPath($"{MAGNOLIAPath}/LightPoint_NB");
        }
        public static void UnloadMAGNOLIATextures()
        {
            Aura_01 = null;
            Aura_02 = null;
            BladeM = null;
            Flash_01 = null;
            Fire = null;
            Cloud512_G = null;
            MFlower = null;
            Shockwave_01 = null;
            DustGlow = null;
            DustGlow_NB = null;
            Lightning01 = null;
            Lightning02 = null;
            Lightning03 = null;
            LightPoint = null;
            LightPoint_NB = null;
        }
    }
}
