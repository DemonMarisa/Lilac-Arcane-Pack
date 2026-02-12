using LAP.Core.Menus.AllMenuID;
using LAP.Core.Menus.AllTitleBG;
using LAP.Core.Menus.Buttoms.Depth_1;
using LAP.Core.Menus.Buttoms.Depth_2;
using LAP.Core.Menus.Buttoms.Depth_Top.ToWebUI;
using LAP.Core.Menus.DrawVideo;
using LAP.Core.Menus.OverLayer;
using LAP.Core.UISystem;
using MenuMod.Core.Menu.Buttoms.Depth_1;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace LAP.Core.Menus
{
    public class LAPTitleUpdate
    {
        #region 引用
        public static BaseUI Start => UIManager.UICollection[GetInstance<Start>().Type];
        public static BaseUI Setting => UIManager.UICollection[GetInstance<Setting>().Type];
        public static BaseUI Quit => UIManager.UICollection[GetInstance<Quit>().Type];
        public static BaseUI ToWeb => UIManager.UICollection[GetInstance<ToLiliesWeb>().Type];
        public static BaseUI StartUI => UIManager.UICollection[GetInstance<StartUI>().Type];
        public static BaseUI ChangeMenuStyle => UIManager.UICollection[GetInstance<ChangeMenuStyle>().Type];
        public static BaseUI SwitchModMenu => UIManager.UICollection[GetInstance<SwitchModMenu>().Type];
        #endregion
        public static bool BeginChangeToOtherMenu = false;
        public static bool ChangeToLAPTitle = false;
        public static int TargetMenuID = -1;
        public static int LastMenuID = -1;
        public static bool openWorkShop;
        public static bool openAchievements;
        public static List<Action> OnChangeToTargetMenuID = [];
        public static void Update()
        {
            HandleTitleAndFadeInOut();
            UpdateButtons();
            UpdateBG();
            MenuVideoPlay.UpdateVideo();
        }
        #region 更新切入切出主界面的淡出
        public static void HandleTitleAndFadeInOut()
        {
            // 只会覆盖原版主界面的时候展现背景
            // 因为原版会优先切换到主界面，随后拦截主界面并标记已经切换到了自定义UI
            if (Main.menuMode == MenuID.Title)
                ChangeToLAPTitle = true;
            else
                LastMenuID = Main.menuMode;
            // 如果是切换到主界面，先不切换菜单，等淡出完成后再切换到主界面
            if (ChangeToLAPTitle)
            {
                if (Main.menuMode != LastMenuID)
                    Main.menuMode = LastMenuID;
                MenuOverLayer.OverlayBlackOpacity = MathHelper.Lerp(MenuOverLayer.OverlayBlackOpacity, 1f, 0.2f);
                if (MenuOverLayer.OverlayBlackOpacity > 0.98f)
                {
                    ChangeToLAPTitle = false;
                    Main.menuMode = LAPMenuID.LAPTitle;
                }
            }
            else if (BeginChangeToOtherMenu)// 如果切换到其它界面，先不切换菜单，等淡出完成后再切换到目标界面
            {
                MenuOverLayer.OverlayBlackOpacity = MathHelper.Lerp(MenuOverLayer.OverlayBlackOpacity, 1f, 0.2f);
                if (MenuOverLayer.OverlayBlackOpacity > 0.98f)
                {
                    BeginChangeToOtherMenu = false;
                    Main.menuMode = TargetMenuID;
                    TargetMenuID = -1;
                    if (OnChangeToTargetMenuID.Count != 0)
                    {
                        for (int i = 0; i < OnChangeToTargetMenuID.Count; i++)
                        {
                            Action action = OnChangeToTargetMenuID[i];
                            action();
                        }
                        OnChangeToTargetMenuID.Clear();
                    }
                }
            }
            else // 默认会慢慢淡出黑色覆盖层
                MenuOverLayer.OverlayBlackOpacity = MathHelper.Lerp(MenuOverLayer.OverlayBlackOpacity, 0f, 0.2f);
        }
        #endregion
        #region 更新按钮
        public static void UpdateButtons()
        {
            if (Main.menuMode == LAPMenuID.LAPTitle)
            {
                Start.Update();
                Setting.Update();
                Quit.Update();
                StartUI.Update();
                ChangeMenuStyle.Update();
                SwitchModMenu.Update();
            }
            ToWeb.Update();
        }
        #endregion
        #region 更新BG
        public static void UpdateBG()
        {
            if (EnderMenus.TitleBgStyle == BGStyle.LiliesStart)
            {
                LiliesStart.Update();
            }
        }
        #endregion
    }
}
