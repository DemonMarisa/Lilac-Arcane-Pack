using LAP.Core.GlobalInstance.Players;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.NetCode
{
    public class LAPNetCode
    {
        public enum MessageType : byte
        {
            SyncMousePosition
        }
        public static void HandleMouseWorldPacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();

            switch (msgType)
            {
                case MessageType.SyncMousePosition:
                    // 从数据包中按写入顺序读取数据
                    byte playerIndex = reader.ReadByte();
                    Vector2 mouseWorld = reader.ReadVector2();
                    bool mouseLeft = reader.ReadBoolean();
                    bool mouseRight = reader.ReadBoolean();
                    // 如果是在服务器端收到了这个包
                    if (Main.netMode == NetmodeID.Server)
                    {
                        // 将这个信息转发给所有其他客户端，让他们也知道
                        // 创建一个新的包用于广播
                        ModPacket broadcastPacket = LAP.Instance.GetPacket();
                        broadcastPacket.Write((byte)MessageType.SyncMousePosition);
                        broadcastPacket.Write(playerIndex);
                        broadcastPacket.WriteVector2(mouseWorld);
                        broadcastPacket.Write(mouseLeft);
                        broadcastPacket.Write(mouseRight);
                        // 发送给所有人 (-1)，除了原始发送者 (whoAmI)
                        broadcastPacket.Send(-1, whoAmI);
                    }
                    // 如果是在客户端收到了服务器转发的包
                    else if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        // 在本地更新对应玩家的鼠标位置
                        LAPPlayer modPlayer = Main.player[playerIndex].GetModPlayer<LAPPlayer>();
                        modPlayer.SyncedMouseWorld = mouseWorld;
                        modPlayer.MouseLeft = mouseLeft;
                        modPlayer.MouseRight = mouseRight;
                    }
                    break;
            }
        }
    }
}
