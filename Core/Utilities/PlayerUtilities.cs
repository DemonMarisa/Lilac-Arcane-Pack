using LAP.Core.GlobalInstance.Players;
using Terraria;
using Terraria.Map;
using Terraria.ModLoader;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        public static LAPPlayer LAP(this Player player)
        {
            return player.GetModPlayer<LAPPlayer>();
        }

        public static float ApplyPlayerDefAndDR(this Player player, int Damage, bool ApplyDRRot)
        {
            float InComingDamage = Damage;
            InComingDamage -= player.statDefense;
            if (ApplyDRRot)
            {
                float realDR = player.endurance / 1f + player.endurance;
                InComingDamage *= 1 - realDR;
            }
            else
            {
                InComingDamage *= 1 - player.endurance;
            }
            return InComingDamage;
        }

        public static bool HasProj<T>(this Player player) where T : ModProjectile => HasProj(player, ModContent.ProjectileType<T>());
        public static bool HasProj(this Player player, int projID) => player.ownedProjectileCounts[projID] > 0;
        /// <summary>
        /// 重载一个out传参，输出你判定的拥有的proj的ID以方便后续可能需要的计算，或者别的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="player"></param>
        /// <param name="ProjID"></param>
        /// <returns></returns>
        public static bool HasProj<T>(this Player player, out int ProjID) where T : ModProjectile
        {
            ProjID = ModContent.ProjectileType<T>();
            return HasProj<T>(player);
        }

        public static int GetRealManaCost(this Player player, int cost)
        {
            return (int)(cost * player.manaCost);
        }
    }
}
