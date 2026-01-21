using LAP.Core.Utilities;
using Terraria;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        /// <summary>
        /// 快速设置数据膨胀倍率
        /// </summary>
        /// 传入的物品<param name="item"></param>
        /// 使用的武器等级<param name="AllweaponTier"></param>
        /// 使用的全局乘数<param name="GlobalMult"></param>
        /// 是否使用自定义乘数，自定义乘数会受到全局乘数影响<param name="UseCustomMult"></param>
        public static void SetCalStatInflation(this Item item, int AllweaponTier, float UseCustomMult, float GlobalMult = 1f)
        {
            item.LAP().UseCICalStatInflation = true;
            item.LAP().WeaponTier = AllweaponTier;
            item.LAP().GlobalMult = GlobalMult;
            item.LAP().UseCustomStatInflationMult = true;
            item.LAP().StatInflationMult = UseCustomMult;
        }
        public static void SetCalStatInflation(this Item item, int AllweaponTier)
        {
            item.LAP().UseCICalStatInflation = true;
            item.LAP().WeaponTier = AllweaponTier;
        }
        public static void SetCalStatInflation(this Item item, int AllweaponTier, float GlobalMult)
        {
            item.LAP().UseCICalStatInflation = true;
            item.LAP().WeaponTier = AllweaponTier;
            item.LAP().GlobalMult = GlobalMult;
        }
        public static void SetCustomMult_Int(this Item item, int TargetDamage)
        {
            item.LAP().UseCICalStatInflation = true;
            item.LAP().UseCustomStatInflationMult = true;
            float mult = TargetDamage / (float)item.damage;
            item.LAP().StatInflationMult = mult;
        }
    }
}
