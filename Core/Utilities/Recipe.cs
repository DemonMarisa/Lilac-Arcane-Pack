using System;
using Terraria;
using Terraria.Localization;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        public static string CreatRecipeGroup(int ShowUpItemID, string name, params int[] AllItem)
        {
            Func<string> creator = () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ShowUpItemID)}";
            RecipeGroup rg = new RecipeGroup(creator, AllItem);
            RecipeGroup.RegisterGroup(name, rg);
            return name;
        }
        public static string CreatRecipeGroup(string name, params int[] AllItem)
        {
            Func<string> creator = () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(AllItem[0])}";
            RecipeGroup rg = new RecipeGroup(creator, AllItem);
            RecipeGroup.RegisterGroup(name, rg);
            return name;
        }
    }
}
