using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.NPCs
{
    public partial class LAPGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        // 面板的DR，这个DR不会每帧重置
        public float TargetDR = 1f;
        // 实际计算的DR，这个DR会每帧重置
        public float StateDR = 1f;
        // 这个值为true时，代表DR不会降低，会用TargetDR计算减伤
        public bool unbreakableDR;
        // 受伤乘算增加
        public float IncomingDamageAddMult = 1f;
        // 受伤乘算增加
        public int IgnoreDefense = 0;
        public override void ResetEffects(NPC npc)
        {
            StateDR = TargetDR;
            IncomingDamageAddMult = 1f;
            IgnoreDefense = 0;
        }
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            ApplyDR(npc, ref modifiers);
            if (IncomingDamageAddMult != 1f)
                modifiers.FinalDamage *= IncomingDamageAddMult;
            if (IgnoreDefense != 0)
                modifiers.Defense.Flat -= IgnoreDefense;
        }
        public void ApplyDR(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (TargetDR != 1f)
            {
                if (unbreakableDR)
                    modifiers.FinalDamage *= TargetDR;
                else
                    modifiers.FinalDamage *= StateDR;
            }
        }
    }
}
