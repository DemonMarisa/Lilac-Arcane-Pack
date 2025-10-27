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

        public static bool HasProj<T>(this Player player) where T : ModProjectile
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<T>()] > 0;
        }
    }
}
