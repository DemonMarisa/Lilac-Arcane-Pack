using LAP.Core.Keybind;
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
                tooltips.ReplaceManaCost(WeaponSkillRealManaCost);
            }
        }
    }
}
