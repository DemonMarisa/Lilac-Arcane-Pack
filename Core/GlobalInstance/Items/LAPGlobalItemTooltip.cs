using LAP.Core.Keybind;
using LAP.Core.SystemsLoader;
using LAP.Core.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Items
{
    public partial class LAPGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            ModifyInflationTooltips(tooltips);
            if (UseWeaponSkill)
            {
                tooltips.IntegrateHotkey(LAPKeybind.WeaponSkillHotKey);
            }

            if (WeaponSkillManaCost >= 0)
            {
                int RealMana = Main.LocalPlayer.GetRealManaCost(WeaponSkillManaCost);
                tooltips.ReplaceManaCost(RealMana);
            }
            if (WeaponSkillFocusCost >= 0)
            {
                int RealFocus = Main.LocalPlayer.GetRealFocusCost(WeaponSkillFocusCost);
                tooltips.ReplaceFocusCost(RealFocus);
            }
        }
    }
}
