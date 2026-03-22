using LAP.Core.GlobalInstance.Players;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.NetCode.Content
{
    public class ReadWriteMouseWorld : BaseLAPHandlePack
    {
        public override void Read(BinaryReader reader, int whoAmI)
        {
            // 从数据包中按写入顺序读取数据
            int playerIndex = reader.ReadInt32();
            Vector2 mouseWorld = reader.ReadVector2();
            // 如果是在服务器端收到了这个包
            if (Main.netMode == NetmodeID.Server)
            {
                // 将这个信息转发给所有其他客户端，让他们也知道
                // 创建一个新的包用于广播
                ModPacket broadcastPacket = LAP.Instance.GetPacket();
                broadcastPacket.Write(Type);
                broadcastPacket.Write(playerIndex);
                broadcastPacket.WriteVector2(mouseWorld);
                // 发送给所有人 (-1)，除了原始发送者 (whoAmI)
                broadcastPacket.Send(-1, whoAmI);
            }
            // 如果是在客户端收到了服务器转发的包
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                // 在本地更新对应玩家的鼠标位置
                LAPPlayer modPlayer = Main.player[playerIndex].LAP();
                modPlayer.SyncedMouseWorld = mouseWorld;
            }
        }
    }
}
