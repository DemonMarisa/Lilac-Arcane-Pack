using LAP.Core.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Content.RecipeGroupAdd
{
    public class LAPRecipeGroup : ModSystem
    {
        public static string PreFix = "LAP:";
        public static RecipeGroup AnyCopperBar;
        public static RecipeGroup AnySilverBar;
        public static RecipeGroup AnyGoldBar;
        public static RecipeGroup AnyEvilBar;
        public static RecipeGroup AnyCobaltBar;
        public static RecipeGroup AnyMythrilBar;
        public static RecipeGroup AnyAdamantiteBar;
        public static RecipeGroup AnyStoneBlock;
        public static RecipeGroup AnyArkhalis;
        public static RecipeGroup AnyDartGun;
        public override void AddRecipeGroups()
        {
            int[] anyCopperBar = [ItemID.CopperBar, ItemID.TinBar];
            LAPUtilities.CreatRecipeGroup(ref AnyCopperBar, PreFix + AnyCopperBar, anyCopperBar);

            int[] anySilverBar = [ItemID.SilverBar, ItemID.TungstenBar];
            LAPUtilities.CreatRecipeGroup(ref AnySilverBar, PreFix + AnySilverBar, anySilverBar);

            int[] anyGoldBar = [ItemID.GoldBar, ItemID.PlatinumBar];
            LAPUtilities.CreatRecipeGroup(ref AnyGoldBar, PreFix + AnyGoldBar, anyGoldBar);

            int[] anyEvilBar = [ItemID.DemoniteBar, ItemID.CrimtaneBar];
            LAPUtilities.CreatRecipeGroup(ref AnyEvilBar, PreFix + AnyEvilBar, anyEvilBar);

            int[] anyCobaltBar = [ItemID.CobaltBar, ItemID.PalladiumBar];
            LAPUtilities.CreatRecipeGroup(ref AnyCobaltBar, PreFix + AnyCobaltBar, anyCobaltBar);

            int[] anyMythrilBar = [ItemID.MythrilBar, ItemID.OrichalcumBar];
            LAPUtilities.CreatRecipeGroup(ref AnyMythrilBar, PreFix + AnyMythrilBar, anyMythrilBar);

            int[] anyAdamantiteBar = [ItemID.AdamantiteBar, ItemID.TitaniumBar];
            LAPUtilities.CreatRecipeGroup(ref AnyAdamantiteBar, PreFix + AnyAdamantiteBar, anyAdamantiteBar);

            int[] anyStoneBlock = [ItemID.StoneBlock, ItemID.EbonstoneBlock, ItemID.CrimstoneBlock, ItemID.PearlstoneBlock,];
            LAPUtilities.CreatRecipeGroup(ref AnyStoneBlock, PreFix + AnyStoneBlock, anyStoneBlock);
            
            int[] anyArkhalis = [ItemID.Arkhalis, ItemID.Terragrim];
            LAPUtilities.CreatRecipeGroup(ref AnyArkhalis, PreFix + AnyArkhalis, anyArkhalis);

            int[] anyDartRifle = [ItemID.DartRifle, ItemID.DartPistol];
            LAPUtilities.CreatRecipeGroup(ref AnyDartGun, PreFix + AnyDartGun, anyDartRifle);
        }
        public override void Unload()
        {
            AnyCopperBar = null;
            AnySilverBar = null;
            AnyGoldBar = null;
            AnyEvilBar = null;
            AnyCobaltBar = null;
            AnyMythrilBar = null;
            AnyAdamantiteBar = null;
            AnyStoneBlock = null;
            AnyArkhalis = null;
            AnyDartGun = null;
        }
    }
}
