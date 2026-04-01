using LAP.Content.Particles_ECS;
using LAP.Core.Enums;
using LAP.Core.SystemsLoader;
using Microsoft.Xna.Framework;

namespace LAP.Core.Presets.Content
{
    public partial class ParticlePreset
    {
        public static int NewTGlowBall(Vector2 position, Vector2 vel, Color color, int lifetime, float scale, float speed = 0, int Blendstate = BlendStateID.Additive)
        {
            return LAPContent.NewParticle(LAPContent.ParticleType<GlowBall_T>(), lifetime, position, vel, color, 0, scale, Blendstate, speed);
        }
    }
}
