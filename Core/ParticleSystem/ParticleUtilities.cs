using LAP.Content.Configs;
using System;

namespace LAP.Core.ParticleSystem
{
    public static class ParticleUtilities
    {
        public static void AddToAlpha(this BaseParticle p)
        {
            if (!p.Important && BaseParticleManager.ActiveParticlesAlpha.Count > LAPConfig.Instance.ParticleLimit)
                BaseParticleManager.ActiveParticlesAlpha.RemoveAt(0);
            BaseParticleManager.ActiveParticlesAlpha.Add(p);
        }

        public static void AddToADD(this BaseParticle p)
        {
            if (!p.Important && BaseParticleManager.ActiveParticlesAdditive.Count > LAPConfig.Instance.ParticleLimit)
                BaseParticleManager.ActiveParticlesAdditive.RemoveAt(0);
            BaseParticleManager.ActiveParticlesAdditive.Add(p);
        }

        public static void AddToNP(this BaseParticle p)
        {
            if (!p.Important && BaseParticleManager.ActiveParticlesNonPremultiplied.Count > LAPConfig.Instance.ParticleLimit)
                BaseParticleManager.ActiveParticlesNonPremultiplied.RemoveAt(0);
            BaseParticleManager.ActiveParticlesNonPremultiplied.Add(p);
        }

        public static void AddToPAlpha(this BaseParticle p)
        {
            if (!p.Important && BaseParticleManager.PriorityActiveParticlesAlpha.Count > LAPConfig.Instance.ParticleLimit)
                BaseParticleManager.PriorityActiveParticlesAlpha.RemoveAt(0);
            BaseParticleManager.PriorityActiveParticlesAlpha.Add(p);
        }

        public static void AddToPADD(this BaseParticle p)
        {
            if (!p.Important && BaseParticleManager.PriorityActiveParticlesAdditive.Count > LAPConfig.Instance.ParticleLimit)
                BaseParticleManager.PriorityActiveParticlesAdditive.RemoveAt(0);
            BaseParticleManager.PriorityActiveParticlesAdditive.Add(p);
        }

        public static void AddToPNP(this BaseParticle p)
        {
            if (!p.Important && BaseParticleManager.PriorityActiveParticlesNonPremultiplied.Count > LAPConfig.Instance.ParticleLimit)
                BaseParticleManager.PriorityActiveParticlesNonPremultiplied.RemoveAt(0);
            BaseParticleManager.PriorityActiveParticlesNonPremultiplied.Add(p);
        }
    }
}
