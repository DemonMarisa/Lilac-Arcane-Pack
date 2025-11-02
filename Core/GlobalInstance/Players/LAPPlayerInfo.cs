using LAP.Core.NetCode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public Vector2 SyncedMouseWorld;
        public bool MouseLeft;
        public bool MouseRight;
        public void UpdateMouseWorld()
        {
            if (Main.myPlayer == Player.whoAmI)
            {
                SyncedMouseWorld = Main.MouseWorld;
                MouseLeft = Main.mouseLeft;
                MouseRight = Main.mouseRight;
            }
            // 只在多人模式的客户端执行
            if (Main.netMode == NetmodeID.MultiplayerClient && Main.myPlayer == Player.whoAmI)
            {
                // 创建一个新的网络数据包
                ModPacket packet = Mod.GetPacket();
                // 写入一个自定义的消息类型，以便HandlePacket能识别
                packet.Write((byte)LAPNetCode.MessageType.SyncMousePosition);
                // 写入是哪个玩家发送的
                packet.Write((byte)Player.whoAmI);
                // 写入鼠标坐标
                packet.WriteVector2(Main.MouseWorld);
                // 写入鼠标左键状态
                packet.Write(Main.mouseLeft);
                // 写入鼠标右键状态
                packet.Write(Main.mouseRight);
                // 发送给服务器
                packet.Send();
            }
        }
    }
}
