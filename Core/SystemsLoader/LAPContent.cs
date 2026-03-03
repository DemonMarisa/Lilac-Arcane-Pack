using LAP.Core.GlobalInstance.Players.DashSystem;
using LAP.Core.Graphics.DrawNode;
using LAP.Core.LAPUI.CustomCD;
using LAP.Core.MetaBallsSystem;
using LAP.Core.ParticleSystem;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        public static int MetaBallType<T>() where T : BaseMetaBall => GetInstance<T>()?.Type ?? 0;
        public static int CDType<T>() where T : BaseCD => GetInstance<T>()?.Type ?? 0;
        public static int DashType<T>() where T : BasePlayerDash => GetInstance<T>()?.Type ?? 0;
        public static int GetTotalNode()
        {
            return NodeManager.PostDustAlpha.Count + NodeManager.PostDustNonPreMult.Count + NodeManager.PostDustAdd.Count + NodeManager.PreProjectileAlpha.Count + NodeManager.PreProjectileNonPreMult.Count + NodeManager.PreProjectileAdd.Count;
        }
        public static int GetTotalParticle()
        {
            return BaseParticleManager.ActiveParticlesAlpha.Count + BaseParticleManager.ActiveParticlesNonPremultiplied.Count + BaseParticleManager.ActiveParticlesAdditive.Count + BaseParticleManager.PriorityActiveParticlesAlpha.Count + BaseParticleManager.PriorityActiveParticlesNonPremultiplied.Count + BaseParticleManager.PriorityActiveParticlesAdditive.Count;
        }
    }
}
