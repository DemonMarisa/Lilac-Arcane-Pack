using System;
using Terraria;
using Terraria.Localization;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        public static void CreatRecipeGroup(ref RecipeGroup rg, int ShowUpItemID, string name, params int[] AllItem)
        {
            Func<string> creator = () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ShowUpItemID)}";
            rg = new RecipeGroup(creator, AllItem);
            RecipeGroup.RegisterGroup(name, rg);
        }
        public static void CreatRecipeGroup(ref RecipeGroup rg, string name, params int[] AllItem)
        {
            Func<string> creator = () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(AllItem[0])}";
            rg = new RecipeGroup(creator, AllItem);
            RecipeGroup.RegisterGroup(name, rg);
        }
    }
}
