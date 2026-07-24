using Microsoft.Xna.Framework;

namespace LAP.Core.Graphics.ScreenDistortion
{
    public class StandardDistortionEntity: DistortionEntity
    {
        public StandardDistortionEntity(int TimeLeft, Vector2 position, float strength, float radius)
        {
            MaxTime = TimeLeft;
            Position = position;
            Strength = strength;
            TargetRadius = radius;
        }
    }
}
