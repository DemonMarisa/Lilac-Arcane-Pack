using Terraria.ModLoader;

namespace LAP.Assets.TextureRegister
{
    public partial class LAPTextureRegister : ModSystem
    {
        public static string ParticlePath => "LAP/Assets/TextureRegister/ParticleTextures";
        public static Tex2DWithPath PointLight { get; private set; }
        public static Tex2DWithPath StarLine1 { get; private set; }
        public static Tex2DWithPath StarLine2 { get; private set; }
        public static Tex2DWithPath StarLine3 { get; private set; }
        public static Tex2DWithPath GlowStar { get; private set; }
        public static Tex2DWithPath SmallGlowBall { get; private set; }
        public static Tex2DWithPath BigWhiteCube { get; private set; }
        public static void LoadParticleTextures()
        {
            PointLight = new Tex2DWithPath($"{ParticlePath}/PointLight01");
            StarLine1 = new Tex2DWithPath($"{ParticlePath}/StarLine1");
            StarLine2 = new Tex2DWithPath($"{ParticlePath}/StarLine2");
            StarLine3 = new Tex2DWithPath($"{ParticlePath}/StarLine3");
            GlowStar = new Tex2DWithPath($"{ParticlePath}/GlowStar");
            SmallGlowBall = new Tex2DWithPath($"{ParticlePath}/SmallGlowBall");
            BigWhiteCube = new Tex2DWithPath($"{ParticlePath}/SmallGlowBall");
        }
        public static void UnloadParticleTextures()
        {
            PointLight = null;
            StarLine1 = null;
            StarLine2 = null;
            StarLine3 = null;
            GlowStar = null;
            SmallGlowBall = null;
            BigWhiteCube = null;
        }
    }
}
