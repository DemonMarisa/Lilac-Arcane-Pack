using LAP.Core.GlobalInstance.Players;
using LAP.Core.SystemsLoader;
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
            SyncMousePosition,
            SyncMouseLeft,
            SyncMouseRight,
            SyncWeaponSkillKey,
            SyncCustomCD
        }
        public static void HandleMouseWorldPacket(BinaryReader reader, int whoAmI)
        {
            // 第一个读取消息类型
            MessageType msgType = (MessageType)reader.ReadByte();

            switch (msgType)
            {
                case MessageType.SyncMousePosition:
                    ReadWriteMouseWorld(reader, whoAmI);
                    break;
                case MessageType.SyncMouseLeft:
                    ReadWriteMouseLeft(reader, whoAmI);
                    break;
                case MessageType.SyncMouseRight:
                    ReadWriteMouseRight(reader, whoAmI);
                    break;
                case MessageType.SyncWeaponSkillKey:
                    ReadWriteWeaponSkill(reader, whoAmI);
                    break;
                case MessageType.SyncCustomCD:
                    ReadCustomCD(reader, whoAmI);
                    break;
            }
        }
        public static void ReadWriteMouseWorld(BinaryReader reader, int whoAmI)
        {
            // 从数据包中按写入顺序读取数据
            byte playerIndex = reader.ReadByte();
            Vector2 mouseWorld = reader.ReadVector2();
            // 如果是在服务器端收到了这个包
            if (Main.netMode == NetmodeID.Server)
            {
                // 将这个信息转发给所有其他客户端，让他们也知道
                // 创建一个新的包用于广播
                ModPacket broadcastPacket = LAP.Instance.GetPacket();
                broadcastPacket.Write((byte)MessageType.SyncMousePosition);
                broadcastPacket.Write(playerIndex);
                broadcastPacket.WriteVector2(mouseWorld);
                // 发送给所有人 (-1)，除了原始发送者 (whoAmI)
                broadcastPacket.Send(-1, whoAmI);
            }
            // 如果是在客户端收到了服务器转发的包
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                // 在本地更新对应玩家的鼠标位置
                LAPPlayer modPlayer = Main.player[playerIndex].GetModPlayer<LAPPlayer>();
                modPlayer.SyncedMouseWorld = mouseWorld;
            }
        }
        public static void ReadWriteMouseLeft(BinaryReader reader, int whoAmI)
        {
            // 从数据包中按写入顺序读取数据
            byte playerIndex = reader.ReadByte();
            bool mouseLeft = reader.ReadBoolean();
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket broadcastPacket = LAP.Instance.GetPacket();
                broadcastPacket.Write((byte)MessageType.SyncMouseLeft);
                broadcastPacket.Write(playerIndex);
                broadcastPacket.Write(mouseLeft);
                broadcastPacket.Send(-1, whoAmI);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                LAPPlayer modPlayer = Main.player[playerIndex].GetModPlayer<LAPPlayer>();
                modPlayer.MouseLeft = mouseLeft;
            }
        }
        public static void ReadWriteMouseRight(BinaryReader reader, int whoAmI)
        {
            // 从数据包中按写入顺序读取数据
            byte playerIndex = reader.ReadByte();
            bool mouseRight = reader.ReadBoolean();
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket broadcastPacket = LAP.Instance.GetPacket();
                broadcastPacket.Write((byte)MessageType.SyncMouseRight);
                broadcastPacket.Write(playerIndex);
                broadcastPacket.Write(mouseRight);
                broadcastPacket.Send(-1, whoAmI);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                LAPPlayer modPlayer = Main.player[playerIndex].GetModPlayer<LAPPlayer>();
                modPlayer.MouseRight = mouseRight;
            }
        }
        public static void ReadWriteWeaponSkill(BinaryReader reader, int whoAmI)
        {
            // 从数据包中按写入顺序读取数据
            byte playerIndex = reader.ReadByte();
            bool weaponSkill = reader.ReadBoolean();
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket broadcastPacket = LAP.Instance.GetPacket();
                broadcastPacket.Write((byte)MessageType.SyncWeaponSkillKey);
                broadcastPacket.Write(playerIndex);
                broadcastPacket.Write(weaponSkill);
                broadcastPacket.Send(-1, whoAmI);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                LAPPlayer modPlayer = Main.player[playerIndex].GetModPlayer<LAPPlayer>();
                modPlayer.JustPressedWeaponSKill = weaponSkill;
            }
        }
        public static void ReadCustomCD(BinaryReader reader, int whoAmI)
        {
            // 从数据包中按写入顺序读取数据
            byte playerIndex = reader.ReadByte();
            int buffType = reader.Read();
            int buffTime = reader.Read();
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket broadcastPacket = LAP.Instance.GetPacket();
                broadcastPacket.Write((byte)MessageType.SyncCustomCD);
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
