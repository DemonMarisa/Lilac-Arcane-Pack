using LAP.Core.Menus.AllMenuID;
using LAP.Core.Menus.Buttoms.BaseButtom;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace LAP.Core.Menus.Buttoms.Depth_2.MenuBGStyles
{
    public class LiliesStartUI : GameMenuButton
    {
        public override string Text => Language.GetTextValue("Mods.LAP.Menus.LiliesStartUI");
        public override Vector2 Center => new Vector2(Main.screenWidth / 2, Main.screenHeight / 2 - 30);
        public override int TargetMenuID => MenuID.None;
        public override void OnMouseLeftRelease()
        {
            EnderMenus.TitleBgStyle = BGStyle.LiliesStart;
        }
        public override int UIDepth => 2;
        public override bool PreSetDepth() => false;
    }
    public class LiliesTrueEndUI : GameMenuButton
    {
        public override string Text => Language.GetTextValue("Mods.LAP.Menus.LiliesTrueEndUI");
        public override Vector2 Center => new Vector2(Main.screenWidth / 2, Main.screenHeight / 2 + 45);
        public override int TargetMenuID => MenuID.None;
        public override void OnMouseLeftRelease()
        {
            EnderMenus.TitleBgStyle = BGStyle.LiliesEnd;
        }
        public override int UIDepth => 2;
        public override bool PreSetDepth() => false;
    }
}
