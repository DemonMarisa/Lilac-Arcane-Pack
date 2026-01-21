using Terraria.ModLoader;

namespace LAP.Core.Keybind
{
    public class LAPKeybind : ModSystem
    {
        public static ModKeybind WeaponSkillHotKey { get; private set; }
        public static ModKeybind DashHotKey { get; private set; }
        public override void Load()
        {
            // Register keybinds            
            WeaponSkillHotKey = KeybindLoader.RegisterKeybind(Mod, "WeaponSkill", "LeftAlt");
            DashHotKey = KeybindLoader.RegisterKeybind(Mod, "DashHotKey", "LeftShift");
        }

        public override void Unload()
        {
            WeaponSkillHotKey = null;
            DashHotKey = null;
        }
    }
}
