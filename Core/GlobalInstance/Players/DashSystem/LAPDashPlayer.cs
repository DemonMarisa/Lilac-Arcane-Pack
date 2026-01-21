using LAP.Core.Keybind;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players.DashSystem
{
    public class LAPDashPlayer : ModPlayer
    {
        public static List<BasePlayerDash> DashCollection = [];
        public int CurDashID;
        /// <summary>
        /// 这个覆盖后必须执行完此次冲刺才会重置
        /// </summary>
        public int OverideCurDashID;
        // 冲刺计时
        public int DashTime = 0;
        // 冲刺冷却
        public int DashDelay = 0;
        public int VanillaDashInput;
        public int BeginDirection;
        public bool BeginDash;
        // 记录每个NPC的whoami和对应的冷却
        public int[] NPCImmuneTime = new int[Main.maxNPCs];
        public override void ResetEffects()
        {
            CheckNPCImmuneTime();
            OtherReset();
        }
        public override void PostUpdateRunSpeeds()
        {
            if (DashCollection.Count == 0)
                return;
            if (CurDashID == -1)
                return;
            // 这两个原版源码判了
            if (Player.grappling[0] == -1 && !Player.tongued)
            {
                int Index = CurDashID;
                if (OverideCurDashID != -1)
                    Index = OverideCurDashID;
                BasePlayerDash ActiveDash = DashCollection[Index];
                // 监测是否开始冲刺
                HandleDashBegin(out bool ThisCanDash);
                if (ThisCanDash)
                {
                    // 如果开始冲刺，赋值并应用起始效果
                    ActiveDash.OnDashStart(Player);
                    DashTime = ActiveDash.DashTime(Player);
                    Player.SetImmuneTimeForAllTypes(ActiveDash.ImmuneTime(Player));
                }
                if (DashTime > 0)
                {
                    if (!ActiveDash.UseCustomDashSpeed)
                    {
                        float PlayerXVel = BeginDirection * Vector2.UnitX.X * ActiveDash.DashSpeed(Player);
                        if (MathF.Abs(Player.velocity.X) < MathF.Abs(PlayerXVel))
                            Player.velocity.X = MathHelper.Lerp(PlayerXVel * ActiveDash.DashEndSpeedMult(Player), PlayerXVel, ActiveDash.DashAmount(Player, DashTime, ActiveDash.DashTime(Player)));
                    }
                    else ActiveDash.ModifyDashSpeed(Player);
                    ActiveDash.DuringDash(Player);
                    BeginDash = true;
                    CheckNPCHit(ActiveDash);
                }
                if (BeginDash && DashTime == 0)
                {
                    ActiveDash.OnDashEnd(Player);
                    DashDelay = ActiveDash.DashDelay(Player);
                    BeginDash = false;
                    OverideCurDashID = -1;
                }
            }
        }
        public void HandleDashBegin(out bool CanDash)
        {
            bool canDash = false;
            CanDash = canDash;
            if (DashTime > 0 || DashDelay > 0 || BeginDash) // 冲刺或CD时时始终不可再次冲刺
                return;
            BeginDirection = Player.direction;
            if (LAPKeybind.DashHotKey.GetAssignedKeys().Count != 0)
            {
                if (LAPKeybind.DashHotKey.JustPressed)
                {
                    CanDash = true;
                }
                return;
            }
            // 原版的双击冲刺判定
            bool vanillaLeftDashInput = Player.controlLeft && Player.releaseLeft;
            bool vanillaRightDashInput = Player.controlRight && Player.releaseRight;
            if (vanillaRightDashInput)
            {
                if (VanillaDashInput > 0)
                {
                    canDash = true;
                    VanillaDashInput = 0;
                }
                else VanillaDashInput = 15;
            }
            else if (vanillaLeftDashInput)
            {
                if (VanillaDashInput < 0)
                {
                    canDash = true;
                    VanillaDashInput = 0;
                }
                else VanillaDashInput = -15;
            }
            CanDash = canDash;
        }
        public void CheckNPCImmuneTime()
        {
            for (int i = 0; i < NPCImmuneTime.Length; i++)
            {
                if (NPCImmuneTime[i] > 0)
                    NPCImmuneTime[i]--;
            }
        }
        public void OtherReset()
        {
            if (DashTime > 0)
                DashTime--;
            if (DashDelay > 0)
                DashDelay--;
            if (VanillaDashInput < 0)
                VanillaDashInput++;
            else if (VanillaDashInput > 0)
                VanillaDashInput--;
            CurDashID = -1;
        }
        public void CheckNPCHit(BasePlayerDash ActiveDash)
        {
            Rectangle hitArea = new Rectangle((int)(Player.position.X + Player.velocity.X * 0.5 - 4f), (int)(Player.position.Y + Player.velocity.Y * 0.5 - 4), Player.width + 8, Player.height + 8);
            foreach (NPC n in Main.ActiveNPCs)
            {
                if (Player.dontHurtCritters && NPCID.Sets.CountsAsCritter[n.type])
                    continue;
                if (NPCImmuneTime[n.whoAmI] > 0)
                    return;
                if (!n.dontTakeDamage && !n.friendly)
                {
                    if (ActiveDash.Colliding(hitArea, n.Hitbox) && (n.noTileCollide || Player.CanHit(n)))
                    {
                        int npcPreDamageHP = n.life;
                        DashDamageInfo dashDamageInfo = ActiveDash.DashDamageInfo(Player);
                        ActiveDash.ModifyDashDamage(Player, ref dashDamageInfo);
                        int dashDamage = (int)Player.GetTotalDamage(dashDamageInfo.damageClass).ApplyTo(dashDamageInfo.Damage);
                        float dashKB = Player.GetTotalKnockback(dashDamageInfo.damageClass).ApplyTo(dashDamageInfo.KnockBack);
                        bool crit = Main.rand.Next(100) < Player.GetTotalCritChance(dashDamageInfo.damageClass);
                        Player.ApplyDamageToNPC(n, dashDamage, dashKB, Player.direction, crit, dashDamageInfo.damageClass, true);
                        Player.SetImmuneTimeForAllTypes(ActiveDash.DashrHitImmuneTime(Player));
                        NPCImmuneTime[n.whoAmI] = ActiveDash.DashHitCoolDown(Player);
                        int npcPostDamageHP = n.life;
                        ActiveDash.OnHitNPC(Player, n, npcPreDamageHP - npcPostDamageHP);
                    }
                }
            }
        }
    }
}
