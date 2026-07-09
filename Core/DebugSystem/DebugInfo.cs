using LAP.Content.Configs;
using LAP.Core.LAPUI.CustomCD;
using LAP.Core.ParticleSystem;
using LAP.Core.ParticleSystem_ECS;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace LAP.Core.DebugSystem
{
    public class DebugInfo : ModSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (!LAPConfig.Instance.DeBugInfo)
                return;
            int mouseIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
            if (mouseIndex != -1)
            {
                layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("LAP Debug Info UI", delegate ()
                {
                    DrawParticleDebugInfo();
                    DrawCDDebugInfo();
                    PerformanceMonitorSystem.DrawPerformanceMetrics();
                    return true;
                }, InterfaceScaleType.UI));
            }
        }
        public static void DrawParticleDebugInfo()
        {
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            int TotalDust = BaseParticleManager.ActiveParticlesAlpha.Count + BaseParticleManager.ActiveParticlesNonPremultiplied.Count + BaseParticleManager.ActiveParticlesAdditive.Count +
                BaseParticleManager.PriorityActiveParticlesAlpha.Count + BaseParticleManager.PriorityActiveParticlesNonPremultiplied.Count +
                BaseParticleManager.PriorityActiveParticlesAdditive.Count + ParticleDataManager.activePoint_add + ParticleDataManager.activePoint_alpha + ParticleDataManager.activePoint_Nonmult;
            string particleCount = $"粒子总数: {TotalDust}";
            Vector2 stringsize = ChatManager.GetStringSize(font, particleCount, Vector2.One);
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, particleCount, LAPUtilities.ScreenCenter_Top() + new Vector2(0, 64), Color.White, 0f, stringsize / 2, new Vector2(1f));
        }
        public static void DrawCDDebugInfo()
        {
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            string particleCount = $"当前CD总数: {CustomCDManger.ActiveCD.Count}";
            Vector2 stringsize = ChatManager.GetStringSize(font, particleCount, Vector2.One);
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, particleCount, LAPUtilities.ScreenCenter_Top() + new Vector2(0, 96), Color.White, 0f, stringsize / 2, new Vector2(1f));
            string cdCount = $"注册了多少cd: {CustomCDManger.CDCollection.Count}";
            Vector2 cdCountsize = ChatManager.GetStringSize(font, cdCount, Vector2.One);
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, cdCount, LAPUtilities.ScreenCenter_Top() + new Vector2(0, 128), Color.White, 0f, cdCountsize / 2, new Vector2(1f));
        }
    }
}
