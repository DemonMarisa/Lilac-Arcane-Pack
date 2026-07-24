using LAP.Core.UISystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;

namespace LAP.Core.BaseClass.UIs
{

    /// <summary>
    /// 使用的时候不要往SD或者SSD或者Load写一些数据的加载，不知道为什么，非常容易出问题
    /// </summary>
    public abstract class RouletteUI : BaseUI
    {
        public float BeginSctorCenterRot;
        public override bool BlockMouseInput => false;
        public override void OnActive()
        {
            int sectorNum = Subset.Count;
            float angle = MathHelper.TwoPi / sectorNum;
            for (int i = 0; i < Subset.Count; i++)
            {
                BaseUI ui = Subset[i];
                ui.SectorCenterRot = angle * i + BeginSctorCenterRot;
                ui.SectorRot = angle;
                ui.OnActive();
            }
            PostOnActive();
        }
        public virtual void PostOnActive()
        {

        }
        public override bool Colliding(Rectangle rectangle, Rectangle mouseRectangle) => true;
        public override void Draw()
        {
            if (PreDraw())
            {
                for (int i = 0; i < Subset.Count; i++)
                {
                    BaseUI ui = Subset[i];
                    ui.Draw();
                }
            }
            PostDraw();
        }
        public virtual bool PreDraw()
        {
            return true;
        }
        public virtual void PostDraw()
        {
        }
    } 
}
