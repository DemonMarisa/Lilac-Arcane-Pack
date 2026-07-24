using Microsoft.Xna.Framework;

namespace LAP.Core.ParticleSystem_ECS
{
    public unsafe struct ParticleData()
    {
        public fixed float oldPosX[20];
        public fixed float oldPosY[20];
        public fixed float oldRot[20];
        public bool ExtraUpdate;
        public int Type;
        public int whoAmI;
        public bool Active;
        public float Time;
        public float Lifetime;
        public float ScreenCut = 0.2f;
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Origin;
        public Color DrawColor;
        public float Opacity;
        public float Rotation;
        public float Scale = 1f;
        public Vector2 Scale2 = Vector2.One;
        public float aifloat0;
        public int aiint0;
        public bool aibool0;
        public float aifloat1;
        public int aiint1;
        public bool aibool1;
        public float aifloat2;
        public int aiint2;
        public bool aibool2;
        public float aifloat3;
        public readonly float LifetimeRatio => Time / Lifetime;
        public Vector2 GetOldPos(int index)
        {
            if (index > 20)
                index = 20;
            return new Vector2(oldPosX[index], oldPosY[index]);
        }
    }
}
