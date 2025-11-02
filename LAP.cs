using LAP.Assets.Musics;
using LAP.Core.NetCode;
using System.IO;
using Terraria.ModLoader;

namespace LAP
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class LAP : Mod
    {
        public static LAP Instance;

        public override void Load()
        {
            Instance = this;
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
