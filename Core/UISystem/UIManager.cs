using LAP.Core.DebugSystem;
using LAP.Core.MiscDate;
using LAP.Core.SystemsLoader;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace LAP.Core.UISystem
{
    public class UIManager : ModSystem
    {
        // 所有UI的集合，UI的Type为在集合中的索引
        public static List<BaseUI> UICollection = new List<BaseUI>();
        // 活跃UI的集合，启动UI时会向内添加Type作为索引去更新BaseUI实例
        // DeActive时会直接根据Type删除索引
        public static List<BaseUI> ActiveUIs = [];
        public static bool MouseConsumed;
        public static int BlockAllUI;
        public static bool BeginSort;
        public override void Load()
        {
            On_Main.UpdateUIStates += PostUpdateUI;
        }
        public override void Unload()
        {
            On_Main.UpdateUIStates -= PostUpdateUI;
        }
        public override void PostSetupContent()
        {
            foreach (var ui in UICollection)
            {
                ui.PostSetUpContent();
            }
        }
        public static void PostUpdateUI(On_Main.orig_UpdateUIStates orig, GameTime gameTime)
        {
            orig(gameTime);
            // 此处建议使用 Main.mouseX/Y 而不是 MouseScreen，因为在某些缩放或全屏模式下 MouseScreen 可能不准
            LAPInfo.MouseRectangle = new Rectangle(Main.mouseX, Main.mouseY, 1, 1);
            if (BlockAllUI > 0)
                BlockAllUI--;
            MouseConsumed = false;
            if (ActiveUIs.Count > 0)
            {
                // 从后往前遍历（从最高 UIDepth 到最低），实现 UI 遮挡逻辑
                for (int i = ActiveUIs.Count - 1; i >= 0; i--)
                {
                    // 如果上层 UI 已经占用了鼠标，mouseConsumed 会变为 true
                    if (ActiveUIs[i].UpdateUI(MouseConsumed))
                        MouseConsumed = true;
                }
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (ActiveUIs.Count == 0)
                return;
            if (BeginSort)
            {
                SortActiveUIs();
                BeginSort = false;
            }
            // 找到原版鼠标悬停提示层
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer("LAP: Custom UIs",
                    delegate
                    {
                        LAPUtilities.ReSetToBeginUI(BlendState.NonPremultiplied);
                        // 正向遍历绘制（UIDepth 小的在底层，UIDepth 大的在顶层）
                        foreach (var ui in ActiveUIs)
                        {
                            ui.Draw();
                        }
                        LAPUtilities.ReSetToEndUI();
                        return true;
                    },
                    InterfaceScaleType.UI) // 使用 UI 缩放矩阵
                );
            }
        }
        public static void SortActiveUIs()
        {
            // 按照深度升序排列
            ActiveUIs.Sort((a, b) => a.UIDepth.CompareTo(b.UIDepth));
        }
    }
}
