using Microsoft.Xna.Framework.Graphics;
using ReLogic.Threading;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.Graphics.DrawNode
{
    /// <summary>
    /// 这个类是用于类似可以快速合批渲染使用shader的粒子，开销比较大
    /// </summary>
    public class NodeManager : ModSystem
    {
        public static List<DrawNode> NodeCollection = [];

        public static List<DrawNode> PostDustAlpha = [];
        public static List<DrawNode> PostDustNonPreMult = [];
        public static List<DrawNode> PostDustAdd = [];

        public static List<DrawNode> PostDustAlphaNoShader = [];
        public static List<DrawNode> PostDustNonPreMultNoShader = [];
        public static List<DrawNode> PostDustAddNoShader = [];

        public static List<DrawNode> PreProjectileAlpha = [];
        public static List<DrawNode> PreProjectileNonPreMult = [];
        public static List<DrawNode> PreProjectileAdd = [];

        public static List<DrawNode> PreProjAlphaNoShader = [];
        public static List<DrawNode> PreProjNonPreMultNoShader = [];
        public static List<DrawNode> PreProjAddNoShader = [];
        /// <summary>
        /// 清除世界状态时调用（例如退出世界时）。
        /// </summary>
        public override void ClearWorld()
        {
            PostDustAlpha.Clear();
            PostDustNonPreMult.Clear();
            PostDustAdd.Clear();
            PreProjectileAlpha.Clear();
            PreProjectileNonPreMult.Clear();
            PreProjectileAdd.Clear();
        }
        // 粒子更新
        public override void PostUpdateDusts()
        {
            UpdateNode( PostDustAlpha);
            UpdateNode( PostDustNonPreMult);
            UpdateNode( PostDustAdd);

            UpdateNode( PreProjectileAlpha);
            UpdateNode( PreProjectileNonPreMult);
            UpdateNode( PreProjectileAdd);

            UpdateNode( PostDustAlphaNoShader);
            UpdateNode( PostDustNonPreMultNoShader);
            UpdateNode( PostDustAddNoShader);

            UpdateNode( PreProjAlphaNoShader);
            UpdateNode( PreProjNonPreMultNoShader);
            UpdateNode( PreProjAddNoShader);
        }
        public static void DrawNode_PreProjectiles(On_Main.orig_DrawProjectiles orig, Main self)
        {
            DrawNodes(PreProjectileAlpha, BlendState.AlphaBlend);
            DrawNodes(PreProjectileAdd, BlendState.Additive);
            DrawNodes(PreProjectileNonPreMult, BlendState.NonPremultiplied);

            DrawNodes(PreProjAlphaNoShader, BlendState.AlphaBlend);
            DrawNodes(PreProjAddNoShader, BlendState.Additive);
            DrawNodes(PreProjNonPreMultNoShader, BlendState.NonPremultiplied);
            orig(self);
        }
        public static void DrawNode(On_Main.orig_DrawDust orig, Main self)
        {
            orig(self);
            DrawNodes(PostDustAlpha, BlendState.AlphaBlend);
            DrawNodes(PostDustAdd, BlendState.Additive);
            DrawNodes(PostDustNonPreMult, BlendState.NonPremultiplied);

            DrawNodes(PostDustAlphaNoShader, BlendState.AlphaBlend);
            DrawNodes(PostDustAddNoShader, BlendState.Additive);
            DrawNodes(PostDustNonPreMultNoShader, BlendState.NonPremultiplied);
        }
        public static void UpdateNode(List<DrawNode> updatelist)
        {
            if (updatelist.Count == 0)
                return;
            FastParallel.For(0, updatelist.Count, (j, k, callback) =>
            {
                for (int i = j; i < k; i++)
                {
                    DrawNode node = updatelist[i];
                    node.Update();
                    node.Position += updatelist[i].Velocity;
                    node.Time++;
                    if (node.ExtraUpdate != 0)
                    {
                        for (int a = 0; a < node.ExtraUpdate; a++)
                        {
                            if (node.ExtraUpdate == 0)
                                break;
                            node.Update();
                            node.Position += node.Velocity;
                            node.Time++;
                        }
                    }
                }
            });
            updatelist.RemoveAll(node =>
            {
                if (node.Time >= node.Lifetime)
                {
                    node.OnKill();
                    return true;
                }
                return false;
            });
        }
        public static void DrawNodes(List<DrawNode> updatelist, BlendState state)
        {
            if (updatelist.Count != 0)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, state, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                for (int i = 0; i < updatelist.Count; i++)
                {
                    updatelist[i].Draw(Main.spriteBatch);
                }
                Main.spriteBatch.End();
            }
        }
    }
}
