using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public bool NameIsMAGNOLIA = false;
        public override void Load()
        {
        }
        public override void Unload()
        {
            NameIsMAGNOLIA = false;
        }
        public override void OnEnterWorld()
        {
            if (Player.name == "MAGNOLIA" || Player.name == "Magnolia" || Player.name == "Lilac" || 
                Player.name == "Nola" || Player.name == "Lilia" || Player.name == "莉莉娅" ||
                Player.name == "莱拉克" || Player.name == "诺拉" || Player.name == "马格诺利亚")
            {
                NameIsMAGNOLIA = true;
            }
        }
    }
}
