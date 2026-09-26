using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public float MaxLifeMultiplier = 0;
        public int MaxLifeAdditive = 0;
        public int MaxLifeMinus = 0;
        public float MaxLifeMinusMult = 1f;

        public float MaxManaMultiplier = 0;
        public int MaxManaAdditive = 0;
        public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
        {
            health = StatModifier.Default;
            if (MaxLifeMultiplier != 0)
                health += MaxLifeMultiplier;
            if (MaxLifeAdditive != 0)
                health.Base += MaxLifeAdditive;
            MaxLifeMultiplier = 0f;
            MaxLifeAdditive = 0;

            mana = StatModifier.Default;
            if (MaxManaMultiplier != 0)
                mana += MaxManaMultiplier;
            if (MaxManaAdditive != 0)
                mana.Base = MaxManaAdditive;
            MaxManaMultiplier = 0f;
            MaxManaAdditive = 0;
        }
        // 以后改到直接挂载原版Reset最后
        public void MinusMaxLife_ResetEffect()
        {
            if (Main.gameMenu)
                return;
            bool hasChange = false;
            if (MaxLifeMinus != 0)
            {
                Player.statLifeMax2 = Player.statLifeMax2 - MaxLifeMinus;
                hasChange = true;
            }
            if (MaxLifeMinusMult != 1f)
            {
                Player.statLifeMax2 = (int)(Player.statLifeMax2 * MaxLifeMinusMult);
                hasChange = true;
            }
            if (hasChange)
            {
                if (Player.statLifeMax2 <= 1)
                    Player.statLifeMax2 = 1;
            }
            MaxLifeMinus = 0;
            MaxLifeMinusMult = 1f;
        }
        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            healValue = (int)(healValue * healingPotionMult);
        }
    }
}
