using Humanizer;
using LAP.Core.CrossModSupports;
using LAP.Core.Enums;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Items
{
    public partial class LAPGlobalItem : GlobalItem
    {
        /// <summary>
        /// 武器的等级
        /// </summary>
        public int WeaponTier = -1;
        /// <summary>
        /// 是否启用了遗产的数据膨胀
        /// </summary>
        public bool UseCICalStatInflation = false;
        /// <summary>
        /// 是否使用自定义膨胀倍率
        /// 直接应用你填入的膨胀倍率并使用这个武器的全局膨胀
        /// </summary>
        public bool UseCustomStatInflationMult = false;
        /// <summary>
        /// 自定义的数据膨胀倍率
        /// </summary>
        public float StatInflationMult = 1f;
        /// <summary>
        /// 这个武器的全局膨胀倍率
        /// </summary>
        public float GlobalMult = 1f;
        /// <summary>
        /// 输出增伤信息
        /// </summary>
        public float DmageMult = 1f;
        public float GetCalculatedDamageMult()
        {
            float mult = 1f;
            if (UseCustomStatInflationMult)
            {
                mult = StatInflationMult;
            }
            else
            {
                if (WeaponTier == AllWeaponTier.PostMoonLord) mult = 1.3f;
                else if (WeaponTier == AllWeaponTier.PostProvidence) mult = 2.2f;
                else if (WeaponTier == AllWeaponTier.PostSentinelst) mult = 2.4f;
                else if (WeaponTier == AllWeaponTier.PostPolterghast) mult = 2.4f;
                else if (WeaponTier == AllWeaponTier.PostOldDuke) mult = 2.5f;
                else if (WeaponTier == AllWeaponTier.PostDOG) mult = 3f;
                else if (WeaponTier == AllWeaponTier.PostYharon) mult = 5f;
                else if (WeaponTier == AllWeaponTier.PostExoMech) mult = 7f;
                else if (WeaponTier == AllWeaponTier.PostScal) mult = 8f;
                else if (WeaponTier == AllWeaponTier.DemonShadow) mult = 10f;
            }
            mult *= GlobalMult;
            return mult;
        }
        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if (!CrossModSupport.UseCICalStatInflation || !UseCICalStatInflation)
                return;
            DmageMult = GetCalculatedDamageMult();
            damage *= DmageMult;
        }
        public void ModifyInflationTooltips(List<TooltipLine> tooltips)
        {
            if (!CrossModSupport.UseCICalStatInflation || !UseCICalStatInflation)
                return;
            string t = Language.GetTextValue("Mods.LAP.WeaponBoost.DamageMult");
            t = t.FormatWith(DmageMult.ToString());
            TooltipLine myLine = new TooltipLine(Mod, "LAP_WeaponBoostDamage", t);
            int targetIndex = tooltips.FindIndex(line => line.Name == "PrefixDamage");
            if (targetIndex != -1)
                tooltips.Insert(targetIndex, myLine);
            else
                tooltips.Add(myLine);
        }
    }
}
