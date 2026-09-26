using LAP.Core.Graphics.DeepGlow;
using LAP.Core.Graphics.LAPCustomSky;
using LAP.Core.Graphics.LAPPostProgress;
using LAP.Core.Graphics.Lightning;
using LAP.Core.Graphics.PixelatedRender;
using LAP.Core.Graphics.VFX;
using LAP.Core.MetaBallsSystem;
using LAP.Core.ParticleSystem;
using LAP.Core.ParticleSystem_ECS;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace UCA.Core.ILEditingManger
{
    public class DrawLayerManger : ModSystem
    {
        // 整体绘制顺序是 NPC - 射弹 - 玩家 - 粒子
        public override void Load()
        {
            On_Main.DrawDust += MetaBallManager.DrawRenderTarget;

            On_Main.DrawDust += BaseParticleManager.DrawParticles;
            On_Main.DrawDust += ParticleDataManager.DrawParticle_ECS;

            On_Main.DrawProjectiles += VFXManager.DrawVFXInstance_PreProjectiles;
            // 保证VFX系统的Add绘制在最高层级
            On_Main.DrawDust += VFXManager.DrawVFXInstance_PostDust;

            On_Main.DrawDust += PixelatedRenderManger.DrawTarget_BeforeDust;
            On_Main.DrawPlayers_AfterProjectiles += PixelatedRenderManger.DrawTarget_BeforePlayers;

            On_Main.DrawDust += LightningBuilder.DrawLightning;

            On_Main.DrawProjectiles += DeepGlow.Hook_AfterProjectile;
            On_Main.DoDraw_Tiles_Solid += DeepGlow.Hook_BeforeTile;
            On_Main.DrawDust += DeepGlow.Hook_AfterDust;
            On_FilterManager.EndCapture += DeepGlow.DrawDeepGlow;

            On_Main.DrawBG += LAPCustomSkyManager.DrawLAPBG;
            On_Main.DrawUnderworldBackground += LAPCustomSkyManager.DrawUnderworldBackgroundLAPBG;

            On_FilterManager.EndCapture += LAPPostProgressManager.On_FilterManager_EndCapture;
        }
        public override void Unload()
        {
            On_Main.DrawDust -= MetaBallManager.DrawRenderTarget;

            On_Main.DrawDust -= BaseParticleManager.DrawParticles;
            On_Main.DrawDust -= ParticleDataManager.DrawParticle_ECS;

            On_Main.DrawProjectiles -= VFXManager.DrawVFXInstance_PreProjectiles;
            // 保证VFX系统的Add绘制在最高层级
            On_Main.DrawDust -= VFXManager.DrawVFXInstance_PostDust;

            On_Main.DrawDust -= PixelatedRenderManger.DrawTarget_BeforeDust;
            On_Main.DrawPlayers_AfterProjectiles -= PixelatedRenderManger.DrawTarget_BeforePlayers;

            On_Main.DrawDust -= LightningBuilder.DrawLightning;

            On_Main.DrawProjectiles -= DeepGlow.Hook_AfterProjectile;
            On_Main.DoDraw_Tiles_Solid -= DeepGlow.Hook_BeforeTile;
            On_Main.DrawDust -= DeepGlow.Hook_AfterDust;
            On_FilterManager.EndCapture -= DeepGlow.DrawDeepGlow;

            On_Main.DrawBG -= LAPCustomSkyManager.DrawLAPBG;
            On_Main.DrawUnderworldBackground -= LAPCustomSkyManager.DrawUnderworldBackgroundLAPBG;

            On_FilterManager.EndCapture -= LAPPostProgressManager.On_FilterManager_EndCapture;
        }
    }
}
