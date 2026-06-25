using LAP.Core.Enums;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Threading;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.VFX
{
    /// <summary>
    /// 这个类是用于那些比较复杂的特效的管理器，提供了一个全局的VFX系统，可以在其中创建和管理各种特效实例。
    /// </summary>
    public class VFXManager : ModSystem
    {
        public const int MaxVFXPerPool = 500;
        public static bool HasAnyVFX;
        public static List<VFXBehavior> VBehavior = [];

        public static VFXInstance[] VFXInstances = new VFXInstance[MaxVFXPerPool];
        public override void Load()
        {
            for (int i = 0; i < MaxVFXPerPool; i++)
            {
                VFXInstances[i] = new VFXInstance { WhoAmI = i, Active = false };
            }
        }
        public override void Unload()
        {
            for (int i = 0; i < MaxVFXPerPool; i++)
            {
                VFXInstances[i] = null;
            }
        }
        // 粒子更新
        public override void PostUpdateDusts()
        {
            if (!HasAnyVFX)
                return;
            HasAnyVFX = false;
            UpdateNode(VFXInstances);
        }
        public static void DrawVFXInstance_PreProjectiles(On_Main.orig_DrawProjectiles orig, Main self)
        {
            if (!HasAnyVFX)
            {
                orig(self);
                return;
            }
            DrawVFXs(VFXInstances, BlendState.AlphaBlend, DrawLayer.BeforeProjectiles);
            DrawVFXs(VFXInstances, BlendState.NonPremultiplied, DrawLayer.BeforeProjectiles);
            DrawVFXs(VFXInstances, BlendState.Additive, DrawLayer.BeforeProjectiles);
            orig(self);
        }
        public static void DrawVFXInstance_PostDust(On_Main.orig_DrawDust orig, Main self)
        {
            if (!HasAnyVFX)
            {
                orig(self);
                return;
            }
            orig(self);
            DrawVFXs(VFXInstances, BlendState.AlphaBlend, DrawLayer.AfterDusts);
            DrawVFXs(VFXInstances, BlendState.NonPremultiplied, DrawLayer.AfterDusts);
            DrawVFXs(VFXInstances, BlendState.Additive, DrawLayer.AfterDusts);
        }
        public static void UpdateNode(VFXInstance[] updatelist)
        {
            for (int i = 0; i < updatelist.Length; i++)
            {
                VFXInstance vfx = updatelist[i];
                if (!vfx.Active)
                    continue;
                HasAnyVFX = true;
                SingleUpdate();
                if (vfx.ExtraUpdate != 0)
                {
                    for (int a = 0; a < vfx.ExtraUpdate; a++)
                    {
                        if (vfx.ExtraUpdate == 0 || vfx.Time > vfx.Lifetime)
                            break;
                        SingleUpdate();
                    }
                }
                void SingleUpdate()
                {
                    vfx.Behavior.Update();
                    if (vfx.Behavior.UpdatePosition())
                        vfx.Position += vfx.Velocity;
                    vfx.Time++;
                }
                if (vfx.Time >= vfx.Lifetime)
                {
                    vfx.Behavior.OnKill();
                    vfx.Active = false;
                }
            }
        }
        public static void DrawVFXs(VFXInstance[] updatelist, BlendState state, DrawLayer layer)
        {
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, state, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            for (int i = 0; i < updatelist.Length; i++)
            {
                VFXInstance vfx = updatelist[i];
                if (vfx == null || !vfx.Active)
                    continue;
                if (vfx.Behavior.BlendState == state && vfx.Behavior.Layer == layer)
                {
                    vfx.Behavior.Draw();
                }
            }
            Main.spriteBatch.End();
        }
    }
}
