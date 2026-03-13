using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace LAP.Assets.Effects
{
    public partial class LAPShaderRegister : ModSystem
    {
        public static Asset<Effect> AlphaFade { get; private set; }
        public static Asset<Effect> AlphaFade_ACut { get; private set; }
        public static Asset<Effect> AlphaFade_ACut_OColor { get; private set; }
        public static Asset<Effect> AlphaFade_OColor { get; private set; }
        public static void Load2()
        {
            AlphaFade = LoadShader("AlphaFade/AlphaFade");
            RegisterMiscShader(AlphaFade, "Pass0", "AlphaFade");

            AlphaFade_ACut = LoadShader("AlphaFade/AlphaFade_ACut");
            RegisterMiscShader(AlphaFade_ACut, "Pass0", "AlphaFade_ACut");

            AlphaFade_ACut_OColor = LoadShader("AlphaFade/AlphaFade_ACut_OColor");
            RegisterMiscShader(AlphaFade_ACut_OColor, "Pass0", "AlphaFade_ACut_OColor");

            AlphaFade_OColor = LoadShader("AlphaFade/AlphaFade_OColor");
            RegisterMiscShader(AlphaFade_OColor, "Pass0", "AlphaFade_OColor");
        }
        public static void UnLoad2()
        {
            AlphaFade = null;
            AlphaFade_ACut = null;
            AlphaFade_ACut_OColor = null;
            AlphaFade_OColor = null;
        }
    }
}
