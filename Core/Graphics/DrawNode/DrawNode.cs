using LAP.Content.Configs;
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
        /// <summary>
        /// 生命周期的进度，介于0到1之间。
        /// 0表示节点刚生成，1表示节点消失。
        /// </summary>
        public float LifetimeRatio => Time / (float)Lifetime;
        public DrawLayer Layer = DrawLayer.BeforeDust;
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
            // 如果粒子数量过多，则清除第一个粒子并添加
            if (NodeManager.ActiveNode.Count > LAPConfig.Instance.ParticleLimit)
                NodeManager.ActiveNode.RemoveAt(0);
            // 用于控制总数的列表
            NodeManager.ActiveNode.Add(this);
            OnSpawn();
            return this;
        }        
        /// <summary>
        /// 在世界内生成粒子
        /// </summary>
        /// <returns></returns>
        public DrawNode SpawnToPixel()
        {
            if (Main.netMode == NetmodeID.Server)
                return this;
            // 初始化时间
            Time = 0;
            // 如果粒子数量过多，则清除第一个粒子并添加
            if (NodeManager.ActivePixelNode.Count > LAPConfig.Instance.ParticleLimit)
                NodeManager.ActivePixelNode.RemoveAt(0);
            // 用于控制总数的列表
            NodeManager.ActivePixelNode.Add(this);
            OnSpawn();
            return this;
        }
        public virtual void OnSpawn() { } 
        public virtual void Update() { }
        public virtual void OnKill() { }
        public virtual void Draw(SpriteBatch sb) { }
        public virtual bool UpDatePos() { return true; }
        protected sealed override void Register()
        {
            if (!NodeManager.NodeCollection.Contains(this))
                NodeManager.NodeCollection.Add(this);

            Type = NodeManager.NodeCollection.Count;
        }
        public void Kill()
        {
            Time = Lifetime;
        }
    }
}
