using System.ComponentModel;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace LAP.Content.Configs
{
    [BackgroundColor(105, 105, 105, 216)]
    public class LAPUIConfig : ModConfig
    {
        public static LAPUIConfig Instance;
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message) => true;
        [BackgroundColor(211, 211, 211, 192)]
        [Range(-10000, 10000)]
        public int CDUIOffsetX { get; set; }
        [BackgroundColor(211, 211, 211, 192)]
        [Range(-10000, 10000)]
        public int CDUIOffsetY { get; set; }
        [BackgroundColor(211, 211, 211, 192)]
        [Range(1, 3)]
        public int CDUIOrigMode { get; set; }
        [BackgroundColor(211, 211, 211, 192)]
        [DefaultValue(true)]
        public bool CDChangePosByBuffCount { get; set; }
        [BackgroundColor(211, 211, 211, 192)]
        [DefaultValue(true)]
        public bool DrawFocusBar { get; set; }
        [BackgroundColor(211, 211, 211, 192)]
        [Range(-10000, 10000)]
        public int FocusBarOffsetX { get; set; }
        [BackgroundColor(211, 211, 211, 192)]
        [Range(-10000, 10000)]
        public int FocusBarOffsetY { get; set; }
    }
}
