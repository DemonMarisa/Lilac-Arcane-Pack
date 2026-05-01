using LAP.Core.NetCode.Content;
using LAP.Core.SystemsLoader;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.NetCode.NetUtilities
{
    public static partial class LAPNetUtils
    {
        public static void SyncedDash(this Player player, int Type)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                return;
            // 只在多人模式的客户端执行
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                // 创建一个新的网络数据包
                ModPacket packet = LAP.Instance.GetPacket();
                // 写入一个自定义的消息类型，以便HandlePacket能识别
                packet.Write(LAPContent.PackHandleType<ReadDash>());
                // 写入是哪个玩家发送的
                packet.Write(player.whoAmI);
                // 写入冲刺类型
                packet.Write(Type);
                // 发送给服务器
                packet.Send();
            }
        }
    }
}
