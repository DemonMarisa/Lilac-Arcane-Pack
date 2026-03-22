using LAP.Core.GlobalInstance.Players;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.NetCode.Content
{
    public class ReadWriteMouseRight : BaseLAPHandlePack
    {
        public override void Read(BinaryReader reader, int whoAmI)
        {
            // 从数据包中按写入顺序读取数据
            int playerIndex = reader.ReadInt32();
            bool mouseRight = reader.ReadBoolean();
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket broadcastPacket = LAP.Instance.GetPacket();
                broadcastPacket.Write(Type);
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
    }
}
