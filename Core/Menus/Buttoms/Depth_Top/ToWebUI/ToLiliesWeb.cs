using LAP.Assets.Menus;
using LAP.Core.UISystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;

namespace LAP.Core.Menus.Buttoms.Depth_Top.ToWebUI
{
    public class ToLiliesWeb : BaseUI
    {
        public static BaseUI ToWeb => UIManager.UICollection[GetInstance<LiliesURL>().Type];
        public override void StartHover()
        {
            SoundEngine.PlaySound(MenuSounds.Hover);
        }
        public override void PostUpdate()
        {
            Position = new Vector2(40, Main.screenHeight - 35);
            Rectangle = Utils.CenteredRectangle(Position, new Vector2(50, 50));
            ToWeb.Update();
        }
        public override void MouseHover(bool isHover)
        {
            if (isHover)
            {
                if (Main.mouseLeft)
                    Scale2 = MathHelper.Lerp(Scale2, 0.95f, 0.2f);
                else
                    Scale2 = MathHelper.Lerp(Scale2, 1.1f, 0.2f);
            }
            else
            {
                Scale2 = MathHelper.Lerp(Scale2, 1f, 0.2f);
            }
        }
        public override void OnLeftClick()
        {
            SoundEngine.PlaySound(MenuSounds.Click);
        }
        public override void OnMouseLeftRelease()
        {
            if (!LiliesURL.Active)
                LiliesURL.Active = true;
            LiliesURL.Out = !LiliesURL.Out;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = BGTextureRegister.LiliesCross.Value;
            Vector2 Orig = texture.Size() / 2;
            Main.spriteBatch.Draw(texture, Position, null, Color.White, 0, Orig, 0.2f * Scale2, 0, 0);

            ToWeb.Draw(spriteBatch);
        }
    }
}
