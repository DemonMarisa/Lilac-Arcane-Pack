using LAP.Assets.Menus;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LAP.Core.Menus.AllTitleBG
{
    public class LiliesTrueEnd
    {
        public static void Draw()
        {
            Texture2D BG = BGTextureRegister.LiliesMenu2.Value;
            Main.spriteBatch.Draw(BG, LAPUtilities.ScreenCenter(), null, Color.White, 0, BG.Size() / 2, 0.9f, 0, 0);

            Vector2 DrawPos = new Vector2(Main.screenWidth / 2 - 10, 345);
            Texture2D texture = BGTextureRegister.LiliesLogo.Value;
            Main.spriteBatch.Draw(texture, DrawPos, null, Color.White, 0, texture.Size() / 2, 0.9f, 0, 0);
        }
    }
}
