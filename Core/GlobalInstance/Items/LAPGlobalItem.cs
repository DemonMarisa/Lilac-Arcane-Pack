using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Items
{
    public partial class LAPGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public bool UseWeaponSkill = false;
        public bool DrawUCASmallIcon = false;
    }
}
