using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace LAP.Assets.Effects
{
    public partial class LAPShaderRegister : ModSystem
    {        
        private const string ShaderPath = "LAP/Assets/Effects/Overlays/";
        private const string ScreenShaderPath = "LAP/Assets/Effects/ScreenShaders/";
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
        public static Asset<Effect> ThresholdShader { get; private set; }
        public override void Load()
        {
            if (Main.dedServ)
                return;
            DisplacemenShader = LoadShader("DisplacemenShader");

            MetaballShader = LoadShader("MetaBallShader");

            EdgeMeltsShader = LoadShader("EdgeMeltsShader");

            StandardFlowShader = LoadShader("StandardFlowShader");

            FlowWithAShader = LoadShader("FlowWithAShader");

            PolarDistortShader = LoadShader("PolarDistortShader");

            PolarDistortShaderWithR = LoadShader("PolarDistortShaderWithR");

            SlashTrailShader = LoadShader("SlashTrailShader");

            CDUIMeltShader = LoadShader("CDUIMeltShader");

            GassBlur = LoadShader("GassBlur");

            Fill = LoadShader("Fill");

            FocusBar = LoadShader("FocusBar");

            ThresholdShader = LoadShader("ThresholdShader");

            LoadScreen();

            Load2();
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
            ThresholdShader = null;

            UnLoadScreen();

            UnLoad2();
        }

        public static Asset<Effect> LoadShader(string path)
        {
            return Request<Effect>($"{ShaderPath}{path}");
        }

        //public static void RegisterMiscShader(Asset<Effect> shader, string passName, string registrationName)
        //{
        //    Asset<Effect> shaderPointer = shader;
        //    MiscShaderData passParamRegistration = new(shaderPointer, passName);
        //    GameShaders.Misc[$"{ShaderPrefix}{registrationName}"] = passParamRegistration;
        //}
    }
}
