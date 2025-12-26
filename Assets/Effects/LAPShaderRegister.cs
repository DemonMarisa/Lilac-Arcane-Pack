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
        public static Asset<Effect> MetaballShader { get; private set; }
        public static Asset<Effect> EdgeMeltsShader { get; private set; }
        public static Asset<Effect> StandardFlowShader { get; private set; }
        public static Asset<Effect> FlowWithAShader { get; private set; }
        public static Asset<Effect> PolarDistortShader { get; private set; }
        public static Asset<Effect> PolarDistortShaderWithR { get; private set; }
        public static Asset<Effect> DisplacemenShader { get; private set; }
        public static Asset<Effect> SlashTrailShader { get; private set; }
        public static Asset<Effect> CDUIMeltShader { get; private set; }
        public static Asset<Effect> GassBlur { get; private set; }
        public static Asset<Effect> Fill { get; private set; }
        public static Asset<Effect> FocusBar { get; private set; }
        public override void Load()
        {
            if (Main.dedServ)
                return;

            static Asset<Effect> LoadShader(string path)
            {
                return ModContent.Request<Effect>($"{ShaderPath}{path}");
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

            SlashTrailShader = LoadShader("SlashTrailShader");
            RegisterMiscShader(SlashTrailShader, "LAPSlashTrailShaderPass", "SlashTrailShader");

            CDUIMeltShader = LoadShader("CDUIMeltShader");
            RegisterMiscShader(CDUIMeltShader, "Pass0", "CDUIMeltShader");

            GassBlur = LoadShader("GassBlur");
            RegisterMiscShader(GassBlur, "Pass0", "GassBlur");

            Fill = LoadShader("Fill");
            RegisterMiscShader(Fill, "Pass0", "Fill");

            FocusBar = LoadShader("FocusBar");
            RegisterMiscShader(FocusBar, "Pass0", "FocusBar");
        }
        public override void Unload()
        {
            DisplacemenShader = null;
            MetaballShader = null;
            EdgeMeltsShader = null;
            StandardFlowShader = null;
            FlowWithAShader = null;
            PolarDistortShader = null;
            PolarDistortShaderWithR = null;
            SlashTrailShader = null;
            CDUIMeltShader = null;
            GassBlur = null;
            Fill = null;
            FocusBar = null;
        }
        public static void RegisterMiscShader(Asset<Effect> shader, string passName, string registrationName)
        {
            Asset<Effect> shaderPointer = shader;
            MiscShaderData passParamRegistration = new(shaderPointer, passName);
            GameShaders.Misc[$"{ShaderPrefix}{registrationName}"] = passParamRegistration;
        }
    }
}
