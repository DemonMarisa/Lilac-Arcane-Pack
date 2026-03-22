using System.Collections.Generic;
using System.IO;
using Terraria;

namespace LAP.Core.NetCode
{
    public class LAPNetCode
    {
        public static List<BaseLAPHandlePack> Handler = [];
        public static void HandleLAPPacket(BinaryReader reader, int whoAmI)
        {
            int index = reader.ReadInt32();
            if (Handler.IndexInRange(index))
                Handler[index].Read(reader, whoAmI);
            else
                throw new IOException($"LAP : Received invalid packet with index {index} from player {whoAmI}");
        }
    }
}
