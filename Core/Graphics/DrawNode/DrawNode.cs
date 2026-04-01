using LAP.Core.Enums;
using LAP.Core.SystemsLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.DrawNode
{
    /// <summary>
    /// 用于外挂数据
    /// </summary>
    public abstract class DrawNode : ModType
    {
        public int Type = 0;
        /// <summary>
        /// 这个节点存在了多少帧，一般不需要手动修改这个值
        /// </summary>
        public int Time;
        /// <summary>
        /// 节点的存在时间上限
        /// </summary>
        public int Lifetime = 0;
        public int ExtraUpdate = 0;
        /// <summary>
        /// 位置与向量
        /// </summary>
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Origin;
        public Color DrawColor;
        public float Rotation;
        public float Scale = 1f;
        public float Opacity = 1f;
        public virtual bool UseShader => false;
        /// <summary>
        /// 生命周期的进度，介于0到1之间。
        /// 0表示节点刚生成，1表示节点消失。
        /// </summary>
        public float LifetimeRatio => Time / (float)Lifetime;
        public virtual DrawLayer Layer => DrawLayer.AfterDusts;
        public virtual int BlendState => BlendStateID.Additive;
        /// <summary>
        /// 在世界内生成粒子
        /// </summary>
        /// <returns></returns>
        public DrawNode Spawn()
        {
            if (Main.netMode == NetmodeID.Server)
                return this;
            // 初始化时间
            Time = 0;
            OnSpawn();
            int total = LAPContent.GetTotalNode();
            if (total > 1000)
                return this;
            if (!UseShader)
            {
                if (Layer == DrawLayer.AfterDusts)
                {
                    if (BlendState == BlendStateID.Additive) NodeManager.PostDustAdd.Add(this);
                    if (BlendState == BlendStateID.Alpha) NodeManager.PostDustAlpha.Add(this);
                    if (BlendState == BlendStateID.NonPremult) NodeManager.PostDustNonPreMult.Add(this);
                }
                else if (Layer == DrawLayer.BeforeProjectiles)
                {
                    if (BlendState == BlendStateID.Additive) NodeManager.PreProjectileAdd.Add(this);
                    if (BlendState == BlendStateID.Alpha) NodeManager.PreProjectileAlpha.Add(this);
                    if (BlendState == BlendStateID.NonPremult) NodeManager.PreProjectileNonPreMult.Add(this);
                }
            }
            else
            {
                if (Layer == DrawLayer.AfterDusts)
                {
                    if (BlendState == BlendStateID.Additive) NodeManager.PostDustAddNoShader.Add(this);
                    if (BlendState == BlendStateID.Alpha) NodeManager.PostDustAlphaNoShader.Add(this);
                    if (BlendState == BlendStateID.NonPremult) NodeManager.PostDustNonPreMultNoShader.Add(this);
                }
                else if (Layer == DrawLayer.BeforeProjectiles)
                {
                    if (BlendState == BlendStateID.Additive) NodeManager.PreProjAddNoShader.Add(this);
                    if (BlendState == BlendStateID.Alpha) NodeManager.PreProjAlphaNoShader.Add(this);
                    if (BlendState == BlendStateID.NonPremult) NodeManager.PreProjNonPreMultNoShader.Add(this);
                }
            }
            return this;
        }        
        public virtual void OnSpawn() { } 
        public virtual void Update() { }
        public virtual void OnKill() { }
        public virtual void Draw(SpriteBatch sb) { }
        public virtual bool UpDatePos() { return true; }
        protected sealed override void Register()
        {
            Type = NodeManager.NodeCollection.Count;
            if (!NodeManager.NodeCollection.Contains(this))
                NodeManager.NodeCollection.Add(this);
        }
        public void Kill()
        {
            Time = Lifetime;
        }
    }
}
