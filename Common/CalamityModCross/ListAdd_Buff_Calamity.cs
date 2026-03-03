using CalamityMod.Buffs;
using CalamityMod.Buffs.Cooldowns;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Buffs.StatDebuffs;
using Terraria.ModLoader;
using static LAP.Core.MiscDate.LAPList;

namespace LAP.Common.CalamityModCross
{
    public class ListAdd_Buff_Calamity
    {        
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
    }
}
