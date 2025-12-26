using LAP.Content.Configs;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players
{
    public partial class LAPPlayer : ModPlayer
    {
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (LAP.Instance.UnofficialCalamityEnhanced is not null)
                return;
            if (!LAPConfig.Instance.HeadRotFollowMouse)
                return;
            if (Player.sleeping.isSleeping)
                return;
            if (Main.gameMenu)
                return;
            Vector2 headPos = Player.Center + new Vector2(0, -10);
            Vector2 vec = LAPUtilities.GetVector2(headPos, SyncedMouseWorld);
            Player.headRotation = MathHelper.Lerp(Player.headRotation, Utils.ToRotation(new Vector2(MathF.Cos(vec.X), MathF.Sin(vec.Y * Player.direction))), 0.25f);
        }
    }
}
