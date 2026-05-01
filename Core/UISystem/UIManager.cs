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
        public static List<int> ActiveUI = [];
        public static int MaxDepth = 20;
        public static bool[] ActiveDepth = new bool[MaxDepth];
        public static int[] ActiveDepthCount = new int[MaxDepth];
        public static int BlockAllUI;
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
            for (int i = 0; i < UICollection.Count; i++)
            {
                UICollection[i].PostSetUpContent();
            }
        }
        public static void PostUpdateUI(On_Main.orig_UpdateUIStates orig, GameTime gameTime)
        {
            orig(gameTime);
            LAPInfo.MouseRectangle = new Rectangle((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 4, 4);
            if (ActiveUI.Count != 0)
            {
                for (int i = ActiveUI.Count - 1; i >= 0; i--)
                {
                    LAPContent.GetUI(ActiveUI[i]).Update();
                }
            }
            for (int i = 0; i < ActiveDepth.Length; i++)
            {
                if (ActiveDepthCount[i] == 0)
                    ActiveDepth[i] = false;
                else
                    ActiveDepth[i] = true;
            }
            for (int i = 0; i < ActiveDepthCount.Length; i++)
            {
                if (ActiveDepthCount[i] > 0)
                    ActiveDepthCount[i]--;
            }
            if (BlockAllUI > 0)
                BlockAllUI--;
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (ActiveUI.Count != 0)
            {
                int mouseIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
                if (mouseIndex != -1)
                {
                    layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("LAP Custom UI", delegate ()
                    {
                        LAPUtilities.ReSetToBeginUI(BlendState.NonPremultiplied);
                        // 绘制前，获取所有活跃的 UI 并按 UIDepth 升序排序
                        var sortedActiveUIs = ActiveUI.Select(type => LAPContent.GetUI(type)).OrderBy(ui => ui.UIDepth).ToList();
                        for (int i = 0; i < sortedActiveUIs.Count; i++)
                        {
                            sortedActiveUIs[i].Draw(Main.spriteBatch);
                        }
                        LAPUtilities.ReSetToEndUI();
                        return true;
                    }, InterfaceScaleType.UI));
                }
            }
        }
    }
}
