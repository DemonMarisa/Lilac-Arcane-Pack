using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace LAP.Assets.TextureRegister
{
    public partial class LAPTextureRegister : ModSystem
    {
        public static string MAGNOLIAPath => "LAP/Assets/TextureRegister/MAGNOLIA";
        public static Tex2DWithPath BladeM { get; private set; }
        public static string BladeMPath => $"{MAGNOLIAPath}/BladeM";
        public static void LoadMAGNOLIATextures()
        {
            BladeM = new Tex2DWithPath($"{MAGNOLIAPath}/BladeM");
        }
        public static void UnloadMAGNOLIATextures()
        {
            BladeM = null;
        }
    }
}
