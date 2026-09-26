using LAP.Assets.Effects;
using LAP.Assets.TextureRegister;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.LAPPostProgress
{
    public class LAPPostProgressManager :ModSystem
    {
        public static List<LAPPostProgress> PostProgress = new List<LAPPostProgress>();
        public static List<LAPPostProgress> ActivePostProgress = new List<LAPPostProgress>();
        public override void Load()
        {
            if (Main.dedServ)
                return;
            // On_FilterManager.EndCapture += On_FilterManager_EndCapture;
        }
        public override void Unload()
        {
            if (Main.dedServ)
                return;
            // On_FilterManager.EndCapture -= On_FilterManager_EndCapture;
        }
        public override void PostUpdateEverything()
        {
            if (ActivePostProgress.Count != 0)
            {
                for (int i = 0; i < ActivePostProgress.Count; i++)
                {
                    var postProgress = ActivePostProgress[i];
                    postProgress.Update();
                }
                for (int i = ActivePostProgress.Count - 1; i >= 0; i--)
                {
                    var postProgress = ActivePostProgress[i];
                    if (postProgress.DeActiveCondition())
                    {
                        postProgress.OnDeActive();
                        ActivePostProgress.RemoveAt(i);
                    }
                }
            }
        }
        public static void On_FilterManager_EndCapture(On_FilterManager.orig_EndCapture orig, FilterManager self, RenderTarget2D finalTexture, RenderTarget2D screenTarget, RenderTarget2D screenTargetSwap, Color clearColor)
        {
            if (!Main.dedServ && ActivePostProgress.Count != 0)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null);

                for (int i = 0; i < ActivePostProgress.Count; i++)
                {
                    var postProgress = ActivePostProgress[i];
                    postProgress.Draw(finalTexture, screenTarget, screenTargetSwap, clearColor);
                }

                Main.spriteBatch.End();
            }

            orig(self, finalTexture, screenTarget, screenTargetSwap, clearColor);
        }
    }
}
