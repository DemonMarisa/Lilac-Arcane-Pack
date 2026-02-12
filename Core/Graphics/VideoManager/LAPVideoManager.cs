using LAP.Assets.Movies;
using LAP.Content.Configs;
using LAP.Core.SystemsLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace LAP.Core.Graphics.VideoManager
{
    //public class LAPVideoManager : ModSystem
    //{
    //    public static LAPVideoManager Instance;
    //    public static VideoPlayer videoPlayer;
    //    public static Texture2D currentFrameTexture;
    //    public static List<Asset<Video>> VideoCollection = [];
    //    public static int ActiveVideoIndex;
    //    public static bool isPlay;
    //    public override void Load()
    //    {
    //        Instance = this;
    //        videoPlayer = new VideoPlayer();
    //        isPlay = false;
    //    }
    //    public override void Unload()
    //    {
    //        videoPlayer?.Dispose();
    //        videoPlayer = null;
    //        currentFrameTexture?.Dispose();
    //        currentFrameTexture = null;
    //        for (int i = 0; i < VideoCollection.Count; i++)
    //        {
    //            VideoCollection[i]?.Dispose();
    //            VideoCollection[i] = null;
    //        }
    //        VideoCollection?.Clear();
    //        VideoCollection = null;
    //    }
    //    public override void UpdateUI(GameTime gameTime)
    //    {
    //        if (videoPlayer.State == MediaState.Stopped)
    //            isPlay = false;
    //        if (isPlay && videoPlayer.State == MediaState.Stopped)
    //        {
    //            Video video = LAPContent.GetVideo(LAPMoviesRegister.Prologue);
    //            videoPlayer.Play(video);
    //        }
    //    }
    //    public static void DrawVideo()
    //    {
    //        // 获取当前帧的纹理
    //        Texture2D videoTexture = videoPlayer.GetTexture();
    //        if (videoTexture != null)
    //        {
    //            // 计算屏幕居中位置
    //            Vector2 position = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
    //            Vector2 origin = videoTexture.Size() / 2;
    //            // 绘制视频帧
    //           Main.spriteBatch.Draw(videoTexture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
    //        }
    //    }
    //    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    //    {
    //        if (!isPlay)
    //            return;
    //        int mouseIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
    //        if (mouseIndex != -1)
    //        {
    //            layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("LAP Video UI", delegate ()
    //            {
    //                DrawVideo();
    //                return true;
    //            }, InterfaceScaleType.UI));
    //        }
    //    }
    //}
}
