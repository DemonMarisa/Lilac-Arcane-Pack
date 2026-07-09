using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.DebugSystem
{
    public class CheckVanilla : ModSystem
    {
        public override void Load()
        {
            On_Main.Update += On_Main_Update_CheckMs;
            On_Main.Draw += On_Main_Draw_CheckMs;
            On_Main.EnsureRenderTargetContent += On_Main_Draw_EnsureRenderTargetContent;
            On_Main.DoDraw += On_Main_DoDraw;
            On_Main.DrawBG += On_Main_DrawBG;
            On_Main.DrawDust += On_Main_DrawDust;
            On_Main.DrawCachedProjs += On_Main_DrawCachedProjs;
        }
        public override void Unload()
        {
            On_Main.Update -= On_Main_Update_CheckMs;
            On_Main.Draw -= On_Main_Draw_CheckMs;
            On_Main.EnsureRenderTargetContent -= On_Main_Draw_EnsureRenderTargetContent;
        }
        public static void On_Main_Update_CheckMs(On_Main.orig_Update orig, Main self, GameTime gameTime)
        {
            PerformanceMonitorSystem.StartTimer("原版全部更新延迟");
            orig(self, gameTime);
            PerformanceMonitorSystem.StopTimer("原版全部更新延迟");
        }
        public static void On_Main_Draw_CheckMs(On_Main.orig_Draw orig, Main self, GameTime gameTime)
        {
            PerformanceMonitorSystem.StartTimer("原版全部绘制延迟");
            orig(self, gameTime);
            PerformanceMonitorSystem.StopTimer("原版全部绘制延迟");
        }
        public static void On_Main_Draw_EnsureRenderTargetContent(On_Main.orig_EnsureRenderTargetContent orig, Main self)
        {
            PerformanceMonitorSystem.StartTimer("原版重设RT2D延迟");
            orig(self);
            PerformanceMonitorSystem.StopTimer("原版重设RT2D延迟");
        }
        public static void On_Main_DoDraw(On_Main.orig_DoDraw orig, Main self, GameTime gameTime)
        {
            PerformanceMonitorSystem.StartTimer("原版DoDraw延迟");
            PerformanceMonitorSystem.StartTimer("原版DoDraw到绘制BG的延迟");
            orig(self, gameTime);
            PerformanceMonitorSystem.StopTimer("原版DoDraw延迟");
            PerformanceMonitorSystem.StopTimer("原版DrawCachedProjs绘制到DoDraw结束延迟");
        }
        public static void On_Main_DrawBG(On_Main.orig_DrawBG orig, Main self)
        {
            orig(self);
            PerformanceMonitorSystem.StopTimer("原版DoDraw到绘制BG的延迟");
            PerformanceMonitorSystem.StartTimer("原版BG到绘制粒子延迟");
        }
        public static void On_Main_DrawDust(On_Main.orig_DrawDust orig, Main self)
        {
            orig(self);
            PerformanceMonitorSystem.StopTimer("原版BG到绘制粒子延迟");
            PerformanceMonitorSystem.StartTimer("原版粒子绘制到DrawCachedProjs结束延迟");
        }
        public static void On_Main_DrawCachedProjs(On_Main.orig_DrawCachedProjs orig, Main self, List<int> projCache, bool startSpriteBatch = true)
        {
            PerformanceMonitorSystem.StopTimer("原版粒子绘制到DrawCachedProjs结束延迟");
            PerformanceMonitorSystem.StartTimer("原版DrawCachedProjs绘制到DoDraw结束延迟");
            orig(self, projCache, startSpriteBatch);

        }
    }
}
