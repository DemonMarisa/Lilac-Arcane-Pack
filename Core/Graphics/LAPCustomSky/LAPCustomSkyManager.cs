using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.LAPCustomSky
{
    public class LAPCustomSkyManager : ModSystem
    {
        public static List<LAPCustomSky> CustomSkies = new List<LAPCustomSky>();
        public static List<LAPCustomSky> ActiveCustomSkies = new List<LAPCustomSky>();
        public override void Load()
        {
            // On_Main.DrawBG += DrawLAPBG;
            // On_Main.DrawUnderworldBackground += DrawUnderworldBackgroundLAPBG;
        }
        public override void Unload()
        {
            // On_Main.DrawBG -= DrawLAPBG;
            // On_Main.DrawUnderworldBackground -= DrawUnderworldBackgroundLAPBG;
        }
        public override void PostUpdateWorld()
        {
            if (ActiveCustomSkies.Count != 0)
            {
                for (int i = 0; i < ActiveCustomSkies.Count; i++)
                {
                    var sky = ActiveCustomSkies[i];
                    sky.Update();
                }
                for (int i = ActiveCustomSkies.Count - 1; i >= 0; i--)
                {
                    var sky = ActiveCustomSkies[i];
                    if (sky.DeActiveCondition())
                    {
                        sky.OnDeActive();
                        ActiveCustomSkies.RemoveAt(i);
                    }
                }
            }
        }
        public static void DrawLAPBG(On_Main.orig_DrawBG orig, Main self)
        {
            orig(self);
            if (ActiveCustomSkies.Count != 0)
            {
                for (int i = 0; i < ActiveCustomSkies.Count; i++)
                {
                    var sky = ActiveCustomSkies[i];
                    sky.Draw();
                }
            }
        }
        public static void DrawUnderworldBackgroundLAPBG(On_Main.orig_DrawUnderworldBackground orig, Main self, bool flat)
        {
            orig(self, flat);
            if (ActiveCustomSkies.Count != 0)
            {
                for (int i = 0; i < ActiveCustomSkies.Count; i++)
                {
                    var sky = ActiveCustomSkies[i];
                    sky.DrawAfterUnderGroundBG();
                }
            }
        }
    }
}
