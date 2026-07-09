using LAP.Core.GlobalInstance.Players;
using LAP.Core.SystemsLoader;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.NetCode.Content
{
    public class ReadWritePlayerMouseState : BaseLAPHandlePack
    {
        public override void Read(BinaryReader reader, int whoAmI)
        {
            // 读取ID
            byte playerId = reader.ReadByte();
            // 读取鼠标按键状态
            BitsByte flags = reader.ReadByte();
            bool isMouseLeft = flags[0];
            bool isMouseRight = flags[1];
            // 读取鼠标坐标
            Vector2 mouseWorld = reader.ReadVector2();
            if (playerId >= 0 && playerId < Main.maxPlayers && Main.player[playerId].active)
            {
                Player player = Main.player[playerId];
                LAPPlayer modPlayer = player.LAP();
                // 将读取到的数据赋值给本地变量
                modPlayer.MouseLeft = isMouseLeft;
                modPlayer.MouseRight = isMouseRight;
                modPlayer.SyncedMouseWorld = mouseWorld;
                // 如果当前接收者是服务器，说明这是某个客户端发来的同步请求
                // 服务器需要把这个包原封不动地转发给其他所有客户端，这样大家才能看到他的状态
                if (Main.netMode == NetmodeID.Server)
                {
                    ModPacket packet = Mod.GetPacket();
                    // 写入包头
                    packet.Write(Type);
                    // 重新按顺序写入刚才读到的数据
                    packet.Write(playerId);
                    packet.Write(flags);
                    packet.WriteVector2(mouseWorld);
                    // 发送给所有客户端 (-1)，但跳过最初发送这个包的客户端 (whoAmI)
                    // 避免发送者收到自己刚刚发出去的包造成死循环或覆盖
                    packet.Send(-1, whoAmI);
                }
            }
        }
    }
}
