using LAP.Core.AnimationHandle;
using LAP.Core.GlobalInstance.Items;
using LAP.Core.GlobalInstance.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        public static LAPGlobalProj LAP(this Projectile proj)
        {
            return proj.GetGlobalProjectile<LAPGlobalProj>();
        }

        public static LAPGlobalItem LAP(this Item item)
        {
            return item.GetGlobalItem<LAPGlobalItem>();
        }

        public static bool PressLeftAndRightClick()
        {
            return Main.mouseLeft && Main.mouseRight;
        }
        public static bool JustPressLeftClick()
        {
            return Main.mouseLeft && !Main.mouseRight;
        }

        public static bool JustPressRightClick()
        {
            return !Main.mouseLeft && Main.mouseRight;
        }
        public static bool PressLeftAndRightClick(this Player player)
        {
            return player.LAP().MouseLeft && player.LAP().MouseRight;
        }
        public static bool JustPressLeftClick(this Player player)
        {
            return player.LAP().MouseLeft && !player.LAP().MouseRight;
        }

        public static bool JustPressRightClick(this Player player)
        {
            return !player.LAP().MouseLeft && player.LAP().MouseRight;
        }

        public static Vector2 LocalMouseWorld(this Player player)
        {
            return player.LAP().SyncedMouseWorld;
        }
    }
}
