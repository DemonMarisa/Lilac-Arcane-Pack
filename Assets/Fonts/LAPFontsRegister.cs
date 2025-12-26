using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.ModLoader;

namespace LAP.Assets.Fonts
{
    public class LAPFontsRegister : ModSystem
    {
        public static Asset<DynamicSpriteFont> MGRFonts { get; private set; }
        public override void Load()
        {
            MGRFonts = ModContent.Request<DynamicSpriteFont>("LAP/Assets/Fonts/MGRFonts");
        }
        public override void Unload()
        {
            MGRFonts = null;
        }
    }
}
