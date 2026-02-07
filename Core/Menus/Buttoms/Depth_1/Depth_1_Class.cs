using LAP.Core.Menus.Buttoms.BaseButtom;
using LAP.Core.Menus.Buttoms.Depth_2;
using LAP.Core.UISystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace LAP.Core.Menus.Buttoms.Depth_1
{
    public class Start : GameMenuButton
    {
        public override string Text => Language.GetTextValue("Mods.LAP.Menus.Start");
        public override Vector2 Center => new Vector2(Main.screenWidth / 2, 600);
        public override int TargetMenuID => MenuID.None;
        public override void OnMouseLeftRelease()
        {
            // 必须没有二级UI打开才可以打开
            if (!UIManager.ActiveDepth[2])
                StartUI.Active = true;
        }
    }
    public class Setting : GameMenuButton
    {
        public override string Text => Language.GetTextValue("Mods.LAP.Menus.Settings");
        public override Vector2 Center => new Vector2(Main.screenWidth / 2, 600) + Vector2.UnitY * 75;
        public override int TargetMenuID => MenuID.Settings;
    }
    public class Quit : GameMenuButton
    {
        public override string Text => Language.GetTextValue("Mods.LAP.Menus.Quit");
        public override Vector2 Center => new Vector2(Main.screenWidth / 2, 600) + Vector2.UnitY * 150;
        public override int TargetMenuID => MenuID.None;
        public override void OnMouseLeftRelease()
        {
            Main.instance.Exit();
        }
    }
}
