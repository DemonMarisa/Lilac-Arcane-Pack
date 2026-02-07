using LAP.Assets.Menus;
using LAP.Assets.TextureRegister;
using LAP.Core.Menus.Buttoms.Depth_2.Buttoms;
using LAP.Core.Menus.MenuUtilities;
using LAP.Core.UISystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;

namespace LAP.Core.Menus.Buttoms.Depth_2
{
    public class StartUI : BaseUI
    {
        public static bool Active = false;
        public bool Out = true;
        public static BaseUI SinglePlayer => UIManager.UICollection[GetInstance<SinglePlayer>().Type];
        public static BaseUI MultiPlayer => UIManager.UICollection[GetInstance<MultiPlayer>().Type];
        public static BaseUI WorkShop => UIManager.UICollection[GetInstance<WorkShop>().Type];
        public static BaseUI Achievements => UIManager.UICollection[GetInstance<Achievements>().Type];
        public static BaseUI Credits => UIManager.UICollection[GetInstance<Credits>().Type];
        public override int UIDepth => 2;
        public override bool PreSetDepth() => Active;
        public override void PostUpdate()
        {
            Rectangle = new Rectangle(0, 0, Main.screenWidth, Main.screenHeight);
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
                SinglePlayer.Update();
                MultiPlayer.Update();
                WorkShop.Update();
                Achievements.Update();
                Credits.Update();
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

            SinglePlayer.Opacity = Opacity;
            MultiPlayer.Opacity = Opacity;
            WorkShop.Opacity = Opacity;
            Achievements.Opacity = Opacity;
            Credits.Opacity = Opacity;
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
                float Offset = 240;
                Vector2 Height = Vector2.UnitY * 5;
                Vector2 Pos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2 + Offset) - Height;
                Vector2 Scale = new Vector2(4f, 1f);
                spriteBatch.Draw(texture, Pos, null, Color.White * Opacity, 0, texture.Size() / 2, Scale, SpriteEffects.FlipHorizontally, 0);

                Vector2 Pos2 = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2 - Offset) - Height;
                spriteBatch.Draw(texture, Pos2, null, Color.White * Opacity, 0, texture.Size() / 2, Scale, SpriteEffects.FlipHorizontally, 0);

                SinglePlayer.Draw(spriteBatch);
                MultiPlayer.Draw(spriteBatch);
                WorkShop.Draw(spriteBatch);
                Achievements.Draw(spriteBatch);
                Credits.Draw(spriteBatch);

                UIDrawUtilities.DrawMouseRightTip(Opacity);
            }
        }
    }
}
