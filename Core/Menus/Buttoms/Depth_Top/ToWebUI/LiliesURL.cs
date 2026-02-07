using LAP.Assets.Fonts;
using LAP.Assets.Menus;
using LAP.Assets.TextureRegister;
using LAP.Core.UISystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.UI.Chat;

namespace LAP.Core.Menus.Buttoms.Depth_Top.ToWebUI
{
    public class LiliesURL : BaseUI
    {
        public string EnderLiliesWeb = "https://en.enderlilies.com/";
        public float scale;
        public float TextOpacity = 1f;
        public float LineOpacty = 0f;
        public static bool Active = false;
        public static bool Out = true;
        public override void PostUpdate()
        {
            Position = new Vector2(40, Main.screenHeight - 36);
            DynamicSpriteFont font = LAPFontsRegister.Mouse_Text_Lilies.Value;
            Vector2 Size = ChatManager.GetStringSize(font, EnderLiliesWeb, Vector2.One);
            Rectangle = Utils.CenteredRectangle(Position + new Vector2(Size.X / 2 + 20, 0), Size);
            if (Active && !Out)
            {
                TextOpacity = MathHelper.Lerp(TextOpacity, 1f, 0.2f);
            }
            if (Out)
            {
                TextOpacity = MathHelper.Lerp(TextOpacity, 0f, 0.2f);
                if (TextOpacity < 0.02f)
                {
                    Active = false;
                }
            }
        }
        public override void StartHover()
        {
            SoundEngine.PlaySound(MenuSounds.Hover);
        }
        public override bool PreUpdateHover()
        {
            return Active;
        }
        public override void MouseHover(bool isHover)
        {
            if (isHover)
            {
                if (Main.mouseLeft)
                    scale = MathHelper.Lerp(scale, 0.95f, 0.2f);
                else
                    scale = MathHelper.Lerp(scale, 1.1f, 0.2f);
                LineOpacty = MathHelper.Lerp(LineOpacty, 1f, 0.2f);
            }
            else
            {
                scale = MathHelper.Lerp(scale, 1f, 0.2f);
                LineOpacty = MathHelper.Lerp(LineOpacty, 0f, 0.2f);
            }
        }
        public override void OnLeftClick()
        {
            SoundEngine.PlaySound(MenuSounds.Click);
        }
        public override void OnMouseLeftRelease()
        {
            Utils.OpenToURL(EnderLiliesWeb);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Active)
                return;
            DynamicSpriteFont font = LAPFontsRegister.Mouse_Text_Lilies.Value;
            Vector2 Textscale = Vector2.One * scale;
            Vector2 Size = ChatManager.GetStringSize(font, EnderLiliesWeb, Textscale);
            Vector2 VerionPos = Position + Vector2.UnitX * 30 + Vector2.UnitY * 3;
            Vector2 DrawOrig = new Vector2(0, Size.Y / 2);
            ChatManager.DrawColorCodedString(Main.spriteBatch, font, EnderLiliesWeb, VerionPos, Color.White * TextOpacity, 0, DrawOrig, Textscale);

            Texture2D whitCube = LAPTextureRegister.WhiteCube.Value;
            Vector2 whitCubeOrig = new Vector2(0, whitCube.Size().Y / 2);
            Vector2 CubePos = VerionPos + Vector2.UnitY * 15 - Vector2.UnitX * 3;
            Main.spriteBatch.Draw(whitCube, CubePos, null, Color.White * LineOpacty, 0, whitCubeOrig, new Vector2(16 * scale, 0.07f), 0, 0);
        }
    }
}
