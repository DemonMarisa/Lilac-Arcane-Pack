using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using LAP.Core.ParticleSystem;

namespace LAP.Core.Graphics.DrawNode
{
    public class NodeManager : ModSystem
    {
        public static List<DrawNode> NodeCollection = [];
        public static readonly List<DrawNode> ActivePixelNode = [];
        public static readonly List<DrawNode> ActiveNode = [];
        /// <summary>
        /// 清除世界状态时调用（例如退出世界时）。
        /// </summary>
        public override void ClearWorld()
        {
            ActivePixelNode.Clear();
            ActiveNode.Clear();
        }
        // 粒子更新
        public override void PostUpdateDusts()
        {
            UpdatePixelNode();
            UpdateNode();
        }
        #region 更新像素化节点
        public static void UpdatePixelNode()
        {
            if (ActivePixelNode.Count == 0)
                return;

            for (int i = 0; i < ActivePixelNode.Count; i++)
            {
                ActivePixelNode[i].Update();
                if (ActivePixelNode[i].UpDatePos())
                    ActivePixelNode[i].Position += ActivePixelNode[i].Velocity;
                ActivePixelNode[i].Time++;

                if (ActivePixelNode[i].ExtraUpdate != 0)
                {
                    for (int j = 0; j < ActivePixelNode[i].ExtraUpdate; j++)
                    {
                        ActivePixelNode[i].Update();
                        if (ActivePixelNode[i].UpDatePos())
                            ActivePixelNode[i].Position += ActivePixelNode[i].Velocity;
                        ActivePixelNode[i].Time++;
                    }
                }

                if (ActivePixelNode[i].Time >= ActivePixelNode[i].Lifetime)
                {
                    ActivePixelNode[i].OnKill();
                    ActivePixelNode.Remove(ActivePixelNode[i]);
                }
            }

        }
        #endregion
        public static void UpdateNode()
        {
            if (ActiveNode.Count == 0)
                return;

            for (int i = 0; i < ActiveNode.Count; i++)
            {
                ActiveNode[i].Update();
                ActiveNode[i].Position += ActiveNode[i].Velocity;
                ActiveNode[i].Time++;

                if (ActiveNode[i].ExtraUpdate != 0)
                {
                    for (int j = 0; j < ActiveNode[i].ExtraUpdate; j++)
                    {
                        ActiveNode[i].Update();
                        ActiveNode[i].Position += ActiveNode[i].Velocity;
                        ActiveNode[i].Time++;
                    }
                }

                if (ActiveNode[i].Time >= ActiveNode[i].Lifetime)
                {
                    ActiveNode[i].OnKill();
                    ActiveNode.Remove(ActiveNode[i]);
                }
            }
        }

        public static void DrawNode(On_Main.orig_DrawDust orig, Main self)
        {
            if (ActiveNode.Count != 0)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
                for (int i = 0; i < ActiveNode.Count; i++)
                {
                    if (ActiveNode[i].Layer == DrawLayer.BeforeDust)
                        ActiveNode[i].Draw(Main.spriteBatch);
                }
                Main.spriteBatch.End();

                orig(self);

                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
                for (int i = 0; i < ActiveNode.Count; i++)
                {
                    if (ActiveNode[i].Layer == DrawLayer.AfterDust)
                        ActiveNode[i].Draw(Main.spriteBatch);
                }
                Main.spriteBatch.End();
            }
            else
            {
                orig(self);
            }

        }
    }
}
