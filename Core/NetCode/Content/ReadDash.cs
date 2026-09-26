using LAP.Core.GlobalInstance.Players.DashSystem;
using LAP.Core.SystemsLoader;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.NetCode.Content
{
    // 这一段逻辑：
    // 本地玩家发出冲刺指令后，会广播给全体玩家
    // 其它玩家收到后，会调用立即冲刺的字段，其它玩家如果没有收到冲刺的指令，本地不会进行开始冲刺的判定
    public class ReadDash : BaseLAPHandlePack
    {
        public override void Read(BinaryReader reader, int whoAmI)
        {
            // 从数据包中按写入顺序读取数据
            int playerIndex = reader.ReadInt32();
            int DashType = reader.ReadInt32();
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket broadcastPacket = LAP.Instance.GetPacket();
                broadcastPacket.Write(Type);
                broadcastPacket.Write(playerIndex);
                broadcastPacket.Write(DashType);
                broadcastPacket.Send(-1, whoAmI);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                // 其它玩家收到包并添加后不需要再发送包了
                Player player = Main.player[playerIndex];
                LAPDashPlayer LAPPlayer = player.GetModPlayer<LAPDashPlayer>();
                LAPPlayer.ImmediatelyDashID = DashType;
            }
        }
    }
}
