using LAP.Core.Keybind;
using LAP.Core.NetCode.NetUtilities;
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
        public BasePlayerDash CurDash;
        public int CurDashID = -1;
        /// <summary>
        /// 这个覆盖后必须执行完此次冲刺才会重置
        /// </summary>
        public int OverideCurDashID = -1;
        /// <summary>
        /// 立即冲刺
        /// </summary>
        public int ImmediatelyDashID = -1;
        // 冲刺计时
        public int DashTime = 0;
        // 冲刺冷却
        public int DashDelay = 0;
        public int VanillaDashInput;
        public int BeginDirection;
        public bool BeginDash;
        public Vector2 BeginVelocity;
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
            if (CurDashID == -1 && OverideCurDashID == -1 && ImmediatelyDashID == -1 && DashTime == 0)
                return;
            // 这两个原版源码判了
            if (Player.grappling[0] == -1 && !Player.tongued)
            {
                int Index = CurDashID;
                if (OverideCurDashID != -1)
                    Index = OverideCurDashID;
                bool ThisCanDash = false;
                // 监测是否开始冲刺
                // 如果外部发出了立即冲刺指令，则处理冲刺
                if (ImmediatelyDashID != -1)
                {
                    Index = ImmediatelyDashID;
                    ThisCanDash = true;
                    HandleImmediatelyDash();
                    // 这是为了保证只调用一次
                    ImmediatelyDashID = -1;
                }
                else if (Main.myPlayer == Player.whoAmI)
                {
                    HandleDashBegin(out bool NormalDash);
                    ThisCanDash = NormalDash;
                }
                if (ThisCanDash)
                {
                    BasePlayerDash ActiveDash = DashCollection[Index];
                    CurDash = ActiveDash.Clone();
                    // 如果以后再有改动，注意查看一遍多人同步代码
                    if (Main.myPlayer == Player.whoAmI)
                        Player.SyncedDash(CurDash.Type);
                }
                if (ThisCanDash && !BeginDash)
                {
                    if (!CurDash.CanUseDash(Player))
                        return;
                    BeginVelocity = Player.velocity;
                    // 如果开始冲刺，赋值并应用起始效果
                    CurDash.OnDashStart(Player);
                    DashTime = CurDash.DashTime(Player);
                    BeginDash = true;
                    Player.SetImmuneTimeForAllTypes(CurDash.ImmuneTime(Player));
                }
                if (DashTime > 0)
                {
                    if (!CurDash.UseCustomDashSpeed)
                    {
                        float PlayerXVel = CurDash.DashSpeed(Player);
                        // 这样写是因为DashTime是从最高值逐渐递减的
                        float progress = CurDash.DashAmount(Player, DashTime, CurDash.DashTime(Player));
                        progress = MathHelper.Clamp(progress, 0f, 1f);
                        float FianlXVel = MathHelper.Lerp(PlayerXVel * CurDash.DashEndSpeedMult(Player), PlayerXVel, progress);
                        // 开始的时候会记录当前的玩家速度，如果要应用的冲刺速度低于玩家速度，则不会继续降低速度
                        // 并且如果太快，会强制应用新速度
                        if (MathF.Abs(BeginVelocity.X) < 1e3)
                        {
                            if (MathF.Abs(BeginVelocity.X) < MathF.Abs(FianlXVel))
                                Player.velocity.X = MathF.Abs(FianlXVel) * BeginDirection;
                            else
                                Player.velocity.X = MathF.Abs(BeginVelocity.X) * BeginDirection;
                        }
                        else
                            Player.velocity.X = MathF.Abs(FianlXVel) * BeginDirection;
                    }
                    else
                        CurDash.ModifyDashSpeed(Player);
                    if (Main.myPlayer == Player.whoAmI)
                        CheckNPCHit(CurDash);
                    CurDash.DuringDash(Player);
                    BeginDash = true;
                }
                if (BeginDash && DashTime == 0)
                {
                    CurDash.OnDashEnd(Player);
                    BeginVelocity = Vector2.Zero;
                    DashDelay = CurDash.DashDelay(Player);
                    BeginDash = false;
                    ImmediatelyDashID = -1;
                    CurDash = null;
                }
            }
        }
        public void HandleDashBegin(out bool CanDash)
        {
            bool canDash = false;
            CanDash = canDash;
            // 这一段判定只在本地判定
            if (DashTime > 0 || DashDelay > 0 || BeginDash) // 冲刺或CD时时始终不可再次冲刺
                return;
            BeginDirection = Player.direction;
            if (LAPKeybind.DashHotKey.GetAssignedKeys().Count != 0)
            {
                if (LAPKeybind.DashHotKey.JustPressed)
                {
                    if (Player.controlLeft)
                        BeginDirection = -1;
                    else if (Player.controlRight)
                        BeginDirection = 1;
                    CanDash = true;
                }
            }
            else
            {
                // 原版的双击冲刺判定
                bool vanillaLeftDashInput = Player.controlLeft && Player.releaseLeft;
                bool vanillaRightDashInput = Player.controlRight && Player.releaseRight;
                if (vanillaRightDashInput)
                {
                    if (VanillaDashInput > 0)
                    {
                        BeginDirection = 1;
                        canDash = true;
                        VanillaDashInput = 0;
                    }
                    else
                        VanillaDashInput = 15;
                }
                else if (vanillaLeftDashInput)
                {
                    if (VanillaDashInput < 0)
                    {
                        BeginDirection = -1;
                        canDash = true;
                        VanillaDashInput = 0;
                    }
                    else
                        VanillaDashInput = -15;
                }
                CanDash = canDash;
            }
        }
        public void HandleImmediatelyDash()
        {
            BeginDash = false;
            CurDash?.OnDashEnd(Player);
            BeginDirection = Player.direction;
            if (Player.controlLeft)
                BeginDirection = -1;
            else if (Player.controlRight)
                BeginDirection = 1;
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
            OverideCurDashID = -1;
        }
        public void CheckNPCHit(BasePlayerDash ActiveDash)
        {
            if (Player.whoAmI != Main.myPlayer)
                return;
            Rectangle hitArea = new((int)(Player.position.X + Player.velocity.X * 0.5 - 4f), (int)(Player.position.Y + Player.velocity.Y * 0.5 - 4), Player.width + 8, Player.height + 8);
            foreach (NPC n in Main.ActiveNPCs)
            {
                if (Player.dontHurtCritters && NPCID.Sets.CountsAsCritter[n.type])
                    continue;
                if (NPCImmuneTime[n.whoAmI] > 0)
                    return;
                // 这个ImmunityCooldown是从内往外传的
                int cd = ImmunityCooldownID.General;
                bool? hasModNPC = n.ModNPC?.CanHitPlayer(Player, ref cd);
                if (hasModNPC is not null)
                {
                    if (!hasModNPC.Value)
                        continue;
                }
                if (!ActiveDash.CanHitNPC(Player, n))
                    continue;
                if (!n.dontTakeDamage && !n.friendly)
                {
                    bool hitRec = ActiveDash.Colliding(Player ,hitArea, n.Hitbox) || ActiveDash.Colliding(Player, Player.Hitbox, n.Hitbox);
                    if (hitRec && Player.CanHit(n))
                    {
                        int npcPreDamageHP = n.life;
                        DashDamageInfo dashDamageInfo = ActiveDash.DashDamageInfo(Player);
                        ActiveDash.ModifyDashDamage(Player, ref dashDamageInfo);
                        int dashDamage = (int)Player.GetTotalDamage(dashDamageInfo.damageClass).ApplyTo(dashDamageInfo.Damage);
                        float dashKB = Player.GetTotalKnockback(dashDamageInfo.damageClass).ApplyTo(dashDamageInfo.KnockBack);
                        bool crit = Main.rand.Next(100) < Player.GetTotalCritChance(dashDamageInfo.damageClass);
                        NPC.HitInfo hit = new()
                        {
                            Damage = dashDamage,
                            Knockback = dashKB,
                            HitDirection = Player.direction,
                            Crit = crit,
                            DamageType = dashDamageInfo.damageClass
                        };
                        ActiveDash.ModifyOnHitNPC(Player, ref hit);
                        Player.ApplyDamageToNPC(n, hit.Damage, hit.Knockback, hit.HitDirection, hit.Crit, hit.DamageType);
                        Player.SetImmuneTimeForAllTypes(ActiveDash.DashHitImmuneTime(Player));
                        NPCImmuneTime[n.whoAmI] = ActiveDash.DashHitCoolDown(Player);
                        int npcPostDamageHP = n.life;
                        ActiveDash.OnHitNPC(Player, n, npcPreDamageHP - npcPostDamageHP);
                    }
                }
            }
        }
    }
}
