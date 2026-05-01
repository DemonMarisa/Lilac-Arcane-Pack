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
        public static Tex2DWithPath MediumGlowBall { get; private set; }
        public static Tex2DWithPath StarProj { get; private set; }
        public static Tex2DWithPath BloomLine2 { get; private set; }
        public static Tex2DWithPath BloomLine2Cap { get; private set; }
        public static Tex2DWithPath BloomCircle { get; private set; }
        public static Tex2DWithPath Sparkle { get; private set; }
        public static Tex2DWithPath Lozenge_Glow { get; private set; }
        public static void LoadParticleTextures()
        {
            PointLight = new Tex2DWithPath($"{ParticlePath}/PointLight01");
            StarLine1 = new Tex2DWithPath($"{ParticlePath}/StarLine1");
            StarLine2 = new Tex2DWithPath($"{ParticlePath}/StarLine2");
            StarLine3 = new Tex2DWithPath($"{ParticlePath}/StarLine3");
            GlowStar = new Tex2DWithPath($"{ParticlePath}/GlowStar");
            SmallGlowBall = new Tex2DWithPath($"{ParticlePath}/SmallGlowBall");
            MediumGlowBall = new Tex2DWithPath($"{ParticlePath}/MediumGlowBall");
            StarProj = new Tex2DWithPath($"{ParticlePath}/StarProj");
            BloomLine2 = new Tex2DWithPath($"{ParticlePath}/BloomLine2");
            BloomLine2Cap = new Tex2DWithPath($"{ParticlePath}/BloomLine2Cap");
            BloomCircle = new Tex2DWithPath($"{ParticlePath}/BloomCircle");
            Sparkle = new Tex2DWithPath($"{ParticlePath}/Sparkle");
            Lozenge_Glow = new Tex2DWithPath($"{ParticlePath}/Lozenge_Glow");
        }
        public static void UnloadParticleTextures()
        {
            PointLight = null;
            StarLine1 = null;
            StarLine2 = null;
            StarLine3 = null;
            GlowStar = null;
            SmallGlowBall = null;
            MediumGlowBall = null;
            StarProj = null;
            BloomLine2 = null;
            BloomLine2Cap = null;
            BloomCircle = null;
            Sparkle = null;
            Lozenge_Glow = null;
        }
    }
}
