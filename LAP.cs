using LAP.Assets.Musics;
using LAP.Core.NetCode;
using System.IO;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class LAP : Mod
    {
        public static LAP Instance;
        public Mod InfernumMode = null;
        public Mod CalamityInheritance = null;
        public Mod UCA = null;
        public Mod EnderLiliesMusicPack = null;
        public Mod bossChecklist = null;
        public Mod WrathoftheGods = null;
        public Mod CalamityHunt = null;
        public Mod CalamityMod = null;
        public Mod CalamityModMusic = null;
        public override void Load()
        {
            Instance = this;

            CalamityMod = null;
            ModLoader.TryGetMod("CalamityMod", out CalamityMod);

            CalamityModMusic = null;
            ModLoader.TryGetMod("CalamityModMusic", out CalamityModMusic);

            InfernumMode = null;
            ModLoader.TryGetMod("InfernumMode", out InfernumMode);

            UCA = null;
            ModLoader.TryGetMod("UCA", out UCA);

            CalamityInheritance = null;
            ModLoader.TryGetMod("CalamityInheritance", out CalamityInheritance);

            bossChecklist = null;
            ModLoader.TryGetMod("BossChecklist", out bossChecklist);

            EnderLiliesMusicPack = null;
            ModLoader.TryGetMod("EnderLiliesMusicPack", out EnderLiliesMusicPack);

            CalamityHunt = null;
            ModLoader.TryGetMod("CalamityHunt", out CalamityHunt);

            WrathoftheGods = null;
            ModLoader.TryGetMod("NoxusBoss", out WrathoftheGods);
        }

        public override void Unload()
        {
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            LAPNetCode.HandleMouseWorldPacket(reader, whoAmI);
        }

    }
}
