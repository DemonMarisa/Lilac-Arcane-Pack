using LAP.Core.Graphics.ScreenCaustics;
using LAP.Core.Graphics.ScreenDistortion;
using Microsoft.Xna.Framework;
using Terraria;

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
        public static CausticsEntity AddScreenCaustics(int timeLeft, Vector2 position, float strength, float radius, float width = 0.05f, float Frequency = 1f, bool UseChromaticAberration = true, bool UseSaturate = false)
        {
            for (int i =0; i < CausticsSystem.MaxCaustics;i++)
            {
                if (!CausticsSystem.Caustics[i].Active)
                {
                    CausticsEntity instance = CausticsSystem.Caustics[i];
                    instance.Reset();
                    instance.Active = true;
                    instance.MaxTime = timeLeft;
                    instance.Position = position;
                    instance.TargetRadius = radius;
                    instance.TargeeStrength = strength;
                    instance.Width = width;
                    instance.Frequency = Frequency;
                    instance.UseSaturate = UseSaturate;
                    instance.UseChromaticAberration = UseChromaticAberration;
                    CausticsSystem.HasAnyCaustics = true;
                    return instance;
                }
            }
            return CausticsSystem.HandleCaustics;
        }
    }
}
