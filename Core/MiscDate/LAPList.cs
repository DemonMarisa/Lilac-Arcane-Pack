using LAP.Common.CalamityModCross;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.MiscDate
{
    public class LAPList : ModSystem
    {
        public static List<int> rangedProjectileExceptionList = [ProjectileID.IchorDart, ProjectileID.RainbowBack, ProjectileID.PhantasmArrow];

        public static List<int> debuffList = [BuffID.Poisoned,BuffID.Darkness, BuffID.Cursed, BuffID.OnFire, BuffID.Bleeding, BuffID.Confused,  BuffID.Slow, BuffID.Weak,
                BuffID.Silenced,  BuffID.BrokenArmor, BuffID.CursedInferno, BuffID.Frostburn, BuffID.Chilled,  BuffID.Frozen,  BuffID.Burning, BuffID.Suffocation,
                BuffID.Ichor,  BuffID.Venom, BuffID.Blackout, BuffID.Electrified,  BuffID.Rabies, BuffID.Webbed, BuffID.Stoned, BuffID.Dazed, BuffID.VortexDebuff,  BuffID.WitheredArmor,
                BuffID.WitheredWeapon,  BuffID.OgreSpit, BuffID.BetsysCurse];

        public static List<int> projectileDestroyExceptionList = [ProjectileID.Phantasm,ProjectileID.VortexBeater,ProjectileID.DD2PhoenixBow, ProjectileID.LastPrism,
                ProjectileID.LastPrismLaser, ProjectileID.LaserMachinegun, ProjectileID.ChargedBlasterCannon, ProjectileID.MedusaHead];

        public override void OnModLoad()
        {
            if (LAP.Instance.CalamityMod is not null)
            {
                ListAdd_Buff_Calamity.LoadCalamityBuffIDs();
            }
        }
        public override void Unload()
        {
            rangedProjectileExceptionList = null;
            debuffList = null;
            projectileDestroyExceptionList = null;
        }
    }
}
