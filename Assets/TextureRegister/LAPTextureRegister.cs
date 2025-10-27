using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace LAP.Assets.TextureRegister
{
    public class LAPTextureRegister : ModSystem
    {
        public static string InvisibleTexturePath => "UCA/Assets/Textures/InvisibleProj";
        public static Asset<Texture2D> ShadowNebula { get; private set; }
        public static Asset<Texture2D> InvisibleProj { get; private set; }
        public static Asset<Texture2D> WhiteCircle { get; private set; }
        public static Asset<Texture2D> WhiteCube { get; private set; }
        public override void Load()
        {
            ShadowNebula = ModContent.Request<Texture2D>($"LAP/Assets/TextureRegister/ExtraTextures/MetaBallBG/ShadowNebula");
            InvisibleProj = ModContent.Request<Texture2D>($"LAP/Assets/TextureRegister/Textures/InvisibleProj");
            WhiteCircle = ModContent.Request<Texture2D>($"LAP/Assets/TextureRegister/Textures/WhiteCircle");
            WhiteCube = ModContent.Request<Texture2D>($"LAP/Assets/TextureRegister/Textures/WhiteCube");
        }
        public override void Unload()
        {
            ShadowNebula = null;
            InvisibleProj = null;
            WhiteCircle = null;
            WhiteCube = null;
        }
    }
}
