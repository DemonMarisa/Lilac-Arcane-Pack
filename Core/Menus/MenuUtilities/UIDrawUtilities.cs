using LAP.Assets.Fonts;
using LAP.Assets.Menus;
using LAP.Assets.TextureRegister;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.UI.Chat;

namespace LAP.Core.Menus.MenuUtilities
{
    public static class UIDrawUtilities
    {     
        public static void DrawEdge(SpriteBatch spriteBatch, Vector2 DrawPos, float GobalOpacity, float XScale)
        {
            if (GobalOpacity > 0.01f)
            {
                float OPoffset = MathF.Sin(Main.GlobalTimeWrappedHourly * 2) + 1;
                OPoffset /= 10;
                OPoffset = 1f - OPoffset;
                float DrawOpacty = OPoffset * GobalOpacity;
                Texture2D HoverBloom = LAPTextureRegister.EL_Bloom.Value;
                Texture2D Edge = LAPTextureRegister.EL_HoverEdge.Value;
                Vector2 Scale = new Vector2(3f * XScale, 0.5f);
                Vector2 BloomDrawPos = DrawPos - Vector2.UnitY * 3;
                Vector2 EdgeScale = new Vector2(1f, 1f);
                spriteBatch.Draw(HoverBloom, BloomDrawPos, null, Color.White * 0.3f * DrawOpacty, 0, HoverBloom.Size() / 2, Scale, 0, 0);

                Vector2 LeftOrig = new Vector2(0, Edge.Height / 2);
                Vector2 LeftDrawPos = DrawPos - Vector2.UnitX * 180 * XScale - Vector2.UnitY * 4;
                Vector2 LeftEdgeScale = new Vector2(1f, 1f) * 0.9f;
                spriteBatch.Draw(Edge, LeftDrawPos, null, Color.White * 0.9f * DrawOpacty, 0, LeftOrig, LeftEdgeScale, 0, 0);

                Vector2 RightOrig = new Vector2(Edge.Width, Edge.Height / 2);
                Vector2 RightDrawPos = DrawPos + Vector2.UnitX * 180 * XScale - Vector2.UnitY * 4;
                Vector2 RightEdgeScale = new Vector2(1f, 1f) * 0.9f;
                spriteBatch.Draw(Edge, RightDrawPos, null, Color.White * 0.9f * DrawOpacty, 0, RightOrig, RightEdgeScale, SpriteEffects.FlipHorizontally, 0);
            }
        }
        public static void DrawMouseRightTip(float Opacity)
        {
            string Version = "鼠标右键退出";
            DynamicSpriteFont font = LAPFontsRegister.Death_Text_Lilies.Value;
            Vector2 scale = Vector2.One;
            Vector2 Size = ChatManager.GetStringSize(font, Version, scale);
            Vector2 VerionPos = new Vector2(Main.screenWidth, Main.screenHeight) - Vector2.UnitX * 60 - Vector2.UnitY * 35;
            Vector2 DrawOrig = new Vector2(Size.X, Size.Y / 2);
            ChatManager.DrawColorCodedString(Main.spriteBatch, font, Version, VerionPos, Color.White * Opacity, 0, DrawOrig, scale * 0.6f);

            Texture2D InPut = BGTextureRegister.Input_Keyboard.Value;
            Vector2 DrawPos = new Vector2(Main.screenWidth, Main.screenHeight) - Vector2.UnitX * 30 - Vector2.UnitY * 40;
            Vector2 InPutScale = Vector2.One;
            Rectangle rec = InPut.Frame(9, 12, 5, 9);
            Main.spriteBatch.Draw(InPut, DrawPos, rec, Color.White * Opacity, 0, rec.Size() / 2, InPutScale, 0, 0);
        }
    }
}
