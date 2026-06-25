using LAP.Core.Enums;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.VFX
{
    public abstract class VFXBehavior : ModType
    {
        public VFXInstance VFXInstance = null;
        public int Type { get; private set; }
        protected sealed override void Register()
        {
            Type = VFXManager.VBehavior.Count;
            if (!VFXManager.VBehavior.Contains(this))
                VFXManager.VBehavior.Add(this);
        }
        // 由子类决定自己处于什么渲染层、什么混合模式
        public virtual DrawLayer Layer => DrawLayer.AfterDusts;
        public virtual BlendState BlendState => BlendState.Additive;
        public virtual void OnSpawn() { }
        public virtual bool UpdatePosition() => true;
        public virtual void Update() { }
        public virtual void OnKill() { }
        public virtual void Draw() { }
        public virtual VFXBehavior CloneForSpawner()
        {
            return MemberwiseClone() as VFXBehavior;
        }
    }
}
