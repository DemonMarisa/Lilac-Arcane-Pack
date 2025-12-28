using CalamityMod.Buffs;
using CalamityMod.Buffs.Cooldowns;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Buffs.StatDebuffs;
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
                LoadCalamityBuffIDs();
            }
        }
        #region 添加Debuff列表
        [JITWhenModsEnabled("CalamityMod")]
        public static void LoadCalamityBuffIDs()
        {
            debuffList.Add(BuffType<SulphuricPoisoning>());
            debuffList.Add(BuffType<Shadowflame>());
            debuffList.Add(BuffType<BrimstoneFlames>());
            debuffList.Add(BuffType<BurningBlood>());
            debuffList.Add(BuffType<BrainRot>());
            debuffList.Add(BuffType<ElementalMix>());
            debuffList.Add(BuffType<CosmicFreeze>());
            debuffList.Add(BuffType<GodSlayerInferno>());
            debuffList.Add(BuffType<AstralInfectionDebuff>());
            debuffList.Add(BuffType<HolyFlames>());
            debuffList.Add(BuffType<Irradiated>());
            debuffList.Add(BuffType<Plague>());
            debuffList.Add(BuffType<CrushDepth>());
            debuffList.Add(BuffType<RiptideDebuff>());
            debuffList.Add(BuffType<MarkedforDeath>());
            debuffList.Add(BuffType<AbsorberAffliction>());
            debuffList.Add(BuffType<ArmorCrunch>());
            debuffList.Add(BuffType<Crumbling>());
            debuffList.Add(BuffType<Vaporfied>());
            debuffList.Add(BuffType<Eutrophication>());
            debuffList.Add(BuffType<Dragonfire>());
            debuffList.Add(BuffType<Nightwither>());
            debuffList.Add(BuffType<MiracleBlight>());
            debuffList.Add(BuffType<WhisperingDeath>());
            debuffList.Add(BuffType<FrozenLungs>());
            debuffList.Add(BuffType<FishAlert>());
            debuffList.Add(BuffType<HolyInferno>());
            debuffList.Add(BuffType<IcarusFolly>());
            debuffList.Add(BuffType<DoGExtremeGravity>());
            debuffList.Add(BuffType<PopoNoselessBuff>());
            debuffList.Add(BuffType<SearingLava>());
            debuffList.Add(BuffType<WeakBrimstoneFlames>());
            debuffList.Add(BuffType<Withered>());
            debuffList.Add(BuffType<NOU>());
        }
        #endregion
        public override void Unload()
        {
            rangedProjectileExceptionList = null;
            debuffList = null;
            projectileDestroyExceptionList = null;
        }
    }
}
