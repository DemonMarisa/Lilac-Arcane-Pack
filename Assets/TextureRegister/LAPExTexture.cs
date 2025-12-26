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
        }
    }
}
