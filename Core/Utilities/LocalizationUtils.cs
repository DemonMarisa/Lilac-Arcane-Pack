using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        /*
        public static LocalizedText GetText(this Mod mod, string key)
        {
            return Language.GetOrRegister($"{mod}" + key);
        }
        public static string GetTextValue(this Mod mod, string key)
        {
            return Language.GetTextValue($"{mod}" + key);
        }
        */
        public static string TooltipHotkeyString(this ModKeybind mhk)
        {
            if (Main.dedServ || mhk == null)
            {
                return "";
            }
            List<string> assignedKeys = mhk.GetAssignedKeys();
            if (assignedKeys.Count == 0)
            {
                return "[NONE]";
            }
            StringBuilder stringBuilder = new StringBuilder(16);
            stringBuilder.Append(assignedKeys[0]);
            for (int i = 1; i < assignedKeys.Count; i++)
            {
                stringBuilder.Append(" / ").Append(assignedKeys[i]);
            }
            return stringBuilder.ToString();
        }
        public static void FindAndReplace(this List<TooltipLine> tooltips, string replacedKey, string newKey)
        {
            TooltipLine tooltipLine = tooltips.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Text.Contains(replacedKey));
            if (tooltipLine != null)
            {
                tooltipLine.Text = tooltipLine.Text.Replace(replacedKey, newKey);
            }
        }
        public static void ReplaceManaCost(this List<TooltipLine> tooltips, int Cost)
        {
            if (!Main.dedServ)
            {
                string newKey = Cost.ToString();
                tooltips.FindAndReplace("[LAPManaCost]", newKey);
            }
        }
        public static void IntegrateHotkey(this List<TooltipLine> tooltips, ModKeybind mhk)
        {
            if (!Main.dedServ && mhk != null)
            {
                string newKey = mhk.TooltipHotkeyString();
                tooltips.FindAndReplace("[KEY]", newKey);
            }
        }
    }
}
