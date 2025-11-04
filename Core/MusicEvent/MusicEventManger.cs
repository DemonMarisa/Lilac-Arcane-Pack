using LAP.Assets.Musics;
using LAP.Content.Configs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.MusicEvent
{
    // 此处音乐事件为手动在外部添加，而不是采用和灾厄一样的提前生成并遍历条件
    public record class MusicEventEntry(string Song, TimeSpan Length, TimeSpan OutroSilence, Func<bool> ShouldPlay);
    public class MusicEventManger : ModSystem
    {
        // 待播放列表
        public static List<MusicEventEntry> PlayList = [];
        // 进入世界时的初始化
        public bool CanInitializeEvent = true;
        public static MusicEventEntry CurrentEvent = null;
        /// <summary>
        /// 当前事件的倒计时器。
        /// </summary>
        public static TimeSpan CurrentTrackTimer = TimeSpan.Zero;
        /// <summary>
        /// 标记我们是处于播放阶段 (true) 还是静音淡出阶段 (false)。
        /// </summary>
        public static bool IsInPlayback = true;
        /// <summary>
        /// 存储上一次更新的现实世界时间，用于计算时间差。
        /// </summary>
        public static DateTime lastUpdateTime;
        public static void AddMusicEventEntry(string musicPath, TimeSpan length, Func<bool> shouldPlay, TimeSpan outroSilence)
        {
            if (Main.dedServ)
                return;
            // 如果待播的音乐事件超过64个不会添加
            if (PlayList.Count > 64)
                return;
            if (CurrentEvent is not null)
            {
                if (CurrentEvent.Song == musicPath)
                    return;
            }
            // 添加时候不能添加相同的BGM，防止因为条件问题出问题
            foreach (MusicEventEntry musicEventEntry in PlayList)
            {
                if (musicEventEntry.Song == musicPath)
                    return;
            }
            MusicEventEntry entry = new(musicPath, length, outroSilence, shouldPlay);
            PlayList.Add(entry);
        }
        public override void OnModLoad()
        {
        }
        public override void Unload()
        {
            PlayList.Clear();
            CurrentEvent = null;
        }
        public override void UpdateUI(GameTime gameTime)
        {
            // 服务器上不处理BGM
            if (Main.dedServ)
                return;

            if (CurrentEvent is null)
            {
                for (int i = 0; i < PlayList.Count; i++)
                {
                    if (PlayList[i].ShouldPlay())
                    {
                        CurrentEvent = PlayList[i];
                        PlayList.Remove(PlayList[i]);
                    }
                    else
                    {
                        PlayList.Remove(PlayList[i]);
                    }
                }
                CanInitializeEvent = true;
            }
            else
            {
                int MusicID = MusicLoader.GetMusicSlot(CurrentEvent.Song);
                // 如果当前事件的播放条件突然变为false，立即停止它。
                if (!CurrentEvent.ShouldPlay())
                {
                    Main.musicFade[MusicID] = 0f; // 立即淡出
                    CurrentEvent = null;
                    CurrentTrackTimer = TimeSpan.Zero;
                    CanInitializeEvent = false;
                    return;
                }
                HandleEvent(MusicID);
            }
        }
        public void HandleEvent(int Song)
        {
            if (CanInitializeEvent)
            {
                // 设置播放阶段的倒计时
                CurrentTrackTimer = CurrentEvent.Length;
                IsInPlayback = true; // 进入播放阶段
                // 立即开始播放音乐
                Main.musicBox2 = Song;
                Main.musicFade[Song] = 1f;
                CanInitializeEvent = false;
                lastUpdateTime = DateTime.Now;
                return;
            }
            HandleTimer();
            // 检查计时器状态
            if (IsInPlayback)
            {
                // 处于歌曲播放阶段
                if (CurrentTrackTimer <= TimeSpan.Zero)
                {
                    // 播放阶段结束，进入静音淡出阶段
                    IsInPlayback = false;
                    CurrentTrackTimer = CurrentEvent.OutroSilence; // 重置计时器为静音时长
                    Main.musicFade[Song] = 0f;
                    // 更新上一帧的时间
                    lastUpdateTime = DateTime.Now;
                }
                else
                    Main.musicBox2 = Song;
            }
            else
            {
                // --- 处于静音淡出阶段 ---
                if (CurrentTrackTimer <= TimeSpan.Zero)
                {
                    // 静音阶段结束，彻底完成此事件
                    CurrentEvent = null;
                    CurrentTrackTimer = TimeSpan.Zero;
                }
                else
                {
                    int silence = MusicLoader.GetMusicSlot(Mod, MusicRegister.SliencePath);
                    Main.musicBox2 = silence;
                    Main.musicFade[silence] = 1f;
                }
            }
        }
        // 更新UI会无论游戏是否活跃都会调用，但是只有不活跃的时候，也就是不播放BGM的时候减去进度
        public static void HandleTimer()
        {
            DateTime now = DateTime.Now;
            TimeSpan deltaTime = now - lastUpdateTime;
            // 从倒计时器中减去现实经过的时间
            if (Main.instance.IsActive)
            {
                CurrentTrackTimer -= deltaTime;
            }
            lastUpdateTime = now; // 储存旧时间
        }
    }
}
