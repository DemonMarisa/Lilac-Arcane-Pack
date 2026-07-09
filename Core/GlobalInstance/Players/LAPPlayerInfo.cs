using LAP.Core.NetCode.Content;
using LAP.Core.SystemsLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        internal Vector2 oldSyncedMouseWorld;
        public Vector2 SyncedMouseWorld;

        internal bool OldMouseLeft;
        public bool MouseLeft;

        internal bool OldMouseRight;
        public bool MouseRight;

        // public int mouseSyncCooldown;
        public void UpdateNet()
        {
            // 提前返回，减少代码嵌套层级
            if (Main.myPlayer != Player.whoAmI)
                return;
            // 判断状态是否改变
            bool clicksChanged = (Main.mouseLeft != OldMouseLeft) || (Main.mouseRight != OldMouseRight);
            // 超过10像素才算有效移动，避免频繁发送微小的鼠标移动
            bool positionChanged = Vector2.DistanceSquared(Main.MouseWorld, oldSyncedMouseWorld) > 15f;
            // 发送有2帧的冷却时间，避免频繁发送鼠标移动
            // mouseSyncCooldown--;
            // 触发同步条件，按键状态改变，或者 (坐标发生有效移动 且 冷却时间结束)
            if (clicksChanged || positionChanged)
            {
                // 只在多人模式的客户端执行
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    ModPacket packet = Mod.GetPacket();
                    // 写入类型
                    packet.Write(LAPContent.PackHandleType<ReadWritePlayerMouseState>());
                    packet.Write((byte)Player.whoAmI); // 玩家 ID 使用 byte 即可，因为最多 255 人
                    // 使用 Tmodloader 内置的 BitsByte 将多个 bool 压缩进 1 个 byte 中
                    BitsByte flags = new BitsByte();
                    flags[0] = Main.mouseLeft;
                    flags[1] = Main.mouseRight;
                    packet.Write(flags);
                    // 写入坐标
                    packet.WriteVector2(Main.MouseWorld);
                    packet.Send();
                }
                // 重置冷却时间
                // mouseSyncCooldown = 2;
            }
            // 更新本地旧状态
            OldMouseLeft = Main.mouseLeft;
            OldMouseRight = Main.mouseRight;
            oldSyncedMouseWorld = Main.MouseWorld;
            SyncedMouseWorld = Main.MouseWorld;
            MouseLeft = Main.mouseLeft;
            MouseRight = Main.mouseRight;
        }
    }
}
