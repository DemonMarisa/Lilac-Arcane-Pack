using Microsoft.Xna.Framework;

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
    }
}
