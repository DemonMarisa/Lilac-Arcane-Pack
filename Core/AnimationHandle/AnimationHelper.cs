namespace LAP.Core.AnimationHandle
{
    public struct AniHelper
    {
        public int[] AniProgress = [];

        public int[] MaxAniProgress = [];

        public float[] Auxfloat = [];

        public float[] BreakTime = [];

        public bool[] HasFinish = [];

        public AniHelper(int TotalAniUnit)
        {
            // 使用 new int[length] 来创建指定长度的数组
            AniProgress = new int[TotalAniUnit];

            MaxAniProgress = new int[TotalAniUnit];

            Auxfloat = new float[TotalAniUnit];

            BreakTime = new float[TotalAniUnit];

            HasFinish = new bool[TotalAniUnit];
        }
        public void Reset(int index, bool ResetMaxAni = false)
        {
            AniProgress[index] = 0;
            Auxfloat[index] = 0;
            BreakTime[index] = 0;
            HasFinish[index] = false;
            if (ResetMaxAni)
                MaxAniProgress[index] = 0;
        }
    }
}
