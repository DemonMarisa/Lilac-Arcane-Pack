using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace LAP.Assets.Effects
{
    public partial class LAPShaderRegister : ModSystem
    {
        public static Asset<Effect> Pixelation { get; private set; }
        public static Asset<Effect> ScreenDistortion { get; private set; }
        public static Asset<Effect> DeepGlow { get; private set; }
        public static Asset<Effect> ScreenCausticsShader { get; private set; }
        public static void LoadScreen()
        {
            Pixelation = LoadScreenShader("Pixelation");
            ScreenDistortion = LoadScreenShader("ScreenDistortion");
            DeepGlow = LoadScreenShader("DeepGlow");
            ScreenCausticsShader = LoadScreenShader("ScreenCausticsShader");
        }
        public static void UnLoadScreen()
        {
            Pixelation = null;
            ScreenDistortion = null;
            DeepGlow = null;
            ScreenCausticsShader = null;
        }
        public static Asset<Effect> LoadScreenShader(string path)
        {
            return Request<Effect>($"{ScreenShaderPath}{path}");
        }

    }
}
