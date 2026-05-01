using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace LAP.Assets.Effects
{
    public partial class LAPShaderRegister : ModSystem
    {
        public static Asset<Effect> AlphaFade { get; private set; }
        public static Asset<Effect> AlphaFade_Noise { get; private set; }
        public static Asset<Effect> AlphaFade_Noise_OColor { get; private set; }
        public static Asset<Effect> AlphaFade_OColor { get; private set; }
        public static Asset<Effect> DeepGlow { get; private set; }
        public static void Load2()
        {
            AlphaFade = LoadShader("AlphaFade/AlphaFade");
            AlphaFade_Noise = LoadShader("AlphaFade/AlphaFade_Noise");
            AlphaFade_Noise_OColor = LoadShader("AlphaFade/AlphaFade_Noise_OColor");
            AlphaFade_OColor = LoadShader("AlphaFade/AlphaFade_OColor");
            DeepGlow = LoadScreenShader("DeepGlow");
        }
        public static void UnLoad2()
        {
            AlphaFade = null;
            AlphaFade_Noise = null;
            AlphaFade_Noise_OColor = null;
            AlphaFade_OColor = null;
            DeepGlow = null;
        }
    }
}
