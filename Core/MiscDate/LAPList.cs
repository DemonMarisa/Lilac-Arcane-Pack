using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.MiscDate
{
    public class LAPList : ModSystem
    {
        public static int[] rangedProjectileExceptionList = [ProjectileID.IchorDart, ProjectileID.RainbowBack, ProjectileID.PhantasmArrow];
        public override void Load()
        {
        }

        public override void Unload()
        {
            rangedProjectileExceptionList = null;
        }
    }
}
