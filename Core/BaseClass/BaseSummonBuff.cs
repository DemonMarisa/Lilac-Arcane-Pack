using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.BaseClass
{
    public abstract class BaseSummonBuff : ModBuff
    {
        public virtual int ProjectileType => -1;
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            //Main.persistentBuff[Type] = true;
        }
        public override bool RightClick(int buffIndex)
        {
            foreach (Projectile proj in Main.ActiveProjectiles)
            {
                if (proj.type == ProjectileType && proj.owner == Main.myPlayer)
                {
                    proj.Kill();
                    Main.player[proj.owner].ClearBuff(buffIndex);
                }
            }
            return true;
        }
    }
}
