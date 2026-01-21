using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players.DashSystem
{
    public struct DashDamageInfo(int damage, float knockBack, DamageClass dc)
    {
        public int Damage = damage;
        public float KnockBack = knockBack;
        public DamageClass damageClass = dc;
    }
}
