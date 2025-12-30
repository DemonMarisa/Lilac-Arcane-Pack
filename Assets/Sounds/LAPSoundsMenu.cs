using Terraria;
using Terraria.Audio;

namespace LAP.Assets.Sounds
{
    public class LAPSoundsMenu
    {
        public static string WeaponsSoundRoute => "LAP/Assets/Sounds/Items/Weapons";
        public static SoundStyle CarnageRightUse => new($"{WeaponsSoundRoute}/Magic/CarnageRightUse") { Volume = 1f, Pitch = Main.rand.NextFloat(0.4f, 0.9f) };
    }
}
