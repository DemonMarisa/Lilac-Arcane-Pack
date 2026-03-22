using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace LAP.Core.StateMachine.SynedHitEffect
{
    public class HitEffectManager : ModSystem
    {
        public static List<BaseHitEffect> HitEffect = [];
        public override void Unload()
        {
            HitEffect.Clear();
        }
        public static int HEType<T>() where T : BaseHitEffect
        {
            return GetInstance<T>().Type;
        }
        public static void SpawnHitEffect(int ID, int Owner, IEntitySource source, Vector2 Center, Vector2 velocity)
        {
            if (ID < 0 || ID >= HitEffect.Count)
                return;
            Projectile.NewProjectile(source, Center, velocity, ProjectileType<UseForOnHitNPCProj>(), 0, 0, Owner, ID);
        }
    }
}
