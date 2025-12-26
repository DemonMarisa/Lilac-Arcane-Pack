using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Assets.TextureRegister
{
    public class Tex2DWithPath
    {
        public Asset<Texture2D> Texture { get; }
        public string Path { get; }
        public Tex2DWithPath(Asset<Texture2D> texture, string path)
        {
            Path = path;
            Texture = texture;
        }
        public Tex2DWithPath(string path)
        {
            Path = path;
            Texture = ModContent.Request<Texture2D>($"{Path}");
        }
        public Texture2D Value => Texture.Value;
        public int Height => Texture.Height();
        public int Width => Texture.Width();
        public Vector2 Size()
        {
            return new Vector2(Value.Width, Value.Height);
        }
    }
    public partial class LAPTextureRegister : ModSystem
    {
        public static string InvisibleTexturePath => "LAP/Assets/TextureRegister/Textures/InvisibleProj";
        public static Tex2DWithPath InvisibleProj { get; private set; }
        public static Tex2DWithPath WhiteCircle { get; private set; }
        public static Tex2DWithPath WhiteCube { get; private set; }
        public override void Load()
        {
            InvisibleProj = new Tex2DWithPath($"LAP/Assets/TextureRegister/Textures/InvisibleProj");
            WhiteCircle = new Tex2DWithPath($"LAP/Assets/TextureRegister/Textures/WhiteCircle");
            WhiteCube = new Tex2DWithPath($"LAP/Assets/TextureRegister/Textures/WhiteCube");
            LoadMAGNOLIATextures();
            LoadExp33Texture();
            LoadExtraTexture();
            LoadParticleTextures();
            LoadLILIESTextures();
            LoadUI();
        }
        public override void Unload()
        {
            InvisibleProj = null;
            WhiteCircle = null;
            WhiteCube = null;
            UnloadMAGNOLIATextures();
            UnloadExp33Textures();
            UnloadExtraTextures();
            UnloadParticleTextures();
            UnLoadLILIESTextures();
            UnloadUI();
        }
    }
}
