using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.LAPKeys
{
    public class LAPKeystate : ModSystem
    {
        public static bool JustPressTab;
        public static bool PressTab;
        public static bool ReleaseTab;
        public override void Load()
        {
            On_Main.UpdateUIStates += PostUpdateUI;
        }
        public static void PostUpdateUI(On_Main.orig_UpdateUIStates orig, GameTime gameTime)
        {
            orig(gameTime);

            JustPressTab = false;
            if (Main.keyState.IsKeyDown(Keys.Tab) && !PressTab)
            {
                JustPressTab = true;
                PressTab = true;
                ReleaseTab = false;
            }
            else if (!Main.keyState.IsKeyDown(Keys.Tab))
            {
                PressTab = false;
                ReleaseTab = true;
            }
        }
    }
}
