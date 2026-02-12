using LAP.Assets.Menus;
using LAP.Core.Menus.AllMenuID;
using LAP.Core.Menus.AllTitleBG;
using LAP.Core.Menus.Buttoms.Depth_1;
using LAP.Core.Menus.Buttoms.Depth_2;
using LAP.Core.Menus.Buttoms.Depth_Top;
using LAP.Core.Menus.Buttoms.Depth_Top.ToWebUI;
using LAP.Core.Menus.DrawVideo;
using LAP.Core.UISystem;
using LAP.Core.Utilities;
using MenuMod.Core.Menu.Buttoms.Depth_1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;

namespace LAP.Core.Menus
{
    public class LAPTitleDraw
    {       
        #region 引用
        public static BaseUI Start => UIManager.UICollection[GetInstance<Start>().Type];
        public static BaseUI Setting => UIManager.UICollection[GetInstance<Setting>().Type];
        public static BaseUI Quit => UIManager.UICollection[GetInstance<Quit>().Type];
        public static BaseUI ToWeb => UIManager.UICollection[GetInstance<ToLiliesWeb>().Type];
        public static BaseUI StartUI => UIManager.UICollection[GetInstance<StartUI>().Type];
        public static BaseUI ChangeMenuStyle => UIManager.UICollection[GetInstance<ChangeMenuStyle>().Type];
        public static BaseUI OpenChoiceMenuStyle => UIManager.UICollection[GetInstance<OpenChoiceMenuStyle>().Type];
        public static BaseUI SwitchModMenu => UIManager.UICollection[GetInstance<SwitchModMenu>().Type];
        public static BaseUI LAPGameVersion => UIManager.UICollection[GetInstance<LAPGameVersion>().Type];
        #endregion
        public static void PreDraw()
        {
            LAPUtilities.ReSetToBeginUI(BlendState.NonPremultiplied, SamplerState.LinearWrap);
            // 绘制主界面的背景
            DrawBG();

            LAPUtilities.ReSetToBeginUI(BlendState.Additive, SamplerState.LinearWrap);
            // 绘制按钮
            DrawButtom();

            LAPUtilities.ReSetToBeginUI(BlendState.NonPremultiplied, SamplerState.LinearWrap);

            DrawNonPreMult();

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);

            MenuVideoPlay.DrawVideo();
        }
        public static void PostDraw()
        {
            // 如果不在主菜单，则绘制一个黑色背景，并在上面绘制Logo
            if (Main.menuMode != LAPMenuID.LAPTitle)
            {
                LAPUtilities.ReSetToBeginUI(BlendState.NonPremultiplied, SamplerState.LinearWrap);

                Main.spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth * 2, Main.screenHeight * 2), Color.Black);
                // 绘制Logo
                Vector2 DrawPos = new Vector2(Main.screenWidth / 2, 100);
                Texture2D texture = BGTextureRegister.LiliesLogo.Value;
                Main.spriteBatch.Draw(texture, DrawPos, null, Color.White, 0, texture.Size() / 2, 0.5f, 0, 0);

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
            }
        }
        #region 绘制背景
        public static void DrawBG()
        {
            Main.spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth * 2, Main.screenHeight * 2), Color.Black);
            if (EnderMenus.TitleBgStyle == BGStyle.LiliesEnd)
            {
                LiliesTrueEnd.Draw();
            }
            if (EnderMenus.TitleBgStyle == BGStyle.LiliesStart)
            {
                LiliesStart.Draw();
            }
        }
        #endregion
        #region 绘制按钮
        public static void DrawButtom()
        {
            if (Main.menuMode == LAPMenuID.LAPTitle)
            {
                Start.Draw(Main.spriteBatch);
                Setting.Draw(Main.spriteBatch);
                Quit.Draw(Main.spriteBatch);
                OpenChoiceMenuStyle.Draw(Main.spriteBatch);
                SwitchModMenu.Draw(Main.spriteBatch);
            }
        }
        public static void DrawNonPreMult()
        {
            if (Main.menuMode == LAPMenuID.LAPTitle)
            {
                StartUI.Draw(Main.spriteBatch);
                ChangeMenuStyle.Draw(Main.spriteBatch);
            }
            ToWeb.Draw(Main.spriteBatch);
            LAPGameVersion.Draw(Main.spriteBatch);
        }
        #endregion
    }
}
