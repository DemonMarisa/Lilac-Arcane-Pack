using CalamityMod.Items.Weapons.Magic;
using Terraria.ModLoader;

namespace LAP.Common.CalamityModCross
{
    public class CalWeaponID : ModSystem
    {
        public static int PhotosynthesisID;// 光和射线
        public static int ValkyrieRayID;// 女武神射线
        public override void OnModLoad()
        {
            if (LAP.Instance.CalamityMod is not null)
            {
                GetCalamityWeaponsID();
            }
        }
        [JITWhenModsEnabled("CalamityMod")]
        public static void GetCalamityWeaponsID()
        {
            PhotosynthesisID = ItemType<Photosynthesis>();
            ValkyrieRayID = ItemType<ValkyrieRay>();
        }
    }
}
