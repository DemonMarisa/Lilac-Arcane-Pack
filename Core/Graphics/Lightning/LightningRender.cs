using LAP.Assets.Effects;
using LAP.Assets.TextureRegister;
using LAP.Core.DebugSystem;
using LAP.Core.Enums;
using LAP.Core.Graphics.Primitives.Trail;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using static CalamityMod.Skies.ExoMechsSky;

namespace LAP.Core.Graphics.Lightning
{
    public partial class LightningBuilder
    {
        public static List<TrailDrawData> trailDrawData = [];
        private static List<Vector2> _drawPosBuffer = [];
        private static List<float> _drawWidthBuffer = [];
        public static void DrawLightning(On_Main.orig_DrawDust orig, Main self)
        {
            orig(self);
            if (HasAnyLightning)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                DrawSetting setting = new(LAPTextureRegister.Lightning2_T.Value, false, -1, TrailEffects.None, SamplerState.PointWrap);
                ApplyLightningShader();
                for (int i = 0; i < LAPLightnings.Length; i++)
                {
                    if (LAPLightnings[i].Active)
                    {
                        LAPLightning lightning = LAPLightnings[i];
                        ApplyValue(lightning);
                        Draw(lightning.CachedTrails, Color.White, 1f, setting);
                    }
                }
                Main.spriteBatch.End();

                DeepGlow.DeepGlow.SubmitCustomGlow(() =>
                {
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                    ApplyLightningShader();
                    for (int i = 0; i < LAPLightnings.Length; i++)
                    {
                        if (LAPLightnings[i].Active)
                        {
                            LAPLightning lightning = LAPLightnings[i];
                            ApplyValue(lightning);
                            Draw(lightning.CachedTrails, lightning.Color, 1.2f, setting);
                        }
                    }
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null);
                });
            }
        }
        public static void ApplyLightningShader()
        {
            Effect effect = LAPShaderRegister.LightningShader.Value;
            effect.Parameters["uvMullt"].SetValue(new Vector2(1f, 1f));
            effect.Parameters["noiseMult"].SetValue(new Vector2(1f, 0.5f));
            effect.Parameters["noiseAdd"].SetValue(new Vector2(Main.GlobalTimeWrappedHourly * -0.2f, 0f));
            effect.Parameters["distortionMult"].SetValue(0.4f);
            effect.Parameters["uFadeoutLeftLength"].SetValue(0.05f);
            effect.Parameters["uFadeinRigtLength"].SetValue(0.05f);
            effect.CurrentTechnique.Passes[0].Apply();
            LAPUtilities.SetTexture(LAPTextureRegister.Noise.Value, SamplerState.PointWrap, 1);
            LAPUtilities.SetTexture(LAPTextureRegister.Distortion01.Value, SamplerState.PointWrap, 2);
        }
        public static void ApplyValue(LAPLightning lightning)
        {
            Effect effect = LAPShaderRegister.LightningShader.Value;
            effect.Parameters["uvAdd"].SetValue(new Vector2(Main.GlobalTimeWrappedHourly * -0.2f + lightning.RandomFlowOffset, 0f));
            effect.Parameters["fade"].SetValue(1 - lightning.Opacity);
            effect.CurrentTechnique.Passes[0].Apply();
        }
        public static void Draw(List<List<TrailDrawData>> OldPos, Color color, float widthMult, DrawSetting setting)
        {
            foreach(List<TrailDrawData> list in OldPos)
            {
                trailDrawData.Clear();
                foreach(TrailDrawData data in list)
                {
                    trailDrawData.Add(new TrailDrawData(data.Position - Main.screenPosition, color, data.Height * widthMult, data.Rotation));
                }
                TrailRender.RenderTrail(trailDrawData, setting);
            }
        }
    }
}
