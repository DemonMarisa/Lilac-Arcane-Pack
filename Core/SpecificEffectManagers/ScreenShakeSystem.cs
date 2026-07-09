using LAP.Content.Configs;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.SpecificEffectManagers
{
    public static class ScreenShakeType
    {
        public const int Random = 0;
        public const int PunchSin = 1;
        public const int PunchCos = 2;
    }


    public class ScreenShakeInfo(Vector2 ShakePosition, float ShakeStrength, int ShakeTime, float ShakeDirection, float ShakeAngleOffset, bool useDistanceFade,
        int ShakeEffectDistance, int type = ScreenShakeType.Random, float vibrations = 3f)
    {
        public int Type = type;
        /// <summary>
        /// 是否使用震动衰减
        /// </summary>
        public bool UseDistanceFade = useDistanceFade;
        /// <summary>
        /// 震动存在时间
        /// </summary>
        public int ShakeTime = 0;
        /// <summary>
        /// 震动强度
        /// </summary>
        public int ShakeLifeTime = ShakeTime;
        /// <summary>
        /// 震动影响的距离
        /// </summary>
        public int ShakeEffectDistance = ShakeEffectDistance;
        /// 震动强度
        /// </summary>
        public float ShakeStrength = ShakeStrength;
        /// <summary>
        /// 震动方向的随机范围
        /// </summary>
        public float ShakeAngleOffset = ShakeAngleOffset;
        /// <summary>
        /// 基础的震动方向
        /// </summary>
        public float ShakeDirection = ShakeDirection;
        /// <summary>
        /// 基础的位置
        /// </summary>
        public Vector2 ShakePosition = ShakePosition;
        /// <summary>
        /// 震动周期数，控制往复运动的次数
        /// </summary>
        public float Vibrations = vibrations;
        public void Update()
        {
            float progress = ShakeTime / (float)ShakeLifeTime;

            float currentStrength = MathHelper.Lerp(ShakeStrength, 0, EasingHelper.EaseOutCubic(progress));

            if (UseDistanceFade)
            {
                Player player = Main.LocalPlayer;
                float toPlayerLength = (ShakePosition - player.Center).Length();
                // 距离越远，强度越弱
                currentStrength *= MathHelper.Clamp(1f - (toPlayerLength / ShakeEffectDistance), 0f, 1f);
            }

            Vector2 offset = Vector2.Zero;
            switch (Type)
            {
                case ScreenShakeType.Random:
                    offset = Vector2.UnitX.RotatedBy(ShakeDirection).RotatedByRandom(ShakeAngleOffset) * currentStrength;
                    break;
                case ScreenShakeType.PunchCos:
                    float vibrationMultiplierCos = (float)Math.Cos(progress * MathHelper.TwoPi * Vibrations);
                    offset = Vector2.UnitX.RotatedBy(ShakeDirection) * currentStrength * vibrationMultiplierCos;
                    break;
                case ScreenShakeType.PunchSin:
                    float vibrationMultiplierSin = (float)Math.Sin(progress * MathHelper.TwoPi * Vibrations);
                    offset = Vector2.UnitX.RotatedBy(ShakeDirection) * currentStrength * vibrationMultiplierSin;
                    break;
            }

            Main.screenPosition += offset * LAPConfig.Instance.ScreenShakeStrength;
            ShakeTime++;
        }
    }
    public class ScreenShakeSystem : ModSystem
    {
        public static readonly List<ScreenShakeInfo> ScreenShakes = [];
        public override void ModifyScreenPosition()
        {
            if (ScreenShakes.Count == 0)
                return;

            foreach (ScreenShakeInfo shake in ScreenShakes)
            {
                shake.Update();
            }
            ScreenShakes.RemoveAll(s => s.ShakeTime >= s.ShakeLifeTime);
        }
        public static void AddScreenShakes(Vector2 shakePosition, float shakeStrength, int shakeLifeTime, float shakeDirection, float randomAngleoffset = MathHelper.TwoPi, bool useDistanceFade = true, int ShakeEffectDistance = 1000)
        {
            ScreenShakeInfo screenShakeInfo = new ScreenShakeInfo(shakePosition, shakeStrength, shakeLifeTime, shakeDirection, randomAngleoffset, useDistanceFade, ShakeEffectDistance);
            ScreenShakes.Add(screenShakeInfo);
        }
        /// <summary>
        /// 添加定向周期震屏
        /// </summary>
        public static void AddScreenShake_Sin(Vector2 position, float strength, int lifeTime, float direction, float vibrations = 3f, bool useDistanceFade = true, int effectDistance = 1000)
        {
            var info = new ScreenShakeInfo(position, strength, lifeTime, direction, 0f, useDistanceFade, effectDistance, ScreenShakeType.PunchSin, vibrations);
            ScreenShakes.Add(info);
        }
        public static void AddScreenShake_Cos(Vector2 position, float strength, int lifeTime, float direction, float vibrations = 3f, bool useDistanceFade = true, int effectDistance = 1000)
        {
            var info = new ScreenShakeInfo(position, strength, lifeTime, direction, 0f, useDistanceFade, effectDistance, ScreenShakeType.PunchCos, vibrations);
            ScreenShakes.Add(info);
        }
    }
}
