using LAP.Core.GlobalInstance.Players.DashSystem;
using LAP.Core.Graphics.VFX;
using LAP.Core.LAPUI.CustomCD;
using LAP.Core.MetaBallsSystem;
using LAP.Core.NetCode;
using LAP.Core.ParticleSystem;
using LAP.Core.ParticleSystem_ECS;
using LAP.Core.UISystem;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        public static int MetaBallType<T>() where T : BaseMetaBall => GetInstance<T>()?.Type ?? 0;
        public static int CDType<T>() where T : BaseCD => GetInstance<T>()?.Type ?? 0;
        public static int DashType<T>() where T : BasePlayerDash => GetInstance<T>()?.Type ?? 0;
        public static int PackHandleType<T>() where T : BaseLAPHandlePack => GetInstance<T>()?.Type ?? 0;
        public static int UIType<T>() where T : BaseUI => GetInstance<T>()?.Type ?? 0;
        public static int ParticleType<T>() where T : ParticleBehaviors => GetInstance<T>()?.Type ?? 0;
        public static int VFXType<T>() where T : VFXBehavior => GetInstance<T>()?.Type ?? 0;
        public static int GetTotalParticle()
        {
            return BaseParticleManager.ActiveParticlesAlpha.Count + BaseParticleManager.ActiveParticlesNonPremultiplied.Count + 
                BaseParticleManager.ActiveParticlesAdditive.Count + BaseParticleManager.PriorityActiveParticlesAlpha.Count + 
                BaseParticleManager.PriorityActiveParticlesNonPremultiplied.Count + BaseParticleManager.PriorityActiveParticlesAdditive.Count;
        }
    }
}
