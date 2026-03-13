using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using LAP.Core.MiscDate;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.IDSets
{
    public partial class LAPIDSet : ModSystem
    {
        /// <summary>
        /// 用于元素箭袋的不可分裂弹幕ID集合
        /// </summary>
        public static HashSet<int> CantSplitProj = [];
        /// <summary>
        /// 受到保护的弹幕，用于灾坟虫这种东西不会销毁的弹幕
        /// </summary>
        public static HashSet<int> ProtectedProj = [];
        /// <summary>
        /// 手持弹幕的集合
        /// </summary>
        public static HashSet<int> HeldProj = [];
        /// <summary>
        /// 蘑菇武器的集合
        /// </summary>
        public static HashSet<int> MushroomWeaponIDs = [ItemID.Hammush,ItemID.MushroomSpear,ItemID.Shroomerang];
        /// <summary>
        /// 判定为来自武器战技的射弹合集
        /// </summary>
        public static HashSet<int> WeaponSkillProj = [];
        public override void Load()
        {
            foreach (int a in LAPList.rangedProjectileExceptionList)
            {
                if (!CantSplitProj.Contains(a))
                    CantSplitProj.Add(a);
            }
            foreach (int a in LAPList.projectileDestroyExceptionList)
            {
                if (!ProtectedProj.Contains(a))
                    ProtectedProj.Add(a);
            }
            // 关键字（手持，长矛，钻头，短剑）
            string[] banKeywords = { "Hold", "Held", "Spear", "Drill", "Shortsword" };
            foreach (Mod mod in ModLoader.Mods)
            {
                foreach (ModProjectile modProj in mod.GetContent<ModProjectile>())
                {
                    string typeName = modProj.GetType().Name;
                    bool isBanned = banKeywords.Any(keyword => typeName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
                    if (isBanned)
                    {
                        if (!CantSplitProj.Contains(modProj.Type))
                            CantSplitProj.Add(modProj.Type);
                        if (!ProtectedProj.Contains(modProj.Type))
                            ProtectedProj.Add(modProj.Type);
                        if (!HeldProj.Contains(modProj.Type))
                            HeldProj.Add(modProj.Type);
                    }
                }
            }
        }
        public override void PostSetupContent()
        {
        }
        public override void Unload()
        {
            CantSplitProj = null;
            ProtectedProj = null;
            MushroomWeaponIDs = null;
            WeaponSkillProj = null;
            UnloadProjFrame();
        }
    }
}
