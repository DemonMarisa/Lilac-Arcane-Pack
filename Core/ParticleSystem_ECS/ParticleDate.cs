using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAP.Core.ParticleSystem_ECS
{
    public struct ParticleDate()
    {
        public bool Active;
        public float Time;
        public float Lifetime;
        public float[] ai = new float[8];
    }
}
