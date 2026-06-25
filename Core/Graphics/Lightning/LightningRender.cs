using LAP.Assets.Effects;
using LAP.Assets.TextureRegister;
using LAP.Core.Enums;
using LAP.Core.Graphics.Primitives.Trail;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;

namespace LAP.Core.Graphics.Lightning
{
    public partial class LightningBuilder
    {
        public static List<TrailDrawData> trailDrawData = [];
        public static void DrawLightning(On_Main.orig_DrawDust orig, Main self)
        {
            orig(self);
            if (HasAnyLightning)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                DrawSetting setting = new(LAPTextureRegister.WhiteCube.Value, false, 2, TrailEffects.None, SamplerState.PointWrap);
                for (int i = 0; i < LAPLightnings.Length; i++)
                {
                    if (LAPLightnings[i].Active)
                    {
                        LAPLightning lightning = LAPLightnings[i];
                        ApplyLightningShader(lightning);
                        Draw(lightning.Nodes, lightning.Color, lightning.Width * lightning.xScale, setting);
                    }
                }
                DeepGlow.DeepGlow.SubmitCustomGlow(() =>
                {
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                    for (int i = 0; i < LAPLightnings.Length; i++)
                    {
                        if (LAPLightnings[i].Active)
                        {
                            LAPLightning lightning = LAPLightnings[i];
                            ApplyLightningShader(lightning);
                            Draw(lightning.Nodes, lightning.GlowColor, lightning.Width * lightning.xScale, setting);
                        }
                    }
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null);
                });
                Main.spriteBatch.End();
            }
        }
        public static void ApplyLightningShader(LAPLightning lightning)
        {
            Effect effect = LAPShaderRegister.LightningShader.Value;
            effect.Parameters["uvMullt"].SetValue(new Vector2(1f, 1f));
            effect.Parameters["uvAdd"].SetValue(new Vector2(0, 0f));
            effect.Parameters["noiseMult"].SetValue(new Vector2(1f, 0.5f));
            effect.Parameters["noiseAdd"].SetValue(new Vector2(lightning.RandomFlowOffset, 0f));
            effect.Parameters["fade"].SetValue(lightning.Opacity);
            effect.CurrentTechnique.Passes[0].Apply();
            Main.graphics.GraphicsDevice.Textures[1] = LAPTextureRegister.Noise.Value;
        }
        public static void Draw(IReadOnlyList<Vector2> OldPos, Color color, float height, DrawSetting setting)
        {
            trailDrawData.Clear();
            float rot = 0;
            for (int i = 0; i < OldPos.Count; i++)
            {
                if (i < OldPos.Count - 1)
                    rot = LAPUtilities.GetVector2(OldPos[i], OldPos[i + 1]).ToRotation();
                trailDrawData.Add(new TrailDrawData(OldPos[i] - Main.screenPosition, color, height, rot));
            }
            TrailRender.RenderTrail(trailDrawData, setting);
        }
    }
}
