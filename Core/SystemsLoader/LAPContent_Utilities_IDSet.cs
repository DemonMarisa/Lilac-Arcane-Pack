using LAP.Core.IDSets;
using Microsoft.Xna.Framework;
using Terraria;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        /// <summary>
        /// 手持射弹的添加模版
        /// </summary>
        public static void AddHeldProj(this Projectile proj)
        {
            if (!LAPIDSet.HeldProj.Contains(proj.type))
                LAPIDSet.HeldProj.Add(proj.type);
            if (!LAPIDSet.ProtectedProj.Contains(proj.type))
                LAPIDSet.ProtectedProj.Add(proj.type);
            if (!LAPIDSet.CantSplitProj.Contains(proj.type))
                LAPIDSet.CantSplitProj.Add(proj.type);
        }
        /// <summary>
        /// 用于非手持射弹的受保护射弹添加模版
        /// </summary>
        public static void AddProtectedProj(this Projectile proj, bool CanSplit = true)
        {
            if (!LAPIDSet.ProtectedProj.Contains(proj.type))
                LAPIDSet.ProtectedProj.Add(proj.type);
            if (!CanSplit)
            {
                if (!LAPIDSet.CantSplitProj.Contains(proj.type))
                    LAPIDSet.CantSplitProj.Add(proj.type);
            }
        }
        /// <summary>
        /// 只添加不可分裂弹幕的模版
        /// 一些弹幕分裂后可能会有预期外的效果，所以直接禁止分裂
        /// </summary>
        public static void AddCantSplitProj(this Projectile proj)
        {
            if (!LAPIDSet.CantSplitProj.Contains(proj.type))
                LAPIDSet.CantSplitProj.Add(proj.type);
        }
        /// <summary>
        /// 登记这个射弹的帧数
        /// </summary>
        public static void RegisterFrame(this Projectile proj, Point frame)
        {
            if (!LAPIDSet.ProjFrame.ContainsKey(proj.type))
                LAPIDSet.ProjFrame.Add(proj.type, frame);
        }
        /// <summary>
        /// 添加不可反弹弹幕的模版
        /// </summary>
        public static void AddCantReflect(this Projectile proj)
        {
            if (!LAPIDSet.CantReflectProj.Contains(proj.type))
                LAPIDSet.CantReflectProj.Add(proj.type);
        }
    }
}
