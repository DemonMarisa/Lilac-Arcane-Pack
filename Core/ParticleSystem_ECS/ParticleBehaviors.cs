using Terraria.ModLoader;

namespace LAP.Core.ParticleSystem_ECS
{
    /// <summary>
    /// 粒子的状态机，不要在这里用实例，因为是单实例，用于ECS架构的粒子
    /// </summary>
    public abstract class ParticleBehaviors : ModType
    {
        public int Type;
        /// <summary>
        /// 最高只支持20个OldPos
        /// </summary>
        public virtual int MaxOldData => 0;
        public virtual int ExtraUpdate => 0;
        protected override void Register()
        {
            Type = ParticleDataManager.PAICollection.Count;
            if (!ParticleDataManager.PAICollection.Contains(this))
                ParticleDataManager.PAICollection.Add(this);
        }
        public virtual void OnSpawn(ref ParticleData particleDate) { }
        /// <summary>
        /// 粒子的更新，默认不做任何操作
        /// </summary>
        public virtual void Update(ref ParticleData particleDate) { }
        public virtual void OnKill(ref ParticleData particleDate) { }
        /// <summary>
        /// 覆写这个就可以自定义绘制
        /// </summary>
        public virtual void Draw(ref ParticleData particleDate) { }
    }
}
