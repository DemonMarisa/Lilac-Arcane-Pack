using LAP.Core.NetCode.Content;
using LAP.Core.SystemsLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.NetCode.NetUtilities
{
    public static partial class LAPNetUtils
    {
        public static void SyncedCD(this Player player, int Type, int Timeleft)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                return;
            // 只在多人模式的客户端执行
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                // 创建一个新的网络数据包
                ModPacket packet = LAP.Instance.GetPacket();
                // 写入一个自定义的消息类型，以便HandlePacket能识别
                packet.Write(LAPContent.PackHandleType<ReadCustomCD>());
                // 写入是哪个玩家发送的
                packet.Write(player.whoAmI);
                // 写入CD类型
                packet.Write(Type);
                // 写入CD持续时间
                packet.Write(Timeleft);
                // 发送给服务器
                packet.Send();
            }
        }
        public static void SyncedKillCD(this Player player, int Type, bool Complete)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                return;
            // 只在多人模式的客户端执行
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                // 创建一个新的网络数据包
                ModPacket packet = LAP.Instance.GetPacket();
                // 写入一个自定义的消息类型，以便HandlePacket能识别
                packet.Write(LAPContent.PackHandleType<ReadKillCustomCD>());
                // 写入是哪个玩家发送的
                packet.Write(player.whoAmI);
                // 写入CD类型
                packet.Write(Type);
                // 写入是否立刻完成
                packet.Write(Complete);
                // 发送给服务器
                packet.Send();
            }
        }
    }
}
