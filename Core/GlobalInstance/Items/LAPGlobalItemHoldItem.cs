using LAP.Core.SystemsLoader;
using LAP.Core.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Items
{
    public partial class LAPGlobalItem : GlobalItem
    {
        public override void HoldItem(Item item, Player player)
        {
            if (WeaponSkillManaCost >= 0)
            {
                WeaponSkillRealManaCost = Main.LocalPlayer.GetRealManaCost(WeaponSkillManaCost);
            }
            if (WeaponSkillFocusCost >= 0)
            {
                WeaponSkillRealFocusCost = Main.LocalPlayer.GetRealFocusCost(WeaponSkillFocusCost);
            }
        }
    }
}
