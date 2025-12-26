using LAP.Content.Configs;
using System;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {

        public override void PreUpdate()
        {
            if (LAP.Instance.UnofficialCalamityEnhanced is not null)
                return;
            if (!LAPConfig.Instance.HeadRotFollowMouse)
                return;
            int mouseX = MathF.Sign(SyncedMouseWorld.X - Player.Center.X);
            bool VecXisZero = Player.velocity.X == 0;
            if (VecXisZero && !Player.sleeping.isSleeping && !Player.ItemAnimationActive && Player.ItemTimeIsZero && (!Player.channel || !Player.controlUseTile || !Player.controlUseItem) && !Player.CCed)
            {
                Player.ChangeDir(mouseX);
            }
        }
    }
}
