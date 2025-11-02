using LAP.Core.CrossModSupports;
using LAP.Core.Enums;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Items
{
    public partial class LAPGlobalItem : GlobalItem
    {
        public int WeaponTier = -1;
        public bool UseCICalStatInflation = false;
        public bool UseCustomStatInflationMult = false;
        public float StatInflationMult = 1f;
        public float GlobalMult = 1f;
        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if (!CrossModSupport.UseCICalStatInflation)
                return;

            if (!UseCICalStatInflation)
                return;

            if (UseCustomStatInflationMult)
            {
                damage *= StatInflationMult;
            }
            else
            {
                if (WeaponTier == AllWeaponTier.PostMoonLord)
                    damage *= 1.3f;

                if (WeaponTier == AllWeaponTier.PostProvidence)
                    damage *= 2.2f;

                if (WeaponTier == AllWeaponTier.PostSentinelst)
                    damage *= 2.4f;

                if (WeaponTier == AllWeaponTier.PostPolterghast)
                    damage *= 2.4f;

                if (WeaponTier == AllWeaponTier.PostOldDuke)
                    damage *= 2.5f;

                if (WeaponTier == AllWeaponTier.PostDOG)
                    damage *= 3f;

                if (WeaponTier == AllWeaponTier.PostYharon)
                    damage *= 5f;

                if (WeaponTier == AllWeaponTier.PostExoMech)
                    damage *= 7f;

                if (WeaponTier == AllWeaponTier.PostScal)
                    damage *= 8f;

                if (WeaponTier == AllWeaponTier.DemonShadow)
                    damage *= 10f;
            }
            damage *= GlobalMult;
        }
    }
}
