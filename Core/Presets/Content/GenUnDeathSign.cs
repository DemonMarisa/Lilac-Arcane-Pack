using LAP.Content.Configs;
using LAP.Content.MetaBalls;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace LAP.Core.Presets.Content
{
    public partial class ParticlePreset
    {
        public static void GenUnDeathSign(Vector2 firePos, float speedMult = 1)
        {
            if (LAPConfig.Instance.PerformanceMode)
                speedMult *= 0.7f;
            // 生成星形
            for (int i = 0; i < 60; i++)
            {
                float offsetAngle = MathHelper.TwoPi * i / 60f;

                // Parametric equations for an asteroid.
                float unitOffsetX = (float)Math.Pow(Math.Cos(offsetAngle), 5D);
                float unitOffsetY = (float)Math.Pow(Math.Sin(offsetAngle), 5D);

                Vector2 puffDustVelocity = new Vector2(unitOffsetX, unitOffsetY) * 7f * speedMult;

                NebulaMetaBall.SpawnParticle(firePos,
                    puffDustVelocity,
                    0.13f);
            }

            // 生成四条线
            for (int i = 0; i < 6; i++)
            {
                float offsetAngle = MathHelper.TwoPi * i / 4f + MathHelper.PiOver4;
                Vector2 vector = offsetAngle.ToRotationVector2() * 4 * speedMult;
                for (int j = 0; j < 10; j++)
                {
                    NebulaMetaBall.SpawnParticle(firePos, vector + vector * (j / 10f), 0.15f);
                }
            }

            // 生成四条线的切线
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Vector2 beginVector = new(1, 0.3f);
                    Vector2 endVector = new(1, -0.3f);
                    Vector2 vector = Vector2.Lerp(beginVector, endVector, j / 5f);
                    NebulaMetaBall.SpawnParticle(firePos, vector.RotatedBy(MathHelper.PiOver4 + MathHelper.PiOver2 * i) * 5.7f * speedMult, 0.15f);
                }
            }
        }
    }
}
