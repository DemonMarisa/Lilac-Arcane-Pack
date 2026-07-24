using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        }
        public override void Unload()
        {
            On_Main.Update -= On_Main_Update_CheckMs;
            On_Main.Draw -= On_Main_Draw_CheckMs;
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
    }
}
