using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LAP.Core.Graphics.VFX
{
    public class VFXInstance
    {
        public VFXBehavior Behavior;
        public int WhoAmI;
        public bool Active;
        public int Time;
        public int Lifetime;
        public int ExtraUpdate;

        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Origin;
        public Color DrawColor;
        public float Rotation;
        public float Scale;
        public Vector2 Scale2D;
        public float Opacity;

        public float[] AiFloat = new float[6];
        public int[] AiInt = new int[4];
        public bool[] AiBool = new bool[4];
        public Vector2[] AiVector2 = new Vector2[4];

        public List<Vector2> OldPos = new List<Vector2>();
        public List<float> OldRot = new List<float>();
        public List<float> Oldfloat = new List<float>();
        public float LifetimeRatio => Lifetime == 0 ? 0f : (float)Time / Lifetime;
        public void Reset()
        {
            Behavior = null;

            WhoAmI = 0;
            Active = false;
            Time = 0;
            Lifetime = 0;
            ExtraUpdate = 0;

            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            Origin = Vector2.Zero;
            DrawColor = Color.White;
            Rotation = 0f;
            Scale = 1f;
            Scale2D = Vector2.One;
            Opacity = 1f;

            for (int i = 0; i < AiFloat.Length; i++)
                AiFloat[i] = 0f;
            for (int i = 0; i < AiInt.Length; i++)
                AiInt[i] = 0;
            for (int i = 0; i < AiBool.Length; i++)
                AiBool[i] = false;
            for (int i = 0; i < AiVector2.Length; i++)
                AiVector2[i] = Vector2.Zero;

            OldPos.Clear();
            OldRot.Clear();
            Oldfloat.Clear();
        }
    }
}
