using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.MiscDate
{
    public class LAPInfo : ModSystem
    {
        public static bool AnyBossHere = false;
        public static Vector2 ScreenSize = new Vector2(Main.screenWidth, Main.screenHeight);
        public override void UpdateUI(GameTime gameTime)
        {
            ScreenSize = new Vector2(Main.screenWidth, Main.screenHeight);
        }
        public override void PreUpdateWorld()
        {
            AnyBossHere = false;
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && npc.boss)
                {
                    AnyBossHere = true;
                    return;
                }
            }
        }
    }
}
