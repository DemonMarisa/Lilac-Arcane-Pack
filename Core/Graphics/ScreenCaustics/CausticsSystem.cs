using LAP.Core.Graphics.Lightning;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.ScreenCaustics
{
    public class CausticsSystem : ModSystem
    {
        public static bool HasAnyCaustics;
        public const int MaxCaustics = 50;
        public static CausticsEntity HandleCaustics = new CausticsEntity();
        public static CausticsEntity[] Caustics = new CausticsEntity[MaxCaustics];
        public override void Load()
        {
            if (Main.dedServ)
                return;
            On_FilterManager.EndCapture += On_FilterManager_EndCapture;
            for (int i = 0; i < MaxCaustics; i++)
            {
                Caustics[i] = new CausticsEntity { WhoAmI = i, Active = false };
            }
        }
        public override void Unload()
        {
            if (Main.dedServ)
                return;
            On_FilterManager.EndCapture -= On_FilterManager_EndCapture;
            for (int i = 0; i < MaxCaustics; i++)
            {
                Caustics[i] = null;
            }
        }
        public override void PostUpdateDusts()
        {
            if (!Main.dedServ && HasAnyCaustics)
            {
                bool HasActive = false;
                for (int i = 0; i < Caustics.Length; i++)
                {
                    if (Caustics[i].Active)
                    {
                        HasActive = true;
                        Caustics[i].Update();
                        Caustics[i].Time++;
                        if (Caustics[i].Time > Caustics[i].MaxTime)
                            Caustics[i].Active = false;
                    }
                }
                HasAnyCaustics = HasActive;
            }
        }
        public static void On_FilterManager_EndCapture(On_FilterManager.orig_EndCapture orig, FilterManager self, RenderTarget2D finalTexture, RenderTarget2D screenTarget, RenderTarget2D screenTargetSwap, Color clearColor)
        {
            if (!Main.dedServ && HasAnyCaustics)
            {
                for (int i = 0; i < Caustics.Length; i++)
                {
                    if (Caustics[i].Active)
                    {
                        screenTargetSwap.SwapToTarget();
                        Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null);
                        Caustics[i].ApplyShader();
                        Main.spriteBatch.Draw(screenTarget, Vector2.Zero, Color.White);
                        Main.spriteBatch.End();

                        screenTarget.SwapToTarget();
                        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, Main.Rasterizer, null);
                        Main.spriteBatch.Draw(screenTargetSwap, Vector2.Zero, Color.White);
                        Main.spriteBatch.End();
                    }
                }
            }

            orig(self, finalTexture, screenTarget, screenTargetSwap, clearColor);
        }
    }
}
