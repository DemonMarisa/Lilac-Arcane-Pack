using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace LAP.Assets.Effects
{
    public partial class LAPShaderRegister : ModSystem
    {
        public static Asset<Effect> OverlayPass { get; private set; }
        public static void LoadColor()
        {
            OverlayPass = LoadColorShader("OverlayPass");
        }
        public static void UnLoadColor()
        {
            OverlayPass = null;
        }
        public static Asset<Effect> LoadColorShader(string path)
        {
            return Request<Effect>($"{ColorShadersPath}{path}");
        }
    }
}
