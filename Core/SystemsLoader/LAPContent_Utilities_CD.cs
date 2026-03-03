using LAP.Core.GlobalInstance.Players.CDSystem;
using LAP.Core.LAPUI.CustomCD;
using LAP.Core.NetCode;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        public static bool HasCD<T>(this Player player) where T : BaseCD
        {
            int Type = CDType<T>();
            return player.LAPCD().ActiveCDType.Contains(Type);
        }
        public static BaseCD AddCD(this Player player, int Type, int Timeleft, bool Syned = true)
        {
            BaseCD cd = CustomCDManger.CDCollection[Type];
            cd.OnSpawn(player);
            if (cd.BeginSound is not null)
                SoundEngine.PlaySound(cd.BeginSound);
            for (int i = 0; i < player.LAPCD().ActiveCD.Count; i++)
            {
                if (Type == player.LAPCD().ActiveCD[i].Type)
                {
                    player.LAPCD().ActiveCD[i].MaxTime = Timeleft;
                    player.LAPCD().ActiveCD[i].Time = Timeleft;
                    player.LAPCD().ActiveCD[i].BeginFadeOut = false;
                    if (Syned)
                        player.SyncedCD(Type, Timeleft);
                    return cd;
                }
            }
            Vector2 beginPos = new Vector2(56, CustomCDManger.AllCDY);// 第一个CD的位置
            cd.DrawPosition = beginPos;
            cd.BeginFadeOut = false;
            cd.MaxTime = Timeleft;
            cd.Time = Timeleft;
            player.LAPCD().ActiveCD.Insert(0, cd);
            if (Syned)
                player.SyncedCD(Type, Timeleft);
            return cd;
        }
        /// <summary>
        /// 立刻完成CD的方法，会触发完成事件
        /// </summary>
        /// <param name="player"></param>
        public static bool ImmediateComplete<T>(this Player player) where T : BaseCD
        {
            int Type = CDType<T>();
            for (int i = 0; i < player.LAPCD().ActiveCD.Count; i++)
            {
                if (player.LAPCD().ActiveCD[i].Type == Type)
                {
                    player.LAPCD().ActiveCD[i].Time = 0;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 这是直接移除CD的方法，不会触发完成事件，但是有淡入淡出动画
        /// 这里的方法是当开始淡出动画时，就已经判定CD完成了，所以这里直接开启淡出动画不会调用完成效果
        /// 这样可以保证视觉效果一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="player"></param>
        public static bool RemoveCD<T>(this Player player) where T : BaseCD
        {
            int Type = CDType<T>();
            for (int i = 0; i < player.LAPCD().ActiveCD.Count; i++)
            {
                if (player.LAPCD().ActiveCD[i].Type == Type)
                {
                    player.LAPCD().ActiveCD[i].BeginFadeOut = true;
                    player.LAPCD().ActiveCD[i].Time = 0;
                    return true;
                }
            }
            return false;
        }
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
                packet.Write((int)LAPNetCode.MessageType.SyncCustomCD);
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
        public static LAPCDPlayer LAPCD(this Player player)
        {
            return player.GetModPlayer<LAPCDPlayer>();
        }
    }
}
