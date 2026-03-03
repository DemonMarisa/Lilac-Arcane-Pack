using LAP.Core.Menus.Buttoms.BaseButtom;
using LAP.Core.Menus.DrawVideo;
using LAP.Core.Menus.MenuUtilities;
using LAP.Core.UISystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;

namespace LAP.Core.Menus.Buttoms.Depth_2.Buttoms
{
    public class SinglePlayer : GameMenuButton
    {
        public override string Text => Language.GetTextValue("Mods.LAP.Menus.SinglePlayer");
        public override Vector2 Center => new Vector2(Main.screenWidth / 2, Main.screenHeight / 2 - 170);
        public override int TargetMenuID => MenuID.CharacterSelect;
        public override int UIDepth => 2;
        public override bool PreSetDepth() => false;
        public static BaseUI StartUI => UIManager.UICollection[GetInstance<StartUI>().Type];
        public override void PPUpdate()
        {
            Opacity = StartUI.Opacity;
        }
    }
    public class MultiPlayer : GameMenuButton
    {
        public override string Text => Language.GetTextValue("Mods.LAP.Menus.MultiPlayer");
        public override Vector2 Center => new Vector2(Main.screenWidth / 2, Main.screenHeight / 2 - 87);
        public override int TargetMenuID => MenuID.Multiplayer;
        public override int UIDepth => 2;
        public override bool PreSetDepth() => false;
        public static BaseUI StartUI => UIManager.UICollection[GetInstance<StartUI>().Type];
        public override void PPUpdate()
        {
            Opacity = StartUI.Opacity;
        }
    }
    public class Achievements : GameMenuButton
    {
        public override string Text => Language.GetTextValue("Mods.LAP.Menus.Achievements");
        public override Vector2 Center => new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
        public override int TargetMenuID => MenuID.FancyUI;
        public override void OnMouseLeftRelease()
        {
            UIUtilities.OpenAchievements();
        }
        public override int UIDepth => 2;
        public override bool PreSetDepth() => false;
        public static BaseUI StartUI => UIManager.UICollection[GetInstance<StartUI>().Type];
        public override void PPUpdate()
        {
            Opacity = StartUI.Opacity;
        }
    }
    public class WorkShop : GameMenuButton
    {
        public override string Text => Language.GetTextValue("Mods.LAP.Menus.WorkShop");
        public override Vector2 Center => new Vector2(Main.screenWidth / 2, Main.screenHeight / 2 + 87);
        public override int TargetMenuID => MenuID.FancyUI;
        public override void OnMouseLeftRelease()
        {
            UIUtilities.OpenWorkshop();
        }
        public override int UIDepth => 2;
        public override bool PreSetDepth() => false;
        public static BaseUI StartUI => UIManager.UICollection[GetInstance<StartUI>().Type];
        public override void PPUpdate()
        {
            Opacity = StartUI.Opacity;
        }
    }
    public class Credits : GameMenuButton
    {
        public override string Text => Language.GetTextValue("Mods.LAP.Menus.Credits");
        public override Vector2 Center => new Vector2(Main.screenWidth / 2, Main.screenHeight / 2 + 170);
        public override int TargetMenuID => MenuID.None;
        public override int UIDepth => 2;
        public override bool PreSetDepth() => false;
        public override void OnMouseLeftRelease()
        {
            MenuVideoPlay.CanLiliesCreditsC = true;
        }
        public static BaseUI StartUI => UIManager.UICollection[GetInstance<StartUI>().Type];
        public override void PPUpdate()
        {
            Opacity = StartUI.Opacity;
        }
    }
}
