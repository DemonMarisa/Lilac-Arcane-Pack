using LAP.Core.NetCode.Content;
using LAP.Core.SystemsLoader;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.NetCode.NetUtilities
{
    public static partial class LAPNetUtils
    {
        public static void SyncedRFocus(this Player player, int amount)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                return;
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = LAP.Instance.GetPacket();
                packet.Write(LAPContent.PackHandleType<ReadCustomCD>());
                packet.Write(player.whoAmI);
                packet.Write(amount);
                packet.Send();
            }
        }
    }
}
