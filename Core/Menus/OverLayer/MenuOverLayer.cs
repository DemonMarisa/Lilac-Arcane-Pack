using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.Menus.OverLayer
{
    public class MenuOverLayer : ModSystem
    {
        public static float OverlayBlackOpacity = 0f;
        public override void Load()
        {
            On_Main.DrawThickCursor += DrawThickCursor;
            On_Main.DrawVersionNumber += On_DrawVersion;
            On_Main.DrawSocialMediaButtons += On_DrawSocialMediaButtons;
            On_Main.DrawtModLoaderSocialMediaButtons += On_DrawtModLoaderSocialMediaButtons;
            On_Main.HandleNews += On_HandleNews;
        }
        public override void Unload()
        {
            On_Main.DrawThickCursor -= DrawThickCursor;
            On_Main.DrawVersionNumber -= On_DrawVersion;
            On_Main.DrawSocialMediaButtons -= On_DrawSocialMediaButtons;
            On_Main.DrawtModLoaderSocialMediaButtons -= On_DrawtModLoaderSocialMediaButtons;
            On_Main.HandleNews -= On_HandleNews;
        }
        public static Vector2 DrawThickCursor(On_Main.orig_DrawThickCursor orig, bool smart)
        {
            if (Main.menuMode != MenuID.Title && OverlayBlackOpacity > 0.02f)
                Main.spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth * 2, Main.screenHeight * 2), Color.Black * OverlayBlackOpacity);
            orig(smart);
            return Vector2.Zero;
        }
        public static void On_DrawVersion(On_Main.orig_DrawVersionNumber orig, Color menuColor, float upBump)
        {
            if (MenuLoader.CurrentMenu is EnderMenus)
            {
            }
            else
            {
                orig(menuColor, upBump);
            }
        }
        public static void On_DrawSocialMediaButtons(On_Main.orig_DrawSocialMediaButtons orig, Color menuColor, float upBump)
        {
            if (MenuLoader.CurrentMenu is EnderMenus)
            {
            }
            else
            {
                orig(menuColor, upBump);
            }
        }
        public static void On_DrawtModLoaderSocialMediaButtons(On_Main.orig_DrawtModLoaderSocialMediaButtons orig, Color menuColor, float upBump)
        {
            if (MenuLoader.CurrentMenu is EnderMenus)
            {
            }
            else
            {
                orig(menuColor, upBump);
            }
        }
        public static void On_HandleNews(On_Main.orig_HandleNews orig, Color menuColor)
        {
            if (MenuLoader.CurrentMenu is EnderMenus)
            {
            }
            else
            {
                orig(menuColor);
            }
        }
    }
}
