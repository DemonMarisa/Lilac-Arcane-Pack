using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public int LifeRegen;
        public int BadLifeRegen;
        public override void UpdateLifeRegen()
        {
            Player.lifeRegen += LifeRegen;
            LifeRegen = 0;
        }
        public override void UpdateBadLifeRegen()
        {
            if (BadLifeRegen != 0)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegen -= BadLifeRegen;
                BadLifeRegen = 0;
            }
        }
    }
}
