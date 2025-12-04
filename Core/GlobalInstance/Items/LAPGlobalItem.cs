using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Items
{
    public partial class LAPGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        /// <summary>
        /// 此武器使用战技，并且会自动插入一次战技键
        /// </summary>
        public bool UseWeaponSkill = false;
        // 此武器使用自定义战技
        public bool UseCustomWeaponSkill = false;
        // 绘制UCA的小图标
        public bool DrawUCASmallIcon = false;
        /// <summary>
        /// 大于0时会自动插入一次魔力消耗，你需要手动计算这个值
        /// </summary>
        public int WeaponSkillManaCost = -1;
        // 记录当前战技真正消耗的魔力值
        public int WeaponSkillRealManaCost = -1;
    }
}
