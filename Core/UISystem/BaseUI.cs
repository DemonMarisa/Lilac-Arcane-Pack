using LAP.Core.AnimationHandle;
using LAP.Core.MiscDate;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.UISystem
{
    /// <summary>
    /// 这一套只适合用于面板级的UI，不是用来做小组件的，虽然你完全可以用它来做小组件，但它的设计初衷是面板级UI，所以它有一个重要的设计就是层级关系
    /// 比如一个1级UI，当有2级UI时，1级UI就不会起效了
    /// </summary>
    public abstract class BaseUI : ModType
    {
        public AniHelper AniProgress;
        public bool Active;
        public int FadeProgress;
        public int MaxFadeProgress;
        public int Type;
        public Vector2 Position;
        public Vector2 Scale;
        public float Scale2;
        public Vector2 Orig;
        public float Rotation;
        public float Opacity = 1f;
        public Color color;
        public Rectangle Rectangle;
        public bool IsHover;
        public bool IntoHover;
        public bool PressMouseLeft;
        public bool PressMouseRight;
        public bool CanClose = false;
        public int Parent;
        public List<int> Subset = [];
        // 这两个属性会在你绑定好后自动设置，用于扇形UI的判定
        // 扇区中心
        public float SectorCenterRot;
        // 一个扇形的角度
        public float SectorRot;
        /// <summary>
        /// 必须分配一个深度
        /// </summary>
        public virtual int UIDepth => 0;
        protected sealed override void Register()
        {
            Type = UIManager.UICollection.Count;
            if (!UIManager.UICollection.Contains(this))
                UIManager.UICollection.Add(this);
        }
        public virtual void PostSetUpContent()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Scale2 = 1f;
            Orig = Vector2.Zero;
            Rotation = 0f;
            color = Color.White;
            Rectangle = new Rectangle(0, 0, 0, 0);
            IsHover = false;
        }
        public void Update()
        {
            if (PreSetDepth())
                UIManager.ActiveDepthCount[UIDepth] = 2;

            IsHover = Colliding(Rectangle, LAPInfo.MouseRectangle);

            bool CanUpdate = PreUpdateHover() && !UIManager.ActiveDepth[UIDepth + 1] && UIManager.BlockAllUI == 0;

            if (!CanUpdate)
            {
                IsHover = false;
                IntoHover = false;
            }

            MouseHover(IsHover);

            if (IsHover && !IntoHover)
            {
                StartHover();
                IntoHover = true;
            }
            if (!IsHover && IntoHover)
            {
                OutHover();
                IntoHover = false;
            }

            if (IsHover)
            {
                Main.LocalPlayer.mouseInterface = true; // 阻止玩家使用物品
                if (Main.mouseLeft && !PressMouseLeft)
                {
                    OnLeftClick();
                    PressMouseLeft = true;
                }
                if (!Main.mouseLeft && PressMouseLeft)
                {
                    OnMouseLeftRelease();
                    PressMouseLeft = false;
                }
                if (Main.mouseLeft)
                    MouseLeft();

                if (Main.mouseRight && !PressMouseRight)
                {
                    OnRightClick();
                    PressMouseRight = true;
                }
                if (!Main.mouseRight && PressMouseRight)
                {
                    OnMouseRightRelease();
                    PressMouseRight = false;
                }
                if (Main.mouseRight)
                    MouseRight();
            }
            else
            {
                PressMouseLeft = false;
                PressMouseRight = false;
            }

            PostUpdate();
        }
        /// <summary>
        /// 是否更新悬停效果，true为更新，false为完全不更新
        /// </summary>
        /// <returns></returns>
        public virtual bool PreUpdateHover()
        {
            return true;
        }
        public virtual bool Colliding(Rectangle rectangle, Rectangle mouseRectangle)
        {
            return rectangle.Contains(Main.MouseScreen.ToPoint());
        }
        /// <summary>
        /// 检测鼠标碰撞
        /// </summary>
        public virtual void MouseHover(bool isHover)
        {
        }
        public virtual void StartHover()
        {
        }
        public virtual void OutHover()
        {
        }
        /// <summary>
        /// 常驻更新
        /// </summary>
        public virtual void PostUpdate()
        {
        }
        public virtual void OnLeftClick()
        {
        }
        public virtual void MouseLeft()
        {
        }
        public virtual void OnRightClick()
        {
        }
        public virtual void MouseRight()
        {
        }
        public virtual void OnMouseLeftRelease()
        {
        }
        public virtual void OnMouseRightRelease()
        {
        }
        public virtual void OnActive()
        {

        }
        public virtual bool PreDeActive()
        {
            return true;
        }
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
        public virtual bool PreSetDepth()
        {
            return true;
        }
    }
}
