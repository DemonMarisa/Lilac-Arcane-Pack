using Terraria.ModLoader;

namespace LAP.Assets.TextureRegister
{
    public partial class LAPTextureRegister : ModSystem
    {
        public static string LiliesPath => "LAP/Assets/TextureRegister/LILES";
        public static Tex2DWithPath Butterfly { get; private set; }
        public static Tex2DWithPath Feather { get; private set; }
        public static Tex2DWithPath HoodTrail { get; private set; }
        public static Tex2DWithPath LilyLiquid { get; private set; }
        public static Tex2DWithPath LilySmoke { get; private set; }
        public static Tex2DWithPath Mowa { get; private set; }
        public static Tex2DWithPath Petal { get; private set; }
        public static Tex2DWithPath Ring04 { get; private set; }
        public static Tex2DWithPath Spirit { get; private set; }
        public static Tex2DWithPath Thrust01 { get; private set; }
        public static Tex2DWithPath Thrust02 { get; private set; }
        public static void LoadLILIESTextures()
        {
            Butterfly = new Tex2DWithPath($"{LiliesPath}/Butterfly");
            Feather = new Tex2DWithPath($"{LiliesPath}/Feather");
            HoodTrail = new Tex2DWithPath($"{LiliesPath}/HoodTrail");
            LilyLiquid = new Tex2DWithPath($"{LiliesPath}/LilyLiquid");
            LilySmoke = new Tex2DWithPath($"{LiliesPath}/LilySmoke");
            Mowa = new Tex2DWithPath($"{LiliesPath}/Mowa");
            Petal = new Tex2DWithPath($"{LiliesPath}/Petal");
            Ring04 = new Tex2DWithPath($"{LiliesPath}/Ring04");
            Spirit = new Tex2DWithPath($"{LiliesPath}/Spirit");
            Thrust01 = new Tex2DWithPath($"{LiliesPath}/Thrust01");
            Thrust02 = new Tex2DWithPath($"{LiliesPath}/Thrust02");
        }
        public static void UnLoadLILIESTextures()
        {
            Butterfly = null;
            Feather = null;
            HoodTrail = null;
            LilyLiquid = null;
            LilySmoke = null;
            Mowa = null;
            Petal = null;
            Ring04 = null;
            Spirit = null;
            Thrust01 = null;
            Thrust02 = null;
        }
    }
}
