using LAP.Core.BaseClass;
using LAP.Core.MiscDate;
using Microsoft.Xna.Framework;
using Terraria;

namespace LAP.Content.Projectiles.LifeStealProj
{
    public class StandardHealProj : BaseHealProj
    {
        public override void ExAI()
        {
            for (int i = 0; i < 2; i++)
            {
                int dustType = Main.rand.NextBool(4) ? LAPDustID.DustVampireKnife : LAPDustID.DustLifeDrain;
                Vector2 dustSpawnPos = Projectile.Center - Projectile.velocity * i / 2f;
                Dust crimtameMagic = Dust.NewDustPerfect(dustSpawnPos, dustType);
                crimtameMagic.scale = Main.rand.NextFloat(0.96f, 1.04f);
                crimtameMagic.noGravity = true;
                crimtameMagic.velocity *= 0.1f;
            }
        }
        // 额外的Kill
        public override void ExKill()
        {

        }
    }
}
