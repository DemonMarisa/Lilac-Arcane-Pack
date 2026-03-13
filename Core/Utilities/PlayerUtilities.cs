using LAP.Content.Particles;
using LAP.Core.GlobalInstance.Players;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Map;
using Terraria.ModLoader;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        public static LAPPlayer LAP(this Player player)
        {
            return player.GetModPlayer<LAPPlayer>();
        }

        public static float ApplyPlayerDefAndDR(this Player player, int Damage, bool ApplyDRRot)
        {
            float InComingDamage = Damage;
            InComingDamage -= player.statDefense;
            if (ApplyDRRot)
            {
                float realDR = player.endurance / 1f + player.endurance;
                InComingDamage *= 1 - realDR;
            }
            else
            {
                InComingDamage *= 1 - player.endurance;
            }
            return InComingDamage;
        }
        public static void NCHeal(this Player player, int amount)
        {
            player.statLife += amount;
            if (Main.myPlayer == player.whoAmI)
                player.HealEffect(amount);
        }
        public static bool HasProj<T>(this Player player) where T : ModProjectile => HasProj(player, ModContent.ProjectileType<T>());
        public static bool HasProj(this Player player, int projID) => player.ownedProjectileCounts[projID] > 0;
        public static int HasProjCount(this Player player, int projID) => player.ownedProjectileCounts[projID];
        public static int HasProjCount<T>(this Player player) where T : ModProjectile => player.ownedProjectileCounts[ProjectileType<T>()];
        /// <summary>
        /// 重载一个out传参，输出你判定的拥有的proj的ID以方便后续可能需要的计算，或者别的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="player"></param>
        /// <param name="ProjID"></param>
        /// <returns></returns>
        public static bool HasProj<T>(this Player player, out int ProjID) where T : ModProjectile
        {
            ProjID = ModContent.ProjectileType<T>();
            return HasProj<T>(player);
        }

        public static int GetRealManaCost(this Player player, int cost)
        {
            return (int)(cost * player.manaCost);
        }
        public static Item ActiveItem(this Player player) => Main.mouseItem.IsAir ? player.HeldItem : Main.mouseItem;
        public static float RemainingMinion(this Player player)
        {
            return player.maxMinions - player.slotsMinions;
        }
        public static int ApplyWeaponAttackSpeed(this Player player, Item item, int time,int Min)
        {
            float a = player.GetWeaponAttackSpeed(item);
            float Mult = 1f / a;
            int RealAttack = (int)(time * Mult);
            if (RealAttack < Min)
                return Min;
            else
                return RealAttack;
        }
        public static void SetArmRot(this Player player, float rot, bool setback = true, bool setfront = true)
        {
            if (setback)
                player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, rot - MathHelper.PiOver2);
            if (setfront)
                player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, rot - MathHelper.PiOver2);
        }
        public static Vector2 ClampedMouseWorld(this Player player, float mult = 1)
        {
            Vector2 mouseWorld = player.LocalMouseWorld();
            mouseWorld.X = ((mouseWorld.X >= player.MountedCenter.X) ? MathF.Min(mouseWorld.X, player.MountedCenter.X + 960f * mult) : MathF.Max(mouseWorld.X, player.MountedCenter.X - 960f * mult));
            mouseWorld.Y = ((mouseWorld.Y >= player.MountedCenter.Y) ? MathF.Min(mouseWorld.Y, player.MountedCenter.Y + 540f * mult) : MathF.Max(mouseWorld.Y, player.MountedCenter.Y - 540f * mult));
            return mouseWorld;
        }
        public static Vector2 GetArmRoot(this Player player)
        {
            return player.MountedCenter + new Vector2(-5 * player.direction, -1);
        }
    }
}
