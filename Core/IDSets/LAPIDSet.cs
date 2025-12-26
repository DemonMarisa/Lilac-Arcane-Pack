using LAP.Core.MiscDate;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace LAP.Core.IDSets
{
    public class LAPIDSet : ModSystem
    {
        public static HashSet<int> CantSplitProj = [];
        public override void Load()
        {
            foreach (int a in LAPList.rangedProjectileExceptionList)
            {
                CantSplitProj.Add(a);
            }
            // 关键字列表（手持，长矛，钻头，短剑）
            string[] banKeywords = {"Hold", "Held", "Spear", "Drill", "Shortsword" };
            foreach (Mod mod in ModLoader.Mods)
            {
                foreach (ModProjectile modProj in mod.GetContent<ModProjectile>())
                {
                    string typeName = modProj.GetType().Name;
                    bool isBanned = banKeywords.Any(keyword => typeName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
                    if (isBanned)
                        CantSplitProj.Add(modProj.Type);
                }
            }
        }
        public override void Unload()
        {
            CantSplitProj = null;
        }
    }
}
