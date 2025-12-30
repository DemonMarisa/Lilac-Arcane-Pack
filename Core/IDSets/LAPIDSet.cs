using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using LAP.Core.MiscDate;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.IDSets
{
    public class LAPIDSet : ModSystem
    {
        public static HashSet<int> CantSplitProj = [];
        public static HashSet<int> ProtectedProj = [];
        public static HashSet<int> MushroomWeaponIDs = [ItemID.Hammush,ItemID.MushroomSpear,ItemID.Shroomerang];
        public override void Load()
        {
            foreach (int a in LAPList.rangedProjectileExceptionList)
            {
                CantSplitProj.Add(a);
            }
            foreach (int a in LAPList.projectileDestroyExceptionList)
            {
                ProtectedProj.Add(a);
            }
            // 关键字列表（手持，长矛，钻头，短剑）
            string[] banKeywords = { "Hold", "Held", "Spear", "Drill", "Shortsword" };
            foreach (Mod mod in ModLoader.Mods)
            {
                foreach (ModProjectile modProj in mod.GetContent<ModProjectile>())
                {
                    string typeName = modProj.GetType().Name;
                    bool isBanned = banKeywords.Any(keyword => typeName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
                    if (isBanned)
                    {
                        CantSplitProj.Add(modProj.Type);
                        ProtectedProj.Add(modProj.Type);
                    }
                }
            }
        }
        public override void PostSetupContent()
        {
            if (LAP.Instance.CalamityMod is not null)
            {
                LoadCalamityMushroomItemIDs();
            }
        }

        [JITWhenModsEnabled("CalamityMod")]
        public static void LoadCalamityMushroomItemIDs()
        {
            MushroomWeaponIDs.Add(ItemType<Mycoroot>());
            MushroomWeaponIDs.Add(ItemType<InfestedClawmerang>());
            MushroomWeaponIDs.Add(ItemType<PuffShroom>());
            MushroomWeaponIDs.Add(ItemType<HyphaeRod>());
            MushroomWeaponIDs.Add(ItemType<Fungicide>());
            MushroomWeaponIDs.Add(ItemType<MycelialClaws>());
            MushroomWeaponIDs.Add(ItemType<Shroomer>());
        }
        public override void Unload()
        {
            CantSplitProj = null;
            ProtectedProj = null;
            MushroomWeaponIDs = null;
        }
    }
}
