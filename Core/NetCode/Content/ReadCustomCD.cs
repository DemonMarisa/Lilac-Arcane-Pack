using LAP.Core.SystemsLoader;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.NetCode.Content
{
    public class ReadCustomCD : BaseLAPHandlePack
    {
        public override void Read(BinaryReader reader, int whoAmI)
        {
            // 从数据包中按写入顺序读取数据
            int playerIndex = reader.ReadInt32();
            int buffType = reader.ReadInt32();
            int buffTime = reader.ReadInt32();
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket broadcastPacket = LAP.Instance.GetPacket();
                broadcastPacket.Write(Type);
                broadcastPacket.Write(playerIndex);
                broadcastPacket.Write(buffType);
                broadcastPacket.Write(buffTime);
                broadcastPacket.Send(-1, whoAmI);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                // 其它玩家收到包并添加后不需要再发送包了
                Player player = Main.player[playerIndex];
                player.AddCD(buffType, buffTime, false);
            }
        }
    }
}
