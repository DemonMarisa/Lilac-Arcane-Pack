using LAP.Content.Particles_ECS;
using LAP.Core.Enums;
using LAP.Core.SystemsLoader;
using Microsoft.Xna.Framework;

namespace LAP.Core.Presets.Content
{
    public partial class ParticlePreset
    {
        public static int NewTOFL(Vector2 position, Vector2 vel, Color color, int lifetime, float scale, float speed = 0, float Intensity = MathHelper.TwoPi, int Blendstate = BlendStateID.Additive)
        {
            return LAPContent.NewParticle(LAPContent.ParticleType<TOFL>(), lifetime, position, vel, color, 0, scale, Blendstate, speed, scale, Intensity);
        }
        public static int NewTGlowBall(Vector2 position, Vector2 vel, Color color, int lifetime, float scale, float speed = 0, int Blendstate = BlendStateID.Additive)
        {
            return LAPContent.NewParticle(LAPContent.ParticleType<GlowBall_T>(), lifetime, position, vel, color, 0, scale, Blendstate, speed);
        }
        public static int NewTMGlowBall(Vector2 position, Vector2 vel, Color color, int lifetime, float scale, float speed = 0, int Blendstate = BlendStateID.Additive)
        {
            return LAPContent.NewParticle(LAPContent.ParticleType<GlowBall_T_M>(), lifetime, position, vel, color, 0, scale, Blendstate, speed);
        }
        public static int NewGlowLozenge(Vector2 position, Vector2 vel, Color color, int lifetime, float scale, int Blendstate = BlendStateID.Additive)
        {
            return LAPContent.NewParticle(LAPContent.ParticleType<GlowLozenge>(), lifetime, position, vel, color, 0, scale, Blendstate);
        }
        public static int NewDustGlow(Vector2 position, Vector2 vel, float rot, Color color, int lifetime, float scale, float rotspeed, int Blendstate = BlendStateID.Additive)
        {
            return LAPContent.NewParticle(LAPContent.ParticleType<DustGlow>(), lifetime, position, vel, color, rot, scale, Blendstate, scale, rotspeed);
        }
        public static int NewLightning01(Vector2 position, Vector2 vel, Color color, int lifetime, float scale, float rotation = 0, bool useBloom = false)
        {
            int index = LAPContent.NewParticle(LAPContent.ParticleType<Lightning01>(), lifetime, position, vel, color, rotation, scale, BlendStateID.Additive);
            ParticleSystem_ECS.ParticleDataManager.particleData_add[index].aibool0 = useBloom;
            return index;
        }
        public static int NewLightning02(Vector2 position, Vector2 vel, Color color, int lifetime, float scale, float rotation = 0, bool useBloom = false)
        {
            int index = LAPContent.NewParticle(LAPContent.ParticleType<Lightning02>(), lifetime, position, vel, color, rotation, scale, BlendStateID.Additive);
            ParticleSystem_ECS.ParticleDataManager.particleData_add[index].aibool0 = useBloom;
            return index;
        }
        public static int NewLightning03(Vector2 position, Vector2 vel, Color color, int lifetime, float scale, float rotation = 0, bool useBloom = false)
        {
            int index = LAPContent.NewParticle(LAPContent.ParticleType<Lightning03>(), lifetime, position, vel, color, rotation, scale, BlendStateID.Additive);
            ParticleSystem_ECS.ParticleDataManager.particleData_add[index].aibool0 = useBloom;
            return index;
        }
    }
}
