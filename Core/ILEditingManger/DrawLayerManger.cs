using LAP.Core.Graphics.DrawNode;
using LAP.Core.Graphics.PixelatedRender;
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
            On_Main.DrawProjectiles += NodeManager.DrawNode_PreProjectiles;
            On_Main.DrawDust += NodeManager.DrawNode;
            On_Main.DrawDust += PixelatedRenderManger.DrawTarget_BeforeDust;
            On_Main.DrawPlayers_AfterProjectiles += PixelatedRenderManger.DrawTarget_BeforePlayers;
        }
        public override void Unload()
        {
            On_Main.DrawDust -= MetaBallManager.DrawRenderTarget;
            On_Main.DrawDust -= BaseParticleManager.DrawParticles;
            On_Main.DrawProjectiles -= NodeManager.DrawNode_PreProjectiles;
            On_Main.DrawDust -= NodeManager.DrawNode;
            On_Main.DrawDust -= PixelatedRenderManger.DrawTarget_BeforeDust;
            On_Main.DrawPlayers_AfterProjectiles -= PixelatedRenderManger.DrawTarget_BeforePlayers;
        }
    }
}
