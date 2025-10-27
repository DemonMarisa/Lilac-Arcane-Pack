using LAP.Core.AnimationHandle;
using Microsoft.Xna.Framework;
using Terraria;
using UCA.Core.GlobalInstance.Projectiles;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        public static LAPGlobalProj LAP(this Projectile proj)
        {
            return proj.GetGlobalProjectile<LAPGlobalProj>();
        }

        public static bool PressLeftAndRightClick()
        {
            return Main.mouseLeft && Main.mouseRight;
        }
        public static bool JustPressLeftClick()
        {
            return Main.mouseLeft && !Main.mouseRight;
        }

        public static bool JustPressRightClick()
        {
            return !Main.mouseLeft && Main.mouseRight;
        }
        public static bool PressLeftAndRightClick(this Player player)
        {
            return player.LAP().MouseLeft && player.LAP().MouseRight;
        }
        public static bool JustPressLeftClick(this Player player)
        {
            return player.LAP().MouseLeft && !player.LAP().MouseRight;
        }

        public static bool JustPressRightClick(this Player player)
        {
            return !player.LAP().MouseLeft && player.LAP().MouseRight;
        }

        public static Vector2 LocalMouseWorld(this Player player)
        {
            return player.LAP().SyncedMouseWorld;
        }
        public static void UpDateAni(this AnimationHelper animationHelper, int index, int Break = 0)
        {
            if (animationHelper.AniProgress[index] < animationHelper.MaxAniProgress[index])
                animationHelper.AniProgress[index]++;

            if (animationHelper.AniProgress[index] >= animationHelper.MaxAniProgress[index])
            {
                animationHelper.Auxfloat[index]++;
                if (animationHelper.Auxfloat[index] >= Break)
                    animationHelper.HasFinish[index] = true;
            }
        }
        public static float UpDateAngle(this AnimationHelper animationHelper,float BeginAngle, float EndAngle, int Filp, float Progress, float PreFilpAdd = 0)
        {
            float startAngleOffset = MathHelper.ToRadians(BeginAngle);
            float endAngleOffset = MathHelper.ToRadians(EndAngle);
            float baseRotation = MathHelper.Lerp(startAngleOffset, endAngleOffset, Progress) + PreFilpAdd;
            if (Filp == -1)
                baseRotation = baseRotation * Filp;
            return baseRotation;
        }
    }
}
