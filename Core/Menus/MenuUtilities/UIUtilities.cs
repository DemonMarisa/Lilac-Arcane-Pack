using Terraria;
using Terraria.GameContent.UI.States;
using Terraria.ID;

namespace LAP.Core.Menus.MenuUtilities
{
    public static class UIUtilities
    {
        public static void ChangeMenu(int TargetMenuID)
        {
            LAPTitleUpdate.BeginChangeToOtherMenu = true;
            LAPTitleUpdate.TargetMenuID = TargetMenuID;
        }
        public static void OpenWorkshop()
        {
            LAPTitleUpdate.BeginChangeToOtherMenu = true;
            LAPTitleUpdate.TargetMenuID = MenuID.FancyUI;
            LAPTitleUpdate.OnChangeToTargetMenuID.Add(delegate 
            {
                UIWorkshopHub workshopHub = new UIWorkshopHub(null);
                workshopHub.EnterHub();
                Main.MenuUI.SetState(workshopHub);
            });
        }
        public static void OpenAchievements()
        {
            LAPTitleUpdate.BeginChangeToOtherMenu = true;
            LAPTitleUpdate.TargetMenuID = MenuID.FancyUI;
            LAPTitleUpdate.OnChangeToTargetMenuID.Add(delegate
            {
                Main.menuMode = MenuID.FancyUI;
                Main.MenuUI.SetState(Main.AchievementsMenu);
            });
        }
    }
}
