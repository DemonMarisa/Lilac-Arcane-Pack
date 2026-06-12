using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.DeepGlow
{
    public class DeepGlowLayerManager : ModSystem
    {
        public static Queue<Action> GlowRequests_AfterProjectile = new Queue<Action>();
        public static Queue<Action> GlowRequests_AfterDust = new Queue<Action>();
        public override void Load()
        {
            base.Load();
        }
        public override void Unload()
        {
            base.Unload();
        }
    }
}
