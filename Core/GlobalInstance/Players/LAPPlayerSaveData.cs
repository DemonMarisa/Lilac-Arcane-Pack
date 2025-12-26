using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public override void SaveData(TagCompound tag)
        {
            SaveFP(tag);
        }
        public override void LoadData(TagCompound tag)
        {
            ReadFP(tag);
        }
    }
}
