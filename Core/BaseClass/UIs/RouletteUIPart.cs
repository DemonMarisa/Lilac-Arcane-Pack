using LAP.Core.UISystem;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Terraria;

namespace LAP.Core.BaseClass.UIs
{
    public abstract class RouletteUIPart : BaseUI
    {
        public override bool Colliding(Rectangle rectangle, Rectangle mouseRectangle)
        {
            Vector2 vectorToMouse = LAPUtilities.GetVector2(LAPUtilities.ScreenCenter(), Main.MouseScreen);
            // 规范化到π, 3π方便比较
            float mousetoCenterAngle = vectorToMouse.ToRotation();
            if (mousetoCenterAngle < 0)
            {
                mousetoCenterAngle += MathHelper.TwoPi;
            }
            float HalfAngleAdd = SectorRot / 2f;
            // 从扇形的中线角度转换为顺时针起点角度
            float CenterAngle = SectorCenterRot;
            // 规范化到 [0, 2π)
            if (CenterAngle >= MathHelper.TwoPi)
            {
                CenterAngle -= MathHelper.TwoPi;
            }
            if (LAPUtilities.IsAngleInSector(mousetoCenterAngle, CenterAngle, HalfAngleAdd))
                return true;
            else
                return false;
        }
    }
}
