using LAP.Assets.Movies;
using LAP.Core.Menus.OverLayer;
using LAP.Core.UISystem;
using LAP.Music;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace LAP.Core.Menus.DrawVideo
{
    public class MenuVideoPlay
    {
        public static int VideoPlayerCount;
        public static bool CanPlay;
        public static bool HasPlay;
        public static void UpdateVideo()
        {
            EnderMenus.videoPlayer.Volume *= Main.musicVolume;
            if (!CanPlay && Main.instance.IsActive)
                VideoPlayerCount++;
            if (HasPlay && EnderMenus.videoPlayer.State == MediaState.Stopped)
            {
                CanPlay = false;
                MenuOverLayer.OverlayBlackOpacity = 1f;
                HasPlay = false;
            }
            if (VideoPlayerCount > 1800)
            {
                CanPlay = true;
                VideoPlayerCount = 0;
            }
            if (CanPlay && EnderMenus.videoPlayer.State == MediaState.Stopped)
            {
                HasPlay = true;
                EnderMenus.videoPlayer.Play(LAPMoviesRegister.Prologue.Value);
            }
            if (CanPlay)
            {
                UIManager.BlockAllUI = 2;
            }
            if (Main.mouseLeft || Main.mouseRight)
            {
                EnderMenus.videoPlayer.Stop();
                CanPlay = false;
                VideoPlayerCount = 0;
            }
        }
        public static void DrawVideo()
        {
            if (!CanPlay)
                return;
            Main.spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth * 2, Main.screenHeight * 2), Color.Black);
            Texture2D videoTexture = EnderMenus.videoPlayer.GetTexture();
            if (videoTexture != null)
            {
                Vector2 position = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                Vector2 origin = videoTexture.Size() / 2;
                Main.spriteBatch.Draw(videoTexture, position, null, Color.White, 0f, origin, 0.85f, SpriteEffects.None, 0f);
            }
        }
    }
}
