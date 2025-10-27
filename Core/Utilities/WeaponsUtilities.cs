using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        public static Vector2 GetMagicStuffRotPoint(this Projectile projectile, Texture2D texture)
        {
            Vector2 rotationPoint = projectile.spriteDirection == 1 ? new Vector2(0, texture.Height) : new Vector2(texture.Width, texture.Height);
            return rotationPoint;
        }

        public static Vector2 GetPlayerToMouseVector2(this Player player)
        {
            Vector2 vector = player.LocalMouseWorld() - player.Center;
            vector = vector.SafeNormalize(Vector2.UnitX);
            return vector;
        }

        public static Vector2 GetToMouseVector2(this Player player, Vector2 BeginPos)
        {
            Vector2 vector = player.LocalMouseWorld() - BeginPos;
            vector = vector.SafeNormalize(Vector2.UnitX);
            return vector;
        }

        public static Vector2 GetVector2(Vector2 BeginPos, Vector2 EndPos)
        {
            Vector2 vector = EndPos - BeginPos;
            vector = vector.SafeNormalize(Vector2.UnitX);
            return vector;
        }

        public static Vector2 BetterRotatedBy(this Vector2 spinningpoint, double radians, Vector2 center = default, float Xmult = 1f, float Ymult = 1f)
        {
            float num = (float)Math.Cos(radians);
            float num2 = (float)Math.Sin(radians);
            Vector2 vector = spinningpoint - center;
            Vector2 result = center;
            result.X += (vector.X * num - vector.Y * num2) * Xmult;
            result.Y += (vector.X * num2 + vector.Y * num) * Ymult;
            return result;
        }
    }
}
