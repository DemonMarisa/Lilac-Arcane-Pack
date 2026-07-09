using LAP.Content.Configs;
using LAP.Core.GlobalInstance.Projectiles;
using LAP.Core.IDSets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        /// <summary>
        /// 新建射弹，但是指定伤害类型
        /// </summary>
        /// <returns></returns>
        public static Projectile NewProjWithClass(IEntitySource spawnSource, Vector2 position, Vector2 velocity, int Type, int Damage, float KnockBack, int Owner, DamageClass damageclass, float ai0 = 0f, float ai1 = 0f, float ai2 = 0f)
        {
            int p = Projectile.NewProjectile(spawnSource, position, velocity, Type, Damage, KnockBack, Owner, ai0, ai1, ai2);
            Main.projectile[p].DamageType = damageclass;
            return Main.projectile[p];
        }
        public static bool CheckType<T>(Projectile projectile) where T : ModProjectile
        {
            if (projectile.type == ProjectileType<T>())
                return true;
            return false;
        }
        /// <summary>
        /// 搜索距离指定位置最近的NPC
        /// </summary>
        /// <param name="maxDist">最大搜索距离</param>
        /// <param name="ignoreTiles">穿墙搜索, 默认为</param>
        /// <returns>返回一个NPC实例</returns>
        public static NPC FindClosestTarget(Vector2 center, float maxDist, bool ignoreTiles = true)
        {
            float distStoraged = maxDist;
            NPC acceptableTarget = null;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                float exDist = npc.width + npc.height;
                if (!npc.active || npc.friendly || npc.lifeMax < 5 || !npc.CanBeChasedBy(center, false))
                    continue;
                //单位不可被追踪 或者 超出索敌距离则continue
                if (Vector2.Distance(center, npc.Center) > distStoraged + exDist)
                    continue;
                //搜索符合条件的敌人, 准备返回这个NPC实例
                float curNpcDist = Vector2.Distance(npc.Center, center);
                if (curNpcDist < distStoraged)
                {
                    if (!ignoreTiles)
                    {
                        if (!Collision.CanHitLine(center, 1, 1, npc.Center, 1, 1))
                            continue;
                    }
                    distStoraged = curNpcDist;
                    acceptableTarget = npc;
                }
            }
            //返回这个NPC实例
            return acceptableTarget;
        }
        public static NPC FindClosestNPCExceptSpecific(Vector2 center, float maxDistance, List<NPC> noUseTarget, bool ignoreTiles = true)
        {
            NPC acceptableTarget = null;
            float shortestDistance = maxDistance;

            // Main.npc 数组比 Main.ActiveNPCs 更安全，因为它不会在迭代时被修改，并且包含所有NPC实例。
            // 我们需要检查 npc.active 来确保只处理活跃的NPC。
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                // 基础筛选条件：必须是活跃的、非友好的、可以被追逐的NPC
                if (npc == null || !npc.active || npc.friendly || !npc.CanBeChasedBy())
                    continue;
                // 使用 .Contains() 方法可以简洁高效地完成这个判断。
                // 如果 noUseTarget 为 null 或为空，这个检查也会安全地跳过。
                if (noUseTarget != null && noUseTarget.Contains(npc))
                    continue;
                // 距离筛选
                float distanceToNPC = Vector2.Distance(center, npc.Center);
                // 稍微放宽一点初始距离检查，以考虑到NPC的体积
                float effectiveMaxDistance = maxDistance + (npc.width + npc.height) / 4f;
                if (distanceToNPC > effectiveMaxDistance)
                    continue;
                // 最终筛选：必须比已找到的目标更近，并且满足视线条件
                if (distanceToNPC < shortestDistance)
                {
                    // 检查视线（如果需要）
                    if (ignoreTiles || Collision.CanHitLine(center, 1, 1, npc.Center, 1, 1))
                    {
                        shortestDistance = distanceToNPC;
                        acceptableTarget = npc;
                    }
                }
            }

            return acceptableTarget;
        }
        /// <summary>
        /// 用于根据传入的弹幕伤害进行模式加成计算
        /// </summary>
        public static float PostModeBoostProjDamage(float damage)
        {
            float realDamage = damage * 2;
            if (Main.masterMode)
                realDamage *= 1.5f;
            if (Main.expertMode)
                realDamage *= 2f;
            return realDamage;
        }
        public static float PreModeBoostProjDamage(float damage)
        {
            float realDamage = damage * 0.5f;
            if (Main.expertMode)
                realDamage *= 0.5f;
            if (Main.masterMode)
                realDamage *= 0.66f;
            return realDamage;
        }
        /// <summary>
        /// 用于跟踪指定地点的方法
        /// 只会跟踪你传进去的目标
        /// </summary>
        /// <param name="proj">射弹</param>
        /// <param name="target">射弹目标</param>
        /// <param name="distRequired">最大范围</param>
        /// <param name="speed">射弹速度</param>
        /// <param name="inertia">惯性</param>
        /// <param name="maxAngleChage">角度限制，默认为空. </param>
        public static void HomingTarget(this Projectile proj, Vector2 target, float distRequired, float speed, float inertia, float? maxAngleChage = null)
        {
            if (distRequired > 0 && Vector2.Distance(proj.Center, target) > distRequired)
                return;
            //开始追踪target
            Vector2 home = (target - proj.Center).SafeNormalize(Vector2.UnitY);
            Vector2 velo = (proj.velocity * inertia + home * speed) / (inertia + 1f);
            //这里给了一个角度限制
            if (maxAngleChage.HasValue)
            {
                float curAngle = proj.velocity.ToRotation();
                float tarAngle = velo.ToRotation();
                float angleDiffer = MathHelper.WrapAngle(tarAngle - curAngle);
                //转弧度
                float maxRadians = MathHelper.ToRadians(maxAngleChage.Value);
                if (Math.Abs(angleDiffer) > maxRadians)
                {
                    float clampedAngle = curAngle + Math.Sign(angleDiffer) * maxRadians;
                    float setSpeed = velo.Length();
                    velo = new Vector2((float)Math.Cos(clampedAngle), (float)Math.Sin(clampedAngle)) * setSpeed;
                }
            }
            //设定速度
            proj.velocity = velo;
        }
        public static void HomingTarget(Vector2 center, Vector2 target, ref Vector2 velocity, float distRequired, float speed, float inertia, float? maxAngleChage = null)
        {
            if (distRequired > 0 && Vector2.Distance(center, target) > distRequired)
                return;
            //开始追踪target
            Vector2 home = (target - center).SafeNormalize(Vector2.UnitY);
            Vector2 velo = (target * inertia + home * speed) / (inertia + 1f);
            //这里给了一个角度限制
            if (maxAngleChage.HasValue)
            {
                float curAngle = target.ToRotation();
                float tarAngle = velo.ToRotation();
                float angleDiffer = MathHelper.WrapAngle(tarAngle - curAngle);
                //转弧度
                float maxRadians = MathHelper.ToRadians(maxAngleChage.Value);
                if (Math.Abs(angleDiffer) > maxRadians)
                {
                    float clampedAngle = curAngle + Math.Sign(angleDiffer) * maxRadians;
                    float setSpeed = velo.Length();
                    velo = new Vector2((float)Math.Cos(clampedAngle), (float)Math.Sin(clampedAngle)) * setSpeed;
                }
            }
            //设定速度
            velocity = velo;
        }
        public static Vector2 HalfProjectile(this Projectile proj)
        {
            return new Vector2(proj.width / 2, proj.height / 2);
        }
        public static void SetHeldProj(this Projectile proj, Player Owner, bool SetHeldProj = true, bool SetOwnerDir = true)
        {
            Owner.itemTime = 2;
            Owner.itemAnimation = 2;
            if (SetOwnerDir)
                Owner.ChangeDir(Owner.LocalMouseWorld().X > Owner.Center.X ? 1 : -1);
            if (SetHeldProj)
                Owner.heldProj = proj.whoAmI;
            if (Owner.dead)
                proj.Kill();
        }
        public static bool FinalExtraUpdate(this Projectile proj)
        {
            return proj.numUpdates == -1;
        }
        public static void SpawnLifeStealProj(this Player player, NPC target, IEntitySource Source, int projType, Vector2 Pos, Vector2 vel, int OverridehealAmt = 0, bool Useconditional = false, bool shared = true)
        {
            if (target == null)
                return;
            if (Useconditional && !target.canGhostHeal)
                return;
            int targetplayer = player.whoAmI;
            if (shared)
                targetplayer = FindLowerHPPlayer(player).whoAmI;
            if (Useconditional)
            {
                if (player.moonLeech)
                    Projectile.NewProjectile(Source, Pos, vel, projType, 0, 0f, player.whoAmI, OverridehealAmt, targetplayer);
            }
            else
                Projectile.NewProjectile(Source, Pos, vel, projType, 0, 0f, player.whoAmI, OverridehealAmt, targetplayer);
        }
        public static Player FindLowerHPPlayer(this Player player)
        {
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                int whoAmI = player.whoAmI;
                float precentLife = (float)player.statLife / player.statLifeMax2;
                foreach (Player Activeplayer in Main.ActivePlayers)
                {
                    if (!Activeplayer.active)
                        continue;
                    float thisPlayerPrecentLife = (float)Activeplayer.statLife / Activeplayer.statLifeMax2;
                    if (thisPlayerPrecentLife < precentLife)
                    {
                        precentLife = thisPlayerPrecentLife;
                        whoAmI = Activeplayer.whoAmI;
                    }
                }
                return Main.player[whoAmI];
            }
            else
                return Main.LocalPlayer;
        }
        public static void HomeInNPC(this Projectile proj, float distance, float speed, float inertia, float? maxAngleChage = null, bool ignoreTile = true)
        {
            NPC npc = FindClosestTarget(proj.Center, distance, ignoreTile);
            if (npc is not null)
            {
                proj.HomingTarget(npc.Center, distance, speed, inertia, maxAngleChage);
            }
        }
        public static Player Owner(this Projectile proj)
        {
            return Main.player[proj.owner];
        }
        public static void DrawAfterimages(Projectile proj, int mode, Color lightColor, int typeOneIncrement = 1, Texture2D texture = null)
        {
            if (texture is null)
                texture = TextureAssets.Projectile[proj.type].Value;
            int frameHeight = texture.Height / Main.projFrames[proj.type];
            int frameY = frameHeight * proj.frame;
            float scale = proj.scale;
            float rotation = proj.rotation;
            Rectangle rectangle = new Rectangle(0, frameY, texture.Width, frameHeight);
            Vector2 origin = rectangle.Size() / 2f;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (proj.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;
            bool failedToDrawAfterimages = false;
            if (!LAPConfig.Instance.PerformanceMode)
            {
                Vector2 centerOffset = proj.Size / 2f;
                Color alphaColor = proj.GetAlpha(lightColor);
                switch (mode)
                {
                    case 0:
                        for (int i = 0; i < proj.oldPos.Length; ++i)
                        {
                            Vector2 drawPos = proj.oldPos[i] + centerOffset - Main.screenPosition + new Vector2(0f, proj.gfxOffY);
                            Color color = alphaColor * ((float)(proj.oldPos.Length - i) / (float)proj.oldPos.Length);
                            Main.spriteBatch.Draw(texture, drawPos, new Rectangle?(rectangle), color, rotation, origin, scale, spriteEffects, 0f);
                        }
                        break;
                    case 1:
                        int increment = Math.Max(1, typeOneIncrement);
                        Color drawColor = alphaColor;
                        int afterimageCount = ProjectileID.Sets.TrailCacheLength[proj.type];
                        float afterimageColorCount = (float)afterimageCount * 1.5f;
                        int k = 0;
                        while (k < afterimageCount)
                        {
                            Vector2 drawPos = proj.oldPos[k] + centerOffset - Main.screenPosition + new Vector2(0f, proj.gfxOffY);
                            if (k > 0)
                            {
                                float colorMult = (float)(afterimageCount - k);
                                drawColor *= colorMult / afterimageColorCount;
                            }
                            Main.spriteBatch.Draw(texture, drawPos, new Rectangle?(rectangle), drawColor, rotation, origin, scale, spriteEffects, 0f);
                            k += increment;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < proj.oldPos.Length; ++i)
                        {
                            float afterimageRot = proj.oldRot[i];
                            SpriteEffects sfxForThisAfterimage = proj.oldSpriteDirection[i] == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                            Vector2 drawPos = proj.oldPos[i] + centerOffset - Main.screenPosition + new Vector2(0f, proj.gfxOffY);
                            Color color = alphaColor * ((float)(proj.oldPos.Length - i) / (float)proj.oldPos.Length);
                            Main.spriteBatch.Draw(texture, drawPos, new Rectangle?(rectangle), color, afterimageRot, origin, scale, sfxForThisAfterimage, 0f);
                        }
                        break;
                    default:
                        failedToDrawAfterimages = true;
                        break;
                }
            }
            if (LAPConfig.Instance.PerformanceMode || ProjectileID.Sets.TrailCacheLength[proj.type] <= 0 || failedToDrawAfterimages)
            {
                Vector2 startPos = proj.Center;
                Main.spriteBatch.Draw(texture, startPos - Main.screenPosition + new Vector2(0f, proj.gfxOffY), rectangle, proj.GetAlpha(lightColor), rotation, origin, scale, spriteEffects, 0f);
            }
        }
        public static void AddToSkillProj(this Projectile proj)
        {
            if (!LAPIDSet.WeaponSkillProj.Contains(proj.type))
                LAPIDSet.WeaponSkillProj.Add(proj.type);
        }
        /// <summary>
        /// 根据射弹的extraUpdates和numUpdates计算出玩家本帧的实际位移，可以用于修正射弹的跟随玩家的平滑度
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        public static Vector2 GetOwnerStepFromEu(this Projectile proj)
        {
            int k = proj.extraUpdates - proj.numUpdates;
            int totalUpdates = proj.extraUpdates + 1;
            // 玩家本帧真正的物理位移
            Vector2 realFrameVelocity = proj.Owner().position - proj.Owner().oldPosition;
            // 基于上一帧的绝对位置进行精准线性分步
            Vector2 smoothVel = realFrameVelocity * ((float)(k + 1) / totalUpdates);
            return smoothVel;
        }
    }
}
