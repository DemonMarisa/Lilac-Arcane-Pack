using CalamityMod.Items.Materials;
using Terraria.ModLoader;

namespace LAP.Common.CalamityModCross
{
    public class CalMaterialsID : ModSystem
    {
        public static int ArmoredShellID;// 装甲外壳
        public static int RuinousSoulID;// 毁灭之灵
        public static int LifeAlloyID;// 生命合金
        public static int LivingShardID;// 生命碎片
        public static int AuricBarID;// 金源

        public static int CosmiliteBarID;// 星宇锭
        public override void OnModLoad()
        {
            if (LAP.Instance.CalamityMod is not null)
            {
                GetCalamityMaterialsID();
            }
        }
        [JITWhenModsEnabled("CalamityMod")]
        public static void GetCalamityMaterialsID()
        {
            ArmoredShellID = ItemType<ArmoredShell>();
            RuinousSoulID = ItemType<RuinousSoul>();
            LifeAlloyID = ItemType<LifeAlloy>();
            LivingShardID = ItemType<LivingShard>();
            AuricBarID = ItemType<AuricBar>();

            CosmiliteBarID = ItemType<CosmiliteBar>();
        }
    }
}
