using LAP.Core.NetCode.Content;
using LAP.Core.SystemsLoader;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.NetCode.NetUtilities
{
    public static partial class LAPNetUtils
    {
        public static void SyncedReflectProj(this Projectile proj)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                // 获取一个自定义包
                ModPacket packet = LAP.Instance.GetPacket();
                packet.Write(LAPContent.PackHandleType<ReadReflectProjectile>());
                // 写入目标弹幕的 whoAmI
                packet.Write(proj.whoAmI);
                packet.WriteVector2(proj.velocity);
                packet.WriteVector2(proj.Center);
                packet.Write(proj.rotation);
                packet.Send();
            }
        }
    }
}
