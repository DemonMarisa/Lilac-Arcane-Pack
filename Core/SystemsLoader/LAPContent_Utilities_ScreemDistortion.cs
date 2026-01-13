using LAP.Core.Graphics.ScreenDistortion;
using LAP.Core.Graphics.ScreenDistortion.Distortions;
using Microsoft.Xna.Framework;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        public static void AddScreenDistortion(int timeLeft, Vector2 position, float strength, float radius)
        {
            if (DistortionSystem.Entities.Count > 50)
                DistortionSystem.Entities.RemoveAt(0);
            DistortionSystem.Entities.Add(new StandardDistortionEntity(timeLeft, position, strength, radius));
        }
    }
}
