using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace LAP.Assets.Effects
{
    public class LAPShaderRegister : ModSystem
    {        
        // 当未提供特定着色器时，用作基本绘图的默认值。此着色器仅渲染顶点颜色数据，无需修改。
        private const string ShaderPath = "LAP/Assets/Effects/Overlays/";
        internal const string ShaderPrefix = "LAP:";
        public static Effect MetaballShader;
        public static Effect EdgeMeltsShader;
        public static Effect StandardFlowShader; 
        public static Effect FlowWithAShader;
        public static Effect PolarDistortShader;
        public static Effect PolarDistortShaderWithR;
        public override void Load()
        {
            if (Main.dedServ)
                return;

            static Effect LoadShader(string path)
            {
                return ModContent.Request<Effect>($"{ShaderPath}{path}", AssetRequestMode.ImmediateLoad).Value;
            }

            MetaballShader = LoadShader("MetaBallShader");
            RegisterMiscShader(MetaballShader, "UCAMetalBallPass", "MetaBallShader");

            EdgeMeltsShader = LoadShader("EdgeMeltsShader");
            RegisterMiscShader(EdgeMeltsShader, "UCAEdgeMeltsPass", "EdgeMeltsShader");

            StandardFlowShader = LoadShader("StandardFlowShader");
            RegisterMiscShader(StandardFlowShader, "UCAStandardFlowPass", "StandardFlowShader");

            FlowWithAShader = LoadShader("FlowWithAShader");
            RegisterMiscShader(StandardFlowShader, "UCAFlowWithAPass", "FlowWithAShader");

            PolarDistortShader = LoadShader("PolarDistortShader");
            RegisterMiscShader(PolarDistortShader, "UCAPolarDistortPass", "PolarDistortShader");

            PolarDistortShaderWithR = LoadShader("PolarDistortShaderWithR");
            RegisterMiscShader(PolarDistortShaderWithR, "UCAPolarDistortPass", "PolarDistortShaderWithR");
        }

        public static void RegisterMiscShader(Effect shader, string passName, string registrationName)
        {
            Ref<Effect> shaderPointer = new(shader);
            MiscShaderData passParamRegistration = new(shaderPointer, passName);
            GameShaders.Misc[$"{ShaderPrefix}{registrationName}"] = passParamRegistration;
        }
    }
}
