using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace LAP.Assets.Effects
{
    public class LAPShaderRegister : ModSystem
    {        
        private const string ShaderPath = "LAP/Assets/Effects/Overlays/";
        internal const string ShaderPrefix = "LAP:";
        public static Effect MetaballShader;
        public static Effect EdgeMeltsShader;
        public static Effect StandardFlowShader; 
        public static Effect FlowWithAShader;
        public static Effect PolarDistortShader;
        public static Effect PolarDistortShaderWithR;
        public static Effect DisplacemenShader;
        public override void Load()
        {
            if (Main.dedServ)
                return;

            static Effect LoadShader(string path)
            {
                return ModContent.Request<Effect>($"{ShaderPath}{path}", AssetRequestMode.ImmediateLoad).Value;
            }

            DisplacemenShader = LoadShader("DisplacemenShader");
            RegisterMiscShader(DisplacemenShader, "LPADisplacementPass", "DisplacemenShader");

            MetaballShader = LoadShader("MetaBallShader");
            RegisterMiscShader(MetaballShader, "LAPMetalBallPass", "MetaBallShader");

            EdgeMeltsShader = LoadShader("EdgeMeltsShader");
            RegisterMiscShader(EdgeMeltsShader, "LAPEdgeMeltsPass", "EdgeMeltsShader");

            StandardFlowShader = LoadShader("StandardFlowShader");
            RegisterMiscShader(StandardFlowShader, "LAPStandardFlowPass", "StandardFlowShader");

            FlowWithAShader = LoadShader("FlowWithAShader");
            RegisterMiscShader(StandardFlowShader, "LAPFlowWithAPass", "FlowWithAShader");

            PolarDistortShader = LoadShader("PolarDistortShader");
            RegisterMiscShader(PolarDistortShader, "LAPPolarDistortPass", "PolarDistortShader");

            PolarDistortShaderWithR = LoadShader("PolarDistortShaderWithR");
            RegisterMiscShader(PolarDistortShaderWithR, "LAPPolarDistortPass", "PolarDistortShaderWithR");
        }

        public static void RegisterMiscShader(Effect shader, string passName, string registrationName)
        {
            Ref<Effect> shaderPointer = new(shader);
            MiscShaderData passParamRegistration = new(shaderPointer, passName);
            GameShaders.Misc[$"{ShaderPrefix}{registrationName}"] = passParamRegistration;
        }
    }
}
