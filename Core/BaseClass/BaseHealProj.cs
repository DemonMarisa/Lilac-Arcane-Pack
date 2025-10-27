using LAP.Assets.TextureRegister;
using LAP.Core.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.BaseClass
{
    public abstract class BaseHealProj : ModProjectile
    {
        public override string Texture => LAPTextureRegister.InvisibleTexturePath;
        // 需要搭配方法使用
        #region 别名
        public virtual float FlySpeed => 12f;
        public virtual float Acceleration => 35f;
        public virtual int HealAmt => Main.rand.Next(5, 10);

        public virtual bool UsePredictMult => true;
        public Player Healer => Main.player[Projectile.owner];
        #endregion

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 24;
            Projectile.friendly = true;
            //默认300
            Projectile.timeLeft = 30000;
            //干掉不可穿墙
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 5;
            ExSD();
        }
        // 额外的SD
        public virtual void ExSD()
        {

        }

        public override void AI()
        {
            //直接追踪锁定玩家位置就行了。我也不知道为什么要做别的事情。
            //人都似了为什么还要跑这个弹幕？
            //距离玩家过远也直接处死这个弹幕，没得玩的
            if (!Healer.active || Healer.dead || (Projectile.Center - Healer.Center).Length() > 3000f)
            {
                Projectile.netUpdate = true;
                Projectile.Kill();
                return;
            }
            //设置跟踪玩家的AI
            if (UsePredictMult)
            {
                float DistanceToPlayer = (Projectile.Center - Healer.Center).Length();
                float PredictMult = DistanceToPlayer / (FlySpeed * (Projectile.extraUpdates + 1));
                Projectile.HomingTarget(Healer.Center + Healer.velocity * PredictMult, 3000, FlySpeed, Acceleration);
            }
            else
            {
                Projectile.HomingTarget(Healer.Center, 3000, FlySpeed, Acceleration);
            }

            float distance = (Projectile.Center - Healer.Center).Length();
            if (Projectile.Hitbox.Intersects(Healer.Hitbox) || distance < 30f)
            {
                //干掉射弹即可
                Projectile.netUpdate = true;
                Projectile.Kill();
            }
            ExAI();
        }
        // 额外的AI
        public virtual void ExAI()
        {

        }
        public override void OnKill(int timeLeft)
        {
            //根据提供的恢复量给予治疗
            Healer.Heal(HealAmt);
            ExKill();
        }
        // 额外的Kill
        public virtual void ExKill()
        {

        }
    }
}
