using LAP.Core.AnimationHandle;
using LAP.Core.Enums;
using Microsoft.Xna.Framework;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
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
        public static float UpDateAngle(this AnimationHelper animationHelper, float BeginAngle, float EndAngle, int Filp, float Progress, float PreFilpAdd = 0)
        {
            float startAngleOffset = MathHelper.ToRadians(BeginAngle);
            float endAngleOffset = MathHelper.ToRadians(EndAngle);
            float baseRotation = MathHelper.Lerp(startAngleOffset, endAngleOffset, Progress) + PreFilpAdd;
            if (Filp == -1)
                baseRotation = baseRotation * Filp;
            return baseRotation;
        }

        public static float GetProgress(this AnimationHelper animationHelper, int index)
        {
            int MaxAni = animationHelper.MaxAniProgress[index];
            int CurAni = animationHelper.AniProgress[index];
            float easedProgress = CurAni / (float)MaxAni;
            return easedProgress;
        }
    }
}
