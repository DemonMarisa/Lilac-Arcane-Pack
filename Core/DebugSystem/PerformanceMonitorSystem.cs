using LAP.Content.Configs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace LAP.Core.DebugSystem
{
    // 用于存储每个监控点的统计数据
    public class MetricData
    {
        public double Current;
        public double Max;
        public double Average;
        private const double Alpha = 0.1;
        public void Update(double newValue)
        {
            Current = newValue;
            if (newValue > Max) Max = newValue;
            // 指数加权移动平均
            Average = (Average == 0) ? newValue : (newValue * Alpha) + (Average * (1 - Alpha));
        }
    }
    public class PerformanceMonitorSystem : ModSystem
    {
        // 存储不同监控项的耗时
        public static Dictionary<string, MetricData> FrameMetrics = new Dictionary<string, MetricData>();
        // 内部使用的计时器集合
        private static Dictionary<string, Stopwatch> _stopwatches = new Dictionary<string, Stopwatch>();
        // 开始计时
        public static void StartTimer(string key)
        {
            if (!LAPConfig.Instance.DeBugInfo)
                return;
            if (!_stopwatches.ContainsKey(key))
                _stopwatches[key] = new Stopwatch();
            _stopwatches[key].Restart();
        }
        // 停止计时并记录数据
        public static void StopTimer(string key)
        {
            if (!LAPConfig.Instance.DeBugInfo)
                return;
            if (_stopwatches.TryGetValue(key, out Stopwatch sw))
            {
                sw.Stop();
                if (!FrameMetrics.ContainsKey(key))
                    FrameMetrics[key] = new MetricData();
                FrameMetrics[key].Update(sw.Elapsed.TotalMilliseconds);
            }
        }
        public static string GetGCMetrics()
        {
            // 获取当前托管堆的内存使用量
            long mem = System.GC.GetTotalMemory(false) / 1024 / 1024;
            // 获取自程序启动以来的 GC 发生次数
            int gen0 = System.GC.CollectionCount(0);
            return $"Heap: {mem}MB | GC Gen0 Count: {gen0}";
        }
        public static void DrawPerformanceMetrics()
        {
            var font = FontAssets.MouseText.Value;
            float yOffset = 14;
            string GCText = "GC状态 : " + GetGCMetrics();
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, GCText, new Vector2(10, 80), Color.White, 0f, Vector2.Zero, Vector2.One);

            if (FrameMetrics.Count == 0)
            {
                string noMetricsText = "No performance metrics recorded.";
                var size = ChatManager.GetStringSize(font, noMetricsText, Vector2.One);
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, noMetricsText, new Vector2(10, 100), Color.White, 0f, Vector2.Zero, Vector2.One);
                return;
            }
            foreach (var metric in FrameMetrics)
            {
                if (Main.LocalPlayer.miscCounter % 60 == 0)
                    metric.Value.Max = 0;
                string text = $"{metric.Key}: {metric.Value.Current:F2} ms" + $"  {metric.Value.Average:F2} ms/s" + $"  {metric.Value.Max:F2} ms_Max/s";
                var size = ChatManager.GetStringSize(font, text, Vector2.One);
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, text, new Vector2(10, 100 + yOffset), Color.White, 0f, Vector2.Zero, Vector2.One);
                yOffset += size.Y + 5; // 增加间距
            }
        }
    }
}
