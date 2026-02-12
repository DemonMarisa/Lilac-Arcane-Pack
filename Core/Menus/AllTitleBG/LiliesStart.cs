using LAP.Assets.Menus;
using LAP.Assets.Sounds;
using LAP.Core.Menus.DrawVideo;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;

namespace LAP.Core.Menus.AllTitleBG
{
    public class RainDust(Vector2 pos, float rot, Vector2 scale, Vector2 speed, SpriteEffects SE)
    {
        public int Time;
        public Vector2 position = pos;
        public float Rot = rot;
        public Vector2 Scale = scale;
        public Vector2 Speed = speed;
        public SpriteEffects spriteEffects = SE;
        public void Update()
        {
            Time++;
            position += Speed;
        }
        public void Draw()
        {
            if (Main.rand.NextBool())
            {
                Texture2D rain = BGTextureRegister.RainDrop.Value;
                Main.spriteBatch.Draw(rain, position, null, Color.White, Rot, rain.Size() / 2, Scale, SpriteEffects.None, 0);
                if (Main.rand.NextBool())
                    Main.spriteBatch.Draw(rain, position, null, Color.White * 0.6f, Rot, rain.Size() / 2, Scale, SpriteEffects.None, 0);
            }
        }
    }
    public class LiliesStart
    {
        public static SlotId RainSlotID;
        public static List<RainDust> rainDusts = new List<RainDust>();
        public static int Time = 220;
        public static void Update()
        {
            Time++;
            Vector2 Center = new Vector2(Main.screenWidth / 2, -4000);
            for (int i = 0; i < 3; i++)
            {
                float SpawnX = Main.screenWidth / 2 + Main.rand.Next(-800, 800);
                Vector2 SpawnPos = new Vector2(SpawnX, -50);
                Vector2 Scale = new Vector2(Main.rand.NextFloat(0, 0.005f), Main.rand.NextFloat(0.2f, 2f));
                Vector2 Speed = LAPUtilities.GetVector2(Center, SpawnPos) * 48 * Main.rand.NextFloat(1f, 2f);
                SpriteEffects se = Main.rand.NextBool() ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                float Offset = Main.rand.NextBool() ? 0 : (MathHelper.Pi);
                rainDusts.Add(new RainDust(SpawnPos, Speed.ToRotation() + MathHelper.PiOver2 + Offset, Scale, Speed, se));
            }
            if (Time > 200 && !MenuVideoPlay.CanPlay)
            {
                RainSlotID = SoundEngine.PlaySound(LAPSoundsMenu.RainSound);
                Time = 0;
            }

            for (int i = 0; i < rainDusts.Count; i++)
            {
                rainDusts[i].Update();
            }
            rainDusts.RemoveAll(dust => dust.Time > 60);
        }
        public static void Draw()
        {
            for (int i = 0; i < rainDusts.Count; i++)
            {
                rainDusts[i].Draw();
            }
            Vector2 DrawPos = new Vector2(Main.screenWidth / 2 - 10, 345);
            Texture2D texture = BGTextureRegister.LiliesLogo.Value;
            Main.spriteBatch.Draw(texture, DrawPos, null, Color.White, 0, texture.Size() / 2, 0.9f, 0, 0);
        }
    }
}
