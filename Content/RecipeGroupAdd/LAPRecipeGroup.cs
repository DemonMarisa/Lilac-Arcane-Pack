using LAP.Core.Utilities;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Content.RecipeGroupAdd
{
    public class LAPRecipeGroup : ModSystem
    {
        public static string PreFix = "LAP:";
        public static string AnyCopperBar;
        public static string AnySilverBar;
        public static string AnyGoldBar;
        public static string AnyEvilBar;
        public static string AnyCobaltBar;
        public static string AnyMythrilBar;
        public static string AnyAdamantiteBar;
        public static string AnyStoneBlock;
        public static string AnyArkhalis;
        public static string AnyDartGun;
        public static string AnyCursedFlameIchor;
        public override void AddRecipeGroups()
        {
            int[] anyCopperBar = [ItemID.CopperBar, ItemID.TinBar];
            AnyCopperBar = LAPUtilities.CreatRecipeGroup(PreFix + "AnyCopperBar", anyCopperBar);

            int[] anySilverBar = [ItemID.SilverBar, ItemID.TungstenBar];
            AnySilverBar = LAPUtilities.CreatRecipeGroup(PreFix + "AnySilverBar", anySilverBar);

            int[] anyGoldBar = [ItemID.GoldBar, ItemID.PlatinumBar];
            AnyGoldBar = LAPUtilities.CreatRecipeGroup(PreFix + "AnyGoldBar", anyGoldBar);

            int[] anyEvilBar = [ItemID.DemoniteBar, ItemID.CrimtaneBar];
            AnyEvilBar = LAPUtilities.CreatRecipeGroup(PreFix + "AnyEvilBar", anyEvilBar);

            int[] anyCobaltBar = [ItemID.CobaltBar, ItemID.PalladiumBar];
            AnyCobaltBar = LAPUtilities.CreatRecipeGroup(PreFix + "AnyCobaltBar", anyCobaltBar);

            int[] anyMythrilBar = [ItemID.MythrilBar, ItemID.OrichalcumBar];
            AnyMythrilBar = LAPUtilities.CreatRecipeGroup(PreFix + "AnyMythrilBar", anyMythrilBar);

            int[] anyAdamantiteBar = [ItemID.AdamantiteBar, ItemID.TitaniumBar];
            AnyAdamantiteBar = LAPUtilities.CreatRecipeGroup(PreFix + "AnyAdamantiteBar", anyAdamantiteBar);

            int[] anyStoneBlock = [ItemID.StoneBlock, ItemID.EbonstoneBlock, ItemID.CrimstoneBlock, ItemID.PearlstoneBlock];
            AnyStoneBlock = LAPUtilities.CreatRecipeGroup(PreFix + "AnyStoneBlock", anyStoneBlock);
            
            int[] anyArkhalis = [ItemID.Arkhalis, ItemID.Terragrim];
            AnyArkhalis = LAPUtilities.CreatRecipeGroup(PreFix + "AnyArkhalis", anyArkhalis);

            int[] anyDartRifle = [ItemID.DartRifle, ItemID.DartPistol];
            AnyDartGun = LAPUtilities.CreatRecipeGroup(PreFix + "AnyDartGun", anyDartRifle);

            int[] anyCursedFlameIchor = [ItemID.CursedFlame, ItemID.Ichor];
            AnyCursedFlameIchor = LAPUtilities.CreatRecipeGroup(PreFix + "AnyCursedFlameIchor", anyCursedFlameIchor);
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
            AnyCursedFlameIchor = null;
        }
    }
}
