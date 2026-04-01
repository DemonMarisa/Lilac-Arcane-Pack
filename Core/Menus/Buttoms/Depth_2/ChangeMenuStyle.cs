using LAP.Assets.Menus;
using LAP.Assets.TextureRegister;
using LAP.Core.LAPKeys;
using LAP.Core.Menus.Buttoms.Depth_2.MenuBGStyles;
using LAP.Core.Menus.MenuUtilities;
using LAP.Core.UISystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;

namespace LAP.Core.Menus.Buttoms.Depth_2
{
    public class ChangeMenuStyle : BaseUI
    {
        #region 引用
        public static BaseUI LiliesStart => UIManager.UICollection[GetInstance<LiliesStartUI>().Type];
        public static BaseUI LiliesTrueEnd => UIManager.UICollection[GetInstance<LiliesTrueEndUI>().Type];
        #endregion
        public bool Out = true;
        public static bool Active = false;
        public override int UIDepth => 1;
        public override bool PreSetDepth() => Active;
        public override bool Colliding(Rectangle rectangle, Rectangle mouseRectangle)
        {
            return true;
        }
        public override void PostUpdate()
        {
            Rectangle = new Rectangle(0, 0, Main.screenWidth, Main.screenHeight);
            if (LAPKeystate.JustPressTab && !UIManager.ActiveDepth[1])
            {
                SoundEngine.PlaySound(MenuSounds.Click);
                if (!Active)
                    Active = true;
                else
                    Out = true;
            }
            if (Active && !Out)
            {
                Opacity = MathHelper.Lerp(Opacity, 0.9f, 0.2f);
            }
            if (Out)
            {
                Opacity = MathHelper.Lerp(Opacity, 0f, 0.2f);
            }
            if (Active)
            {
                LiliesStart.Update();
                LiliesTrueEnd.Update();
            }
            else
            {
                Opacity = MathHelper.Lerp(Opacity, 0f, 0.1f);
                Out = false;
            }
            if (Opacity < 0.02f)
            {
                Active = false;
            }
            LiliesStart.Opacity = Opacity;
            LiliesTrueEnd.Opacity = Opacity;
        }
        public override void OnRightClick()
        {
            Out = true;
            if (Active)
                SoundEngine.PlaySound(MenuSounds.LiliesOut);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(LAPTextureRegister.WhiteCube.Value, new Rectangle(0, 0, Main.screenWidth * 2, Main.screenHeight * 2), Color.Black * Opacity);

                Texture2D texture = BGTextureRegister.Line.Value;
                float Offset = 120;
                Vector2 Pos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2 + Offset);
                Vector2 Scale = new Vector2(4f, 1f);
                spriteBatch.Draw(texture, Pos, null, Color.White * Opacity, 0, texture.Size() / 2, Scale, SpriteEffects.FlipHorizontally, 0);

                Vector2 Pos2 = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2 - Offset);
                spriteBatch.Draw(texture, Pos2, null, Color.White * Opacity, 0, texture.Size() / 2, Scale, SpriteEffects.FlipHorizontally, 0);

                LiliesStart.Draw(spriteBatch);
                LiliesTrueEnd.Draw(spriteBatch);

                UIDrawUtilities.DrawMouseRightTip(Opacity);
            }
        }
    }
}
