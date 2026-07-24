using LAP.Assets.Effects;
using LAP.Core.MiscDate;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LAP.Core.Graphics.ScreenCaustics
{
    public class CausticsEntity
    {
        public bool Active;
        public int WhoAmI;
        public int Time;
        public int MaxTime;
        public Vector2 Position;
        // 扭曲强度
        public float TargeeStrength;
        public float Strength;
        public float TargetRadius;
        // 半径
        public float Radius;
        // 宽度
        public float Width;
        // 频率
        public float Frequency;
        // 是否忽视小于0的部分
        public bool UseSaturate;
        // 是否使用色散
        public bool UseChromaticAberration;
        public void Reset()
        {
            Active = false;
            Time = 0;
            MaxTime = 0;
            Position = Vector2.Zero;
            TargeeStrength = 0f;
            Strength = 0f;
            TargetRadius = 0f;
            Radius = 0f;
            Width = 0f;
            Frequency = 0f;
            UseSaturate = false;
            UseChromaticAberration = false;
        }
        public virtual void Update()
        {
            float Progress = Time / (float)MaxTime;
            Radius = MathHelper.Lerp(0f, TargetRadius, EasingHelper.EaseOutCubic(Progress));
            if (Progress < 0.5f)
                Strength = MathHelper.Lerp(0f, TargeeStrength, EasingHelper.EaseOutCubic(Progress * 2));
            else
                Strength = MathHelper.Lerp(TargeeStrength, 0f, EasingHelper.EaseOutCubic((Progress - 0.5f) * 2));
        }
        public virtual void ApplyShader()
        {
            Vector2 drawPos = Vector2.Transform(Position - Main.screenPosition, Main.GameViewMatrix.TransformationMatrix);
            float zoom = Main.GameViewMatrix.Zoom.X;

            Effect effect = LAPShaderRegister.ScreenCausticsShader.Value;
            effect.Parameters["uCenter"].SetValue(drawPos / LAPInfo.ScreenSize);
            effect.Parameters["uRadius"].SetValue(Radius * zoom);
            effect.Parameters["uWidth"].SetValue(Width * zoom);
            effect.Parameters["uIntensity"].SetValue(-Strength);
            effect.Parameters["uFrequency"].SetValue(Frequency);
            effect.Parameters["uScreenResolution"].SetValue(LAPInfo.ScreenSize);
            effect.Parameters["useSaturate"].SetValue(UseSaturate);
            effect.Parameters["UseChromaticAberration"].SetValue(UseChromaticAberration);
            effect.CurrentTechnique.Passes[0].Apply();
        }
    }
}
