using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LAP.Core.LAPConditions
{
    public class MiscConditions : ModSystem
    {
        internal static bool _firstEnterWorld = false;
        public static bool FirstEnterWorld
        {
            get => _firstEnterWorld;
            set
            {
                if (!value)
                    _firstEnterWorld = false;
                else
                    NPC.SetEventFlagCleared(ref _firstEnterWorld, -1);
            }
        }
        public static void ResetAllFlags()
        {
            FirstEnterWorld = false;
        }
        public override void OnWorldLoad() => ResetAllFlags();
        public override void OnWorldUnload() => ResetAllFlags();
        public override void SaveWorldData(TagCompound tag)
        {
            List<string> downed = new List<string>();
            if (FirstEnterWorld)
                downed.Add("LAPFirstEnterWorld");
            tag["LAPMiscFlags"] = downed;
        }
        public override void LoadWorldData(TagCompound tag)
        {
            IList<string> downed = tag.GetList<string>("LAPMiscFlags");
            FirstEnterWorld = downed.Contains("LAPFirstEnterWorld");
        }
        #region 网络同步
        public override void NetSend(BinaryWriter writer)
        {
            BitsByte net1 = new BitsByte();
            // 一个比特=8个字节，如果有部分字节暂时用不上，这些字节是一定得用各种方法占用掉让其形成一个完整的比特的
            // 不然发送的时候会有点问题
            net1[0] = FirstEnterWorld;
            net1[1] = false;
            net1[2] = false;
            net1[3] = false;
            net1[4] = false;
            net1[5] = false;
            net1[6] = false;
            net1[7] = false;
            writer.Write(net1);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte net1 = reader.ReadByte();
            FirstEnterWorld = net1[0];
            _ = net1[1];
            _ = net1[2];
            _ = net1[3];
            _ = net1[4];
            _ = net1[5];
            _ = net1[6];
            _ = net1[7];
        }
        #endregion
    }
}
