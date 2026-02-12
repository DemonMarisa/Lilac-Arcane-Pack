using LAP.Assets.Menus;
using LAP.Assets.TextureRegister;
using LAP.Core.Menus.AllMenuID;
using LAP.Core.Menus.AllTitleBG;
using LAP.Core.Menus.DrawVideo;
using LAP.Core.Menus.OverLayer;
using LAP.Core.Utilities;
using LAP.Music;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LAP.Core.Menus
{
    public class EnderMenus : ModMenu
    {
        public static int TitleBgStyle = BGStyle.LiliesStart;
        public override Asset<Texture2D> Logo => LAPTextureRegister.InvisibleProj.Texture;
        public override Asset<Texture2D> SunTexture => LAPTextureRegister.InvisibleProj.Texture;
        public override Asset<Texture2D> MoonTexture => LAPTextureRegister.InvisibleProj.Texture;
        public override int Music => GetMusicID();
        public static int GetMusicID()
        {
            if (MenuVideoPlay.CanPlay)
                return MusicLoader.GetMusicSlot(MusicRegister.SliencePath);
            if (TitleBgStyle == BGStyle.LiliesEnd)
                return MusicLoader.GetMusicSlot(MusicRegister.MainThemeLiliesPath);
            return MusicLoader.GetMusicSlot(MusicRegister.LilyPath);
        }

        public override ModSurfaceBackgroundStyle MenuBackgroundStyle => null;
        public override string DisplayName => Language.GetTextValue("Mods.LAP.Menus.EnderLilies");
        public static bool CanOut;
        public static VideoPlayer videoPlayer;
        public override void Load()
        {
            videoPlayer = new VideoPlayer();
        }
        #region 选中与离开
        public override void OnSelected()
        {
            LiliesStart.Time = 220;
            MenuOverLayer.OverlayBlackOpacity = 1f;
            CanOut = false;
            Main.menuMode = LAPMenuID.LAPTitle;
            SoundEngine.PlaySound(MenuSounds.Click);
        }

        public override void OnDeselected()
        {
            LiliesStart.Time = 220;
            CanOut = false;
            if (Main.menuMode != MenuID.FancyUI)
                Main.menuMode = MenuID.Title;
            SoundEngine.PlaySound(MenuSounds.LiliesOut);
            if (TitleBgStyle == BGStyle.LiliesStart && SoundEngine.TryGetActiveSound(LiliesStart.RainSlotID, out var result))
            {
                result.Stop();
            }
        }
        #endregion
        #region 更新
        public override void Update(bool isOnTitleScreen)
        {
            // 必须松开一次才能离开
            if (Main.mouseLeftRelease && Main.mouseRightRelease)
            {
                CanOut = true;
            }
            LAPTitleUpdate.Update();
        }
        #endregion
        #region 绘制
        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            LAPTitleDraw.PreDraw();
            return false;
        }
        public override void PostDrawLogo(SpriteBatch spriteBatch, Vector2 logoDrawCenter, float logoRotation, float logoScale, Color drawColor)
        {
            LAPTitleDraw.PostDraw();
        }
        #endregion
    }
}
