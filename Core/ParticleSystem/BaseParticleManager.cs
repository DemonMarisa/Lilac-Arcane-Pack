using LAP.Content.Configs;
using LAP.Core.ParticleSystem_ECS;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using ReLogic.Threading;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace LAP.Core.ParticleSystem
{
    /// <summary>
    /// 这个粒子系统比较低效
    /// 生成与销毁一个粒子就会在内存中创建或删除一个对象，大量的GC与List的增删会导致性能问题
    /// 但是更简单易用一点
    /// </summary>
    public partial class BaseParticleManager : ModSystem
    {
        public static List<BaseParticle> ActiveParticlesAlpha = [];
        public static List<BaseParticle> ActiveParticlesNonPremultiplied = [];
        public static List<BaseParticle> ActiveParticlesAdditive = [];
        // 先绘制先更新的粒子
        public static List<BaseParticle> PriorityActiveParticlesAlpha = [];
        public static List<BaseParticle> PriorityActiveParticlesNonPremultiplied = [];
        public static List<BaseParticle> PriorityActiveParticlesAdditive = [];
        #region 加载卸载
        //public override void Load()
        //{
        //    On_Main.DrawDust += DrawParticles;
        //}
        //public override void Unload()
        //{
        //    On_Main.DrawDust -= DrawParticles;
        //}
        #endregion
        /// <summary>
        /// 清除世界状态时调用（例如退出世界时）。
        /// </summary>
        public override void ClearWorld()
        {
            ActiveParticlesAlpha.Clear();
            ActiveParticlesNonPremultiplied.Clear();
            ActiveParticlesAdditive.Clear();
            PriorityActiveParticlesAlpha.Clear();
            PriorityActiveParticlesNonPremultiplied.Clear();
            PriorityActiveParticlesAdditive.Clear();
        }

        // 粒子更新
        public override void PostUpdateDusts()
        {
            UpdateParticleList(ActiveParticlesAlpha);
            UpdateParticleList(ActiveParticlesNonPremultiplied);
            UpdateParticleList(ActiveParticlesAdditive);
            UpdateParticleList(PriorityActiveParticlesAlpha);
            UpdateParticleList(PriorityActiveParticlesNonPremultiplied);
            UpdateParticleList(PriorityActiveParticlesAdditive);
        }
        public static void UpdateParticleList(List<BaseParticle> list)
        {
            int count = list.Count;
            if (count == 0)
                return;
            FastParallel.For(0, count, (j, k, callback) =>
            {
                for (int i = j; i < k; i++)
                {
                    BaseParticle particle = list[i];
                    particle.Update();
                    particle.Position += particle.Velocity;
                    particle.Time++;
                }
            });
            list.RemoveAll(particle =>
            {
                if (particle.Time >= particle.Lifetime)
                {
                    particle.OnKill();
                    return true;
                }
                return false;
            });
        }
        // 绘制粒子
        public static void DrawParticles(On_Main.orig_DrawDust orig, Main self)
        {
            // 调用源
            orig(self);
            #region 渲染粒子
            #region 渲染优先粒子
            DrawParticles(PriorityActiveParticlesAlpha, BlendState.AlphaBlend);
            DrawParticles(PriorityActiveParticlesAdditive, BlendState.Additive);
            DrawParticles(PriorityActiveParticlesNonPremultiplied, BlendState.NonPremultiplied);
            #endregion
            #region 渲染常规粒子
            DrawParticles(ActiveParticlesAlpha, BlendState.AlphaBlend);
            DrawParticles(ActiveParticlesAdditive, BlendState.Additive);
            DrawParticles(ActiveParticlesNonPremultiplied, BlendState.NonPremultiplied);
            #endregion
            #endregion
        }
        public static void DrawParticles(List<BaseParticle> list, BlendState bl)
        {
            if (list.Count != 0)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, bl, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].UseScreenCut && LAPUtilities.OutOffScreen(list[i].Position, list[i].ScreenCut))
                        continue;
                    list[i].Draw(Main.spriteBatch);
                }
                Main.spriteBatch.End();
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
            if (mouseIndex != -1)
            {
                if (LAPConfig.Instance.DeBugInfo)
                {
                    layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("LAP ParticleDebug Info UI", delegate ()
                    {
                        DrawDebugInfo();
                        return true;
                    }, InterfaceScaleType.UI));
                }
            }
        }
        public static void DrawDebugInfo()
        {
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            int TotalDust = ActiveParticlesAlpha.Count + ActiveParticlesNonPremultiplied.Count + ActiveParticlesAdditive.Count + PriorityActiveParticlesAlpha.Count + PriorityActiveParticlesNonPremultiplied.Count + PriorityActiveParticlesAdditive.Count + ParticleDataManager.activePoint_add + ParticleDataManager.activePoint_alpha + ParticleDataManager.activePoint_Nonmult;
            string particleCount = $"粒子总数: {TotalDust}";
            Vector2 stringsize = ChatManager.GetStringSize(font, particleCount, Vector2.One);
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, particleCount, LAPUtilities.ScreenCenter_Top() + new Vector2(0, 64),  Color.White, 0f, stringsize / 2, new Vector2(1f));
        }
    }
}
