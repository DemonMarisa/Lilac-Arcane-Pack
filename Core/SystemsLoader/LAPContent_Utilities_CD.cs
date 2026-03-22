using LAP.Core.GlobalInstance.Players.CDSystem;
using LAP.Core.LAPUI.CustomCD;
using LAP.Core.NetCode.NetUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;

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
        /// 这是直接移除CD的方法，不会触发完成事件，但是有淡入淡出动画
        /// 只有当Complete为true时才会触发完成事件
        /// 这里的方法是当开始淡出动画时，就已经判定CD完成了，所以这里直接开启淡出动画不会调用完成效果
        /// 这样可以保证视觉效果一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="player"></param>
        public static void RemoveCD(this Player player, int Type, bool Syned = true, bool Complete = true)
        {
            if (!player.LAPCD().ActiveCDType.Contains(Type))
                return;
            for (int i = 0; i < player.LAPCD().ActiveCD.Count; i++)
            {
                BaseCD CD = player.LAPCD().ActiveCD[i];
                if (CD.Type == Type)
                {
                    if (!Complete)
                        CD.BeginFadeOut = true;
                    CD.Time = 0;
                    CD.OnRemove(player);
                    break;
                }
            }
            if (Syned)
                player.SyncedKillCD(Type, Complete);
        }
        public static LAPCDPlayer LAPCD(this Player player)
        {
            return player.GetModPlayer<LAPCDPlayer>();
        }
    }
}
