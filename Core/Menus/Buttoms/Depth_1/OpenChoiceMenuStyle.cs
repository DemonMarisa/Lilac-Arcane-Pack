using LAP.Assets.Fonts;
using LAP.Assets.Menus;
using LAP.Core.UISystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace LAP.Core.Menus.Buttoms.Depth_1
{
    public class OpenChoiceMenuStyle : BaseUI
    {
        public override void PostUpdate()
        {
            Position = new Vector2(40, Main.screenHeight - 35);
            Rectangle = Utils.CenteredRectangle(Position, new Vector2(50, 50));
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D InPut = BGTextureRegister.Input_Keyboard.Value;
            Vector2 DrawPos = new Vector2(Main.screenWidth, Main.screenHeight) - Vector2.UnitX * 40 - Vector2.UnitY * 40;
            Vector2 InPutScale = Vector2.One;
            Rectangle rec = InPut.Frame(9, 12, 3, 11);
            Main.spriteBatch.Draw(InPut, DrawPos, rec, Color.White, 0, rec.Size() / 2, InPutScale, 0, 0);

            string Version = Language.GetTextValue("Mods.LAP.Menus.ChangeMenuBGStyle");
            DynamicSpriteFont font = LAPFontsRegister.Death_Text_Lilies.Value;
            Vector2 scale = Vector2.One;
            Vector2 Size = ChatManager.GetStringSize(font, Version, scale);
            Vector2 VerionPos = new Vector2(Main.screenWidth, Main.screenHeight) - Vector2.UnitX * 80 - Vector2.UnitY * 34;
            Vector2 DrawOrig = new Vector2(Size.X, Size.Y / 2);
            ChatManager.DrawColorCodedString(Main.spriteBatch, font, Version, VerionPos, Color.White, 0, DrawOrig, scale * 0.6f);
        }
    }
}
