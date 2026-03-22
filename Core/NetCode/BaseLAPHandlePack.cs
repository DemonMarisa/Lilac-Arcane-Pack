using System.IO;
using Terraria.ModLoader;

namespace LAP.Core.NetCode
{
    public abstract class BaseLAPHandlePack : ModType
    {
        public int Type;
        protected override void Register()
        {
            Type = LAPNetCode.Handler.Count;
            if (!LAPNetCode.Handler.Contains(this))
                LAPNetCode.Handler.Add(this);
        }
        public virtual void Read(BinaryReader reader, int whoAmI)
        {

        }
    }
}
