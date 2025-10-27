using LAP.Core.Graphics.DrawNode;
using LAP.Core.Graphics.PixelRender;
using LAP.Core.MetaBallsSystem;
using LAP.Core.ParticleSystem;
using Terraria;
using Terraria.ModLoader;

namespace UCA.Core.ILEditingManger
{
    public class DrawLayerManger : ModSystem
    {
        public override void Load()
        {
            On_Main.DrawDust += MetaBallManager.DrawRenderTarget;
            On_Main.DrawDust += BaseParticleManager.DrawParticles;
            On_Main.DrawDust += PixelRenderSystem.DrawTarget_Dust;
            On_Main.DrawDust += NodeManager.DrawNode;
        }

        public override void Unload()
        {
            On_Main.DrawDust -= MetaBallManager.DrawRenderTarget;
            On_Main.DrawDust -= BaseParticleManager.DrawParticles;
            On_Main.DrawDust -= PixelRenderSystem.DrawTarget_Dust;
            On_Main.DrawDust -= NodeManager.DrawNode;
        }
    }
}
