using LAP.Assets.Fonts;
using LAP.Core.UISystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace LAP.Core.Menus.Buttoms.Depth_Top
{
    public class LAPGameVersion : BaseUI
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            string Version = ModLoader.versionedName + Environment.NewLine + "              Terraria " + Main.versionNumber;
            DynamicSpriteFont font = LAPFontsRegister.Mouse_Text_Lilies.Value;
            Vector2 scale = Vector2.One;
            Vector2 Size = ChatManager.GetStringSize(font, Version, scale);
            Vector2 VerionPos = new Vector2(Main.screenWidth - 15, 45);
            Vector2 DrawOrig = new Vector2(Size.X, Size.Y / 2);
            ChatManager.DrawColorCodedString(Main.spriteBatch, font, Version, VerionPos, Color.White, 0, DrawOrig, scale);
        }
    }
}
