using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.ModLoader;

namespace LAP.Assets.Fonts
{
    public class LAPFontsRegister : ModSystem
    {
        public static Asset<DynamicSpriteFont> MGRFonts { get; private set; }
        public static Asset<DynamicSpriteFont> Combat_Crit_Lilies { get; private set; }
        public static Asset<DynamicSpriteFont> Combat_Text_Lilies { get; private set; }
        public static Asset<DynamicSpriteFont> Death_Text_Lilies { get; private set; }
        public static Asset<DynamicSpriteFont> Item_Stack_Lilies { get; private set; }
        public static Asset<DynamicSpriteFont> Mouse_Text_Lilies { get; private set; }
        public override void Load()
        {
            MGRFonts = Request<DynamicSpriteFont>("LAP/Assets/Fonts/MGRFonts");
            Combat_Crit_Lilies = Request<DynamicSpriteFont>("LAP/Assets/Fonts/LiliesFont/Combat_Crit_Lilies");
            Combat_Text_Lilies = Request<DynamicSpriteFont>("LAP/Assets/Fonts/LiliesFont/Combat_Text_Lilies");
            Death_Text_Lilies = Request<DynamicSpriteFont>("LAP/Assets/Fonts/LiliesFont/Death_Text_Lilies");
            Item_Stack_Lilies = Request<DynamicSpriteFont>("LAP/Assets/Fonts/LiliesFont/Item_Stack_Lilies");
            Mouse_Text_Lilies = Request<DynamicSpriteFont>("LAP/Assets/Fonts/LiliesFont/Mouse_Text_Lilies");
        }
        public override void Unload()
        {
            MGRFonts = null;
            Combat_Crit_Lilies = null;
            Combat_Text_Lilies = null;
            Death_Text_Lilies = null;
            Item_Stack_Lilies = null;
            Mouse_Text_Lilies = null;
        }
    }
}
