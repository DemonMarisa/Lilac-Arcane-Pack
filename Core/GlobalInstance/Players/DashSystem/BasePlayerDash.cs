using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.GlobalInstance.Players.DashSystem
{
    public class BasePlayerDash : ModType
    {
        public int Type;
        /// <summary>
        /// 他为True时会使用模版计算速度，否则会调用ModifyDashSpeed来完全接管速度计算
        /// </summary>
        public bool UseCustomDashSpeed;
        public virtual bool CanHitNPC(Player player, NPC target) => true;
        public virtual DashDamageInfo DashDamageInfo(Player player) => new(50, 3, DamageClass.Default);
        /// <summary>
        /// 决定玩家当前状态是否允许使用该冲刺（如法力值不足、没有特定Buff则返回false）
        /// </summary>
        public virtual bool CanUseDash(Player player) => true;
        /// <summary>
        /// 这个冲刺给予的无敌时间
        /// </summary>
        public virtual int ImmuneTime(Player player) => 12;
        /// <summary>
        /// 这个冲刺撞击敌人后给予的无敌时间
        /// </summary>
        public virtual int DashHitImmuneTime(Player player) => 12;
        /// <summary>
        /// 冲刺的持续时间
        /// </summary>
        public virtual int DashTime(Player player) => 12;
        /// <summary>
        /// 冲刺的冷却
        /// </summary>
        public virtual int DashDelay(Player player) => 30;
        /// <summary>
        /// 冲刺开始时调用
        /// </summary>
        public virtual void OnDashStart(Player player)
        {
        }
        /// <summary>
        /// 冲刺途中调用
        /// </summary>
        public virtual void DuringDash(Player player)
        {
        }
        /// <summary>
        /// 冲刺结束后调用
        /// </summary>
        public virtual void OnDashEnd(Player player)
        {
        }
        /// <summary>
        /// 冲刺的速度
        /// </summary>
        public virtual float DashSpeed(Player player) => 10f;
        /// <summary>
        /// 冲刺结束时的速度
        /// </summary>
        public virtual float DashEndSpeedMult(Player player) => 0.33f;
        /// <summary>
        /// 冲刺减速的参数
        /// </summary>
        public virtual float DashAmount(Player player, int CurAni, int MaxAni)
        {
            return (float)CurAni / MaxAni;
        }
        /// <summary>
        /// 覆写他会完全接管冲刺速度的计算
        /// </summary>
        public virtual void ModifyDashSpeed(Player player)
        {

        }
        /// <summary>
        /// 击中后的冷却时间
        /// </summary>
        public virtual int DashHitCoolDown(Player player) => 12;
        /// <summary>
        /// 允许你修改碰撞逻辑
        /// </summary>
        public virtual bool Colliding(Player player, Rectangle dashHitbox, Rectangle targetHitbox)
        {
            if (dashHitbox.Intersects(targetHitbox))
                return true;
            else
                return false;
        }
        /// <summary>
        /// 允许你修改冲刺的伤害
        /// </summary>
        public virtual void ModifyDashDamage(Player player, ref DashDamageInfo dashDamageInfo)
        {
        }
        /// <summary>
        /// 允许你修改击中NPC时的结构体
        /// </summary>
        public virtual void ModifyOnHitNPC(Player player, ref NPC.HitInfo npcInfo)
        {
        }
        /// <summary>
        /// 击中敌对NPC时调用
        /// </summary>
        public virtual void OnHitNPC(Player player, NPC target, int DamageDone)
        {
        }
        protected sealed override void Register()
        {
            Type = LAPDashPlayer.DashCollection.Count;
            LAPDashPlayer.DashCollection.Add(this);
        }
    }
}
