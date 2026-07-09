using Terraria.ModLoader;

namespace LAP.Assets.TextureRegister
{
    public partial class LAPTextureRegister : ModSystem
    {
        public static string ExtraPath => "LAP/Assets/TextureRegister/ExtraTextures";
        public static Tex2DWithPath ShadowNebula { get; private set; }
        public static Tex2DWithPath RoughenEdgesLine { get; private set; }
        public static Tex2DWithPath BloomLine { get; private set; }
        public static Tex2DWithPath BinaryLine { get; private set; }
        public static Tex2DWithPath GlowLine { get; private set; }
        public static Tex2DWithPath StreakTrail { get; private set; }
        public static Tex2DWithPath FireNoise { get; private set; }
        public static Tex2DWithPath HarshNoise { get; private set; }
        public static Tex2DWithPath Noise { get; private set; }
        public static Tex2DWithPath Wood { get; private set; }
        public static Tex2DWithPath UltimaRayEnd { get; private set; }
        public static Tex2DWithPath UltimaRayMid { get; private set; }
        public static Tex2DWithPath UltimaRayStart { get; private set; }
        public static Tex2DWithPath StandardFlow1 { get; private set; }
        public static Tex2DWithPath StandardFlow2 { get; private set; }
        public static Tex2DWithPath StandardFlow3 { get; private set; }
        public static Tex2DWithPath StandardGradient { get; private set; }
        public static Tex2DWithPath OpticalFlaresLine { get; private set; }
        public static Tex2DWithPath DeathRay_T { get; private set; }
        public static Tex2DWithPath GigaBeam_T { get; private set; }
        public static Tex2DWithPath Lightning_T { get; private set; }
        public static Tex2DWithPath Lightning2_T { get; private set; }
        public static Tex2DWithPath NoiseLine { get; private set; }
        public static void LoadExtraTexture()
        {
            ShadowNebula = new Tex2DWithPath($"{ExtraPath}/MetaBallBG/ShadowNebula");
            RoughenEdgesLine = new Tex2DWithPath($"{ExtraPath}/RoughenEdgesLine");
            BloomLine = new Tex2DWithPath($"{ExtraPath}/BloomLine");
            BinaryLine = new Tex2DWithPath($"{ExtraPath}/BinaryLine");
            GlowLine = new Tex2DWithPath($"{ExtraPath}/GlowLine");
            StreakTrail = new Tex2DWithPath($"{ExtraPath}/StreakTrail");
            FireNoise = new Tex2DWithPath($"{ExtraPath}/FireNoise");
            HarshNoise = new Tex2DWithPath($"{ExtraPath}/HarshNoise");
            Noise = new Tex2DWithPath($"{ExtraPath}/Noise");
            Wood = new Tex2DWithPath($"{ExtraPath}/Wood");
            UltimaRayEnd = new Tex2DWithPath($"{ExtraPath}/Laser/UltimaRayEnd");
            UltimaRayMid = new Tex2DWithPath($"{ExtraPath}/Laser/UltimaRayMid");
            UltimaRayStart = new Tex2DWithPath($"{ExtraPath}/Laser/UltimaRayStart");
            StandardFlow1 = new Tex2DWithPath($"{ExtraPath}/StandardFlow1");
            StandardFlow2 = new Tex2DWithPath($"{ExtraPath}/StandardFlow2");
            StandardFlow3 = new Tex2DWithPath($"{ExtraPath}/StandardFlow3");
            StandardGradient = new Tex2DWithPath($"{ExtraPath}/StandardGradient");
            OpticalFlaresLine = new Tex2DWithPath($"{ExtraPath}/OpticalFlaresLine");
            DeathRay_T = new Tex2DWithPath($"{ExtraPath}/DeathRay_T");
            GigaBeam_T = new Tex2DWithPath($"{ExtraPath}/GigaBeam_T");
            Lightning_T = new Tex2DWithPath($"{ExtraPath}/Lightning_T");
            Lightning2_T = new Tex2DWithPath($"{ExtraPath}/Lightning2_T");
            NoiseLine = new Tex2DWithPath($"{ExtraPath}/NoiseLine");
        }
        public static void UnloadExtraTextures()
        {
            ShadowNebula = null;
            RoughenEdgesLine = null;
            BloomLine = null;
            BinaryLine = null;
            GlowLine = null;
            StreakTrail = null;
            FireNoise = null;
            HarshNoise = null;
            Noise = null;
            Wood = null;
            UltimaRayMid = null;
            UltimaRayStart = null;
            StandardFlow1 = null;
            StandardFlow2 = null;
            StandardFlow3 = null;
            StandardGradient = null;
            OpticalFlaresLine = null;
            DeathRay_T = null;
            GigaBeam_T = null;
            Lightning_T = null;
            Lightning2_T = null;
            NoiseLine = null;
        }
    }
}
