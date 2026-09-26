using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.LAPPostProgress
{
    public class LAPPostProgress  : ModType
    {
        public int Type;
        protected override void Register()
        {
            if (!LAPPostProgressManager.PostProgress.Contains(this))
                LAPPostProgressManager.PostProgress.Add(this);
        }
        /// <summary>
        /// 60帧的更新
        /// </summary>
        public virtual void Update()
        {

        }
        /// <summary>
        /// 自动取消激活的条件
        /// </summary>
        /// <returns></returns>
        public virtual bool DeActiveCondition()
        {
            return true;
        }
        /// <summary>
        /// 激活时调用
        /// </summary>
        public virtual void OnDeActive()
        {

        }
        /// <summary>
        /// 取消时调用
        /// </summary>
        public virtual void OnActive()
        {
        }
        /// <summary>
        /// 后处理的绘制
        /// </summary>
        /// <param name="finalTexture"></param>
        /// <param name="screenTarget"></param>
        /// <param name="screenTargetSwap"></param>
        /// <param name="clearColor"></param>
        public virtual void Draw(RenderTarget2D finalTexture, RenderTarget2D screenTarget, RenderTarget2D screenTargetSwap, Color clearColor)
        {

        }
    }
}
