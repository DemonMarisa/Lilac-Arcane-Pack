using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            UpdateNet();
            ClampMaxFocus_PostUpdate();
        }
    }
}
