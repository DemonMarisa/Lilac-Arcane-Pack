using LAP.Assets.Effects;
using LAP.Core.MiscDate;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LAP.Core.Graphics.ScreenDistortion
{
    public abstract class DistortionEntity
    {
        public int Time;
        public int MaxTime;
        public Vector2 Position;
        public float Strength;
        public float TargetRadius;
        public float Radius;
        public float UVLerp;
        public virtual void Update()
        {
            float Progress = Time / (float)MaxTime;
            if (Progress < 0.5f)
            {
                Radius = MathHelper.Lerp(0f, TargetRadius, EasingHelper.EaseOutCubic(Progress * 2));
            }
            else
            {
                float Progress2 = (Progress - 0.5f) * 2f;
                Radius = MathHelper.Lerp(TargetRadius, 0, EasingHelper.EaseInCubic(Progress2));
            }
        }
        public virtual void ApplyShader()
        {
            Vector2 drawPos = Vector2.Transform(Position - Main.screenPosition, Main.GameViewMatrix.TransformationMatrix);
            float zoom = Main.GameViewMatrix.Zoom.X;
            float ScrennSizeScale = 1920f / LAPInfo.ScreenSize.X;
            float XScale = (float)Main.screenHeight / Main.screenWidth;
            Effect effect = LAPShaderRegister.ScreenDistortion.Value;
            effect.Parameters["uScreenSize"].SetValue(LAPInfo.ScreenSize);
            effect.Parameters["uTargetCenter"].SetValue(drawPos);
            effect.Parameters["uDistortionRadius"].SetValue(Radius * zoom * ScrennSizeScale);
            effect.Parameters["uDistortionStrength"].SetValue(Strength);
            effect.Parameters["uLerpFactor"].SetValue(UVLerp);
            effect.Parameters["uAspectRatioCorrection"].SetValue(new Vector2(1f, XScale));
            effect.CurrentTechnique.Passes[0].Apply();
        }
    }
}
