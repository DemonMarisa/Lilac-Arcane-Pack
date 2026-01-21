using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        public static Vector2 GetMagicStuffRotPoint(this Projectile projectile, Texture2D texture)
        {
            Vector2 rotationPoint = projectile.spriteDirection == 1 ? new Vector2(0, texture.Height) : new Vector2(texture.Width, texture.Height);
            return rotationPoint;
        }

        public static Vector2 GetPlayerToMouseVector2(this Player player)
        {
            Vector2 vector = player.LocalMouseWorld() - player.Center;
            vector = vector.SafeNormalize(Vector2.UnitX);
            return vector;
        }

        public static Vector2 GetToMouseVector2(this Player player, Vector2 BeginPos)
        {
            Vector2 vector = player.LocalMouseWorld() - BeginPos;
            vector = vector.SafeNormalize(Vector2.UnitX);
            return vector;
        }

        public static Vector2 BetterRotatedBy(this Vector2 spinningpoint, double radians, Vector2 center = default, float Xmult = 1f, float Ymult = 1f)
        {
            float num = (float)Math.Cos(radians);
            float num2 = (float)Math.Sin(radians);
            Vector2 vector = spinningpoint - center;
            Vector2 result = center;
            result.X += (vector.X * num - vector.Y * num2) * Xmult;
            result.Y += (vector.X * num2 + vector.Y * num) * Ymult;
            return result;
        }
        public static float GetDamage<T>(this Player player, float baseDamage) where T : DamageClass
        {
            return player.GetTotalDamage<T>().ApplyTo(baseDamage);
        }
        public static int GetIntDamage<T>(this Player player, float baseDamage) where T : DamageClass
        {
            return (int)player.GetDamage<T>(baseDamage);
        }
        public static bool CheckWoodenAmmo(int type, Player player)
        {
            if (player.hasMoltenQuiver && type == ProjectileID.FireArrow)
                return true;
            return type == ProjectileID.WoodenArrowFriendly;
        }
        public static int DamageSoftCap(float dmgInput, int cap)
        {
            if (dmgInput < cap)
            {
                return (int)dmgInput;
            }
            float num = MathF.Pow(dmgInput / (float)cap, 0.5f) / 1.25f + 0.2f;
            return (int)((float)cap * num);
        }
        public static void UpdateWeaponAim(Player player, float rotationOffset = 0f, float rotationSpeed = 1f, bool SetArm = true, bool SetBackHand = false)
        {
            player.ChangeDir(Math.Sign((player.LocalMouseWorld() - player.Center).X));

            Vector2 aimVect = player.LocalMouseWorld() - player.Center;
            aimVect.SafeNormalize(Vector2.UnitX);

            float targetRotation = aimVect.ToRotation();

            if (player.LocalMouseWorld().X < player.Center.X)
                player.itemRotation = player.itemRotation.AngleLerp(targetRotation - MathHelper.ToRadians(rotationOffset) + MathHelper.Pi, rotationSpeed);
            else
                player.itemRotation = player.itemRotation.AngleLerp(targetRotation + MathHelper.ToRadians(rotationOffset), rotationSpeed);
            if (SetArm)
            {
                player.ChangeDir(Math.Sign((player.LocalMouseWorld() - player.Center).X));
                float rotation = (player.Center - player.LocalMouseWorld()).ToRotation() * player.gravDir + MathHelper.PiOver2;
                player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, rotation);
                if (SetBackHand)
                {
                    player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, rotation);
                }
            }
        }
    }
}
