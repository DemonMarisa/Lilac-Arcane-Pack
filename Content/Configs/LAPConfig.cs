using System.ComponentModel;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace LAP.Content.Configs
{
    [BackgroundColor(105, 105, 105, 216)]
    public class LAPConfig : ModConfig
    {
        public static LAPConfig Instance;
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message) => true;

        [BackgroundColor(211, 211, 211, 192)]
        [Range(5000, 50000)]
        [Increment(1)]
        [DefaultValue(15000)]
        public int ParticleLimit { get; set; }

        [BackgroundColor(211, 211, 211, 192)]
        [Range(0, 10f)]
        [DefaultValue(1f)]
        public float ScreenShakeStrength { get; set; }

        [BackgroundColor(211, 211, 211, 192)]
        [DefaultValue(false)]
        public bool PerformanceMode { get; set; }

        [BackgroundColor(211, 211, 211, 192)]
        [Range(0, 10f)]
        [DefaultValue(1f)]
        public float PlayerDamageMult { get; set; }

        [BackgroundColor(211, 211, 211, 192)]
        [Range(0, 1f)]
        [DefaultValue(1f)]
        public float PlayerDrMult { get; set; }
    }
}
