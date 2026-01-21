using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.ScreenDistortion
{
    public class DistortionSystem : ModSystem
    {
        public static List<DistortionEntity> Entities = [];
        public override void Load()
        {
            if (Main.dedServ)
                return;
            On_FilterManager.EndCapture += On_FilterManager_EndCapture;
        }
        public override void Unload()
        {
            if (Main.dedServ)
                return;
            On_FilterManager.EndCapture -= On_FilterManager_EndCapture;
        }
        public override void PostUpdateDusts()
        {
            if (Main.dedServ)
                return;
            if (Entities.Count != 0)
            {
                for (int i = 0; i < Entities.Count; i++)
                {
                    Entities[i].Update();
                    Entities[i].Time++;
                }
                Entities.RemoveAll(x => x.Time >= x.MaxTime);
            }
        }
        public static void On_FilterManager_EndCapture(On_FilterManager.orig_EndCapture orig, FilterManager self, RenderTarget2D finalTexture, RenderTarget2D screenTarget, RenderTarget2D screenTargetSwap, Color clearColor)
        {
            if (!Main.dedServ && Entities.Count != 0)
            {
                for (int i = 0; i < Entities.Count; i++)
                {
                    screenTargetSwap.SwapToTarget();
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null);
                    Entities[i].ApplyShader();
                    Main.spriteBatch.Draw(screenTarget, Vector2.Zero, Color.White);
                    Main.spriteBatch.End();
                    screenTarget.SwapToTarget();
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, Main.Rasterizer, null);
                    Main.spriteBatch.Draw(screenTargetSwap, Vector2.Zero, Color.White);
                    Main.spriteBatch.End();
                }
            }
            orig(self, finalTexture, screenTarget, screenTargetSwap, clearColor);
        }
    }
}
