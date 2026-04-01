using LAP.Core.Enums;
using LAP.Core.ParticleSystem_ECS;
using Microsoft.Xna.Framework;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        public static int NewParticle(int Type, int timeLeft, Vector2 position, Vector2 velocity, Color drawColor, float rotation = 0, float scale = 0, int blendstate = BlendStateID.Alpha, float ai0 = 0, float ai1 = 0, float ai2 = 0)
        {
            ParticleBehaviors p = ParticleDataManager.PAICollection[Type];
            if (blendstate == BlendStateID.Alpha)
            {
                if (ParticleDataManager.activePoint_alpha < ParticleDataManager.MaxParticle)
                {
                    ref ParticleData particleDate = ref ParticleDataManager.particleData_alpha[ParticleDataManager.activePoint_alpha];
                    ReSetParticleInfo(ref particleDate, Type, timeLeft, position, velocity, drawColor, rotation, scale, ai0, ai1, ai2);
                    ParticleDataManager.activePoint_alpha++;
                    p.OnSpawn(ref particleDate);
                    return ParticleDataManager.activePoint_alpha - 1;
                }
            }
            else if (blendstate == BlendStateID.Additive)
            {
                if (ParticleDataManager.activePoint_add < ParticleDataManager.MaxParticle)
                {
                    ref ParticleData particleDate = ref ParticleDataManager.particleData_add[ParticleDataManager.activePoint_add];
                    ReSetParticleInfo(ref particleDate, Type, timeLeft, position, velocity, drawColor, rotation, scale, ai0, ai1, ai2);
                    ParticleDataManager.activePoint_add++;
                    p.OnSpawn(ref particleDate);
                    return ParticleDataManager.activePoint_add - 1;
                }
            }
            else
            {
                if (ParticleDataManager.activePoint_Nonmult < ParticleDataManager.MaxParticle)
                {
                    ref ParticleData particleDate = ref ParticleDataManager.particleData_nopremult[ParticleDataManager.activePoint_Nonmult];
                    ReSetParticleInfo(ref particleDate, Type, timeLeft, position, velocity, drawColor, rotation, scale, ai0, ai1, ai2);
                    ParticleDataManager.activePoint_Nonmult++;
                    p.OnSpawn(ref particleDate);
                    return ParticleDataManager.activePoint_Nonmult - 1;
                }
            }
            return -1;
        }
        public static void ReSetParticleInfo(ref ParticleData particleDate, int Type, int timeLeft, Vector2 position, Vector2 velocity, Color drawColor, float rotation = 0, float scale = 0, float ai0 = 0, float ai1 = 0, float ai2 = 0)
        {
            particleDate = ParticleDataManager.TempleParticle;

            particleDate.Lifetime = timeLeft;
            particleDate.Active = true;
            particleDate.Type = Type;
            particleDate.whoAmI = ParticleDataManager.activePoint_Nonmult;
            particleDate.Position = position;
            particleDate.Velocity = velocity;
            particleDate.DrawColor = drawColor;
            particleDate.Rotation = rotation;
            particleDate.Scale = scale;
            particleDate.aifloat0 = ai0;
            particleDate.aifloat1 = ai1;
            particleDate.aifloat2 = ai2;
            particleDate.aibool0 = false;
            particleDate.aibool2 = false;
            particleDate.aibool2 = false;
            particleDate.aiint0 = 0;
            particleDate.aiint1 = 0;
            particleDate.aiint2 = 0;
        }
    }
}
