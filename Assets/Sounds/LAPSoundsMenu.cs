using Terraria;
using Terraria.Audio;

namespace LAP.Assets.Sounds
{
    public static partial class LAPSoundsMenu
    {
        public static string AttackSoundRoute => "LAP/Assets/Sounds/Attacks";
        public static string DSSoundRoute => "LAP/Assets/Sounds/DarkSouls";
        public static string ZionSoundRoute => "LAP/Assets/Sounds/MAGNOLIA";
        public static string WeaponsSoundRoute => "LAP/Assets/Sounds/Items/Weapons";
        public static string EnvironmentsSoundRoute => "LAP/Assets/Sounds/Environments";
        public static SoundStyle CarnageRightUse => new($"{WeaponsSoundRoute}/Magic/CarnageRightUse") { Volume = 1f, Pitch = Main.rand.NextFloat(0.4f, 0.9f) };
        #region 环境音
        public static SoundStyle RainSound => new($"{EnvironmentsSoundRoute}/RainSound") { Volume = 1f, Pitch = Main.rand.NextFloat(-0.2f, 0.2f), MaxInstances = 3 };
        #endregion
    }
}
