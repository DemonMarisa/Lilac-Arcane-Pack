using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace LAP.Core.StateMachine.SynedHitEffect
{
    public abstract class BaseHitEffect : ModType
    {
        public int Type;
        protected override void Register()
        {
            Type = HitEffectManager.HitEffect.Count;
            if (!HitEffectManager.HitEffect.Contains(this))
                HitEffectManager.HitEffect.Add(this);
        }
        public virtual void HitEffect(Entity entity, IEntitySource source, Player owner)
        {

        }
    }
}
