using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.NetCode.Content
{
    public class ReadReflectProjectile : BaseLAPHandlePack
    {
        public override void Read(BinaryReader reader, int whoAmI)
        {
            // 从数据包中按写入顺序读取数据
            int projIndex = reader.ReadInt32();
            Vector2 velocity = reader.ReadVector2();
            Vector2 center = reader.ReadVector2();
            float rotation = reader.ReadSingle();
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket broadcastPacket = LAP.Instance.GetPacket();
                broadcastPacket.Write(Type);
                broadcastPacket.Write(projIndex);
                broadcastPacket.WriteVector2(velocity);
                broadcastPacket.WriteVector2(center);
                broadcastPacket.Write(rotation);
                broadcastPacket.Send(-1, whoAmI);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                // 其它玩家收到包并添加后不需要再发送包了
                Projectile proj = Main.projectile[projIndex];
                proj.velocity = velocity;
                proj.Center = center;
                proj.rotation = rotation;
            }
        }
    }
}
