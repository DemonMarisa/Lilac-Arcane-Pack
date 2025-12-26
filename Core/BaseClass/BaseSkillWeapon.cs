using LAP.Core.Keybind;
using LAP.Core.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.BaseClass
{
    public abstract class BaseSkillWeapon : ModItem, ILocalizedModType
    {
        public override void HoldItem(Player player)
        {
            UpdateHoldItem(player);
            // 只在本地调用
            if (player.whoAmI != Main.myPlayer)
                return;
            if (LAPKeybind.WeaponSkillHotKey.JustPressed && !Main.blockMouse)
            {
                if (Main.playerInventory)
                {
                    if (Main.hoverItemName != "")
                        return;
                }
                WeaponSkill(player);
            }
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.IntegrateHotkey(LAPKeybind.WeaponSkillHotKey);
        }
        public virtual void WeaponSkill(Player player)
        {

        }
        public virtual void UpdateHoldItem(Player player)
        {

        }
    }
}
