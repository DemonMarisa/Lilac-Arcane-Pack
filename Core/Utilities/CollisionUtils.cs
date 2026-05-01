using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        public static bool CircularHitboxCollision(Vector2 centerCheckPosition, float radius, Rectangle targetHitbox)
        {
            if (radius <= 0f)
                return false;
            float closestX = MathHelper.Clamp(centerCheckPosition.X, targetHitbox.Left, targetHitbox.Right);
            float closestY = MathHelper.Clamp(centerCheckPosition.Y, targetHitbox.Top, targetHitbox.Bottom);
            float dx = centerCheckPosition.X - closestX;
            float dy = centerCheckPosition.Y - closestY;
            return (dx * dx + dy * dy) <= (radius * radius);
        }
        public static Rectangle[] AABBCircularHitboxes(Vector2 center, float radius, int resolution = 5)
        {
            Rectangle[] generatedHitboxes = new Rectangle[resolution];
            for (int i = 0; i < resolution; i++)
            {
                float angle = (float)(Math.PI / 2.0) * ((i + 0.5f) / resolution);
                int width = (int)(radius * 2f * Math.Cos(angle));
                int height = (int)(radius * 2f * Math.Sin(angle));
                generatedHitboxes[i] = Utils.CenteredRectangle(center, new Vector2(width, height));
            }
            return generatedHitboxes;
        }
    }
}
