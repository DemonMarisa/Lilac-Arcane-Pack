using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace LAP.Core.IDSets
{
    public partial class LAPIDSet : ModSystem
    {
        /// <summary>
        /// 受到保护的弹幕，用于灾坟虫这种东西不会销毁的弹幕
        /// </summary>
        public static HashSet<int> ProtectedProj = [];
        /// <summary>
        /// 手持弹幕的集合
        /// </summary>
        public static HashSet<int> HeldProj = [];
        /// <summary>
        /// 判定为来自武器战技的射弹合集，这个表只打进去始终都是战技弹幕的
        /// </summary>
        public static HashSet<int> WeaponSkillProj = [];
        /// <summary>
        /// 不可被反弹的敌方弹幕，主要用于避免反弹激光弹幕，很多激光弹幕并没有顶点细分，反弹后会变成一大段线段，导致极大的伤害范围和不合理的弹道
        /// </summary>
        public static HashSet<int> CantReflectProj = [];
        public override void Load()
        {
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
            ProtectedProj = null;
            WeaponSkillProj = null;
            UnloadProjFrame();
        }
    }
}
