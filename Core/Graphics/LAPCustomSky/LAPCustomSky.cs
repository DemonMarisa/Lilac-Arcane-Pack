using LAP.Assets.TextureRegister;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.LAPCustomSky
{
    public class LAPCustomSky : ModType
    {
        public int Type;
        protected override void Register()
        {
            if (!LAPCustomSkyManager.CustomSkies.Contains(this))
                LAPCustomSkyManager.CustomSkies.Add(this);
        }
        public virtual void Update()
        {
        }
        /// <summary>
        /// 默认在DrawUnderGround之前
        /// </summary>
        public virtual void Draw()
        {
            Main.spriteBatch.Draw(LAPTextureRegister.WhiteCube.Value, LAPUtilities.ScreenCenter(), null, Color.Black, 0, LAPTextureRegister.WhiteCube.Size() / 2, 100, SpriteEffects.None, default);
        }
        /// <summary>
        /// 默认在DrawUnderGround之后
        /// </summary>
        public virtual void DrawAfterUnderGroundBG()
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
    }
}
