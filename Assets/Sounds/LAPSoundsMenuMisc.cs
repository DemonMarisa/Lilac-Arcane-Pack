using Terraria;
using Terraria.Audio;

namespace LAP.Assets.Sounds
{
    public static partial class LAPSoundsMenu
    {
        public static SoundStyle SPSwing => new($"{ZionSoundRoute}/SPSwing") { Volume = 1f, Pitch = Main.rand.NextFloat(-0.2f, 0.2f) };
        public static SoundStyle SwingAttack => new($"{ZionSoundRoute}/SwingAttack") { Volume = 1f, Pitch = Main.rand.NextFloat(-0.2f, 0.2f) };

        public static SoundStyle StormRulerAttack => new($"{DSSoundRoute}/StormRulerAttack") { Volume = 1f, Pitch = Main.rand.NextFloat(-0.2f, 0.2f) };
        public static SoundStyle StormRulerCharge => new($"{DSSoundRoute}/StormRulerCharge") { Volume = 1f, Pitch = Main.rand.NextFloat(-0.2f, 0.2f) };
        public static SoundStyle WeaponSkillSound => new($"{DSSoundRoute}/WeaponSkillSound") { Volume = 1f, Pitch = Main.rand.NextFloat(-0.2f, 0.2f) };
        public static SoundStyle WindAttack1 => new($"{AttackSoundRoute}/WindAttack1") { Volume = 1f, Pitch = Main.rand.NextFloat(-0.2f, 0.2f), MaxInstances = -1 };
        public static SoundStyle WindAttack2 => new($"{AttackSoundRoute}/WindAttack2") { Volume = 1f, Pitch = Main.rand.NextFloat(-0.2f, 0.2f), MaxInstances = -1 };
    }
}
