using LAP.Core.MiscDate;
using Microsoft.Xna.Framework;
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
        public bool Active;
        public int Type { get; internal set; }
        public Vector2 Position;
        public Vector2 Scale = Vector2.One;
        public float Scale2 = 1f;
        public Vector2 Orig = Vector2.Zero;
        public float Rotation = 0f;
        public float Opacity = 1f;
        public Color color = Color.White;
        public Rectangle Rectangle;
        public bool IsHover { get; private set; }
        public bool IntoHover { get; private set; }
        public bool PressMouseLeft { get; private set; }
        public bool PressMouseRight { get; private set; }
        public bool CanClose = false;
        public BaseUI ParentUI;
        public List<BaseUI> Subset = new();
        public virtual int UIDepth => 0;
        // 是否阻挡下层 UI 的鼠标事件（默认开启）
        public virtual bool BlockMouseInput => true;
        // 轮盘UI的数据
        public float SectorCenterRot;
        public float SectorRot;
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
            Rectangle = Rectangle.Empty;
            IsHover = false;
            IntoHover = false;
        }
        /// <summary>
        /// 更新逻辑。返回 true 表示拦截了鼠标，下层 UI 将无法被交互。
        /// </summary>
        public bool UpdateUI(bool mouseConsumed)
        {
            // 如果鼠标已经被更高层级的 UI 占用，或者全局禁用 UI
            bool canUpdate = PreUpdateHover() && !mouseConsumed && UIManager.BlockAllUI <= 0;
            // 检测碰撞
            bool isColliding = canUpdate && Colliding(Rectangle, LAPInfo.MouseRectangle);
            if (isColliding)
            {
                IsHover = true;
                if (Main.LocalPlayer is not null)
                    Main.LocalPlayer.mouseInterface = true; // 阻止玩家使用物品
            }
            else
            {
                IsHover = false;
            }
            MouseHover(IsHover);
            if (UIManager.BlockAllUI <= 0)
            {
                // 处理悬停状态切换
                if (IsHover && !IntoHover)
                {
                    StartHover();
                    IntoHover = true;
                }
                else if (!IsHover && IntoHover)
                {
                    OutHover();
                    IntoHover = false;
                }
                // 处理鼠标点击事件
                if (IsHover)
                {
                    HandleMouseInput();
                }
                else
                {
                    // 如果不在悬停状态，处理拖拽到一半移出UI的情况
                    if (PressMouseLeft)
                        PressMouseLeft = false;
                    if (PressMouseRight)
                        PressMouseRight = false;
                }
                PostUpdate(mouseConsumed);

            }
            // 告诉管理器，当前 UI 是否消耗了鼠标事件
            return isColliding && BlockMouseInput;
        }

        private void HandleMouseInput()
        {
            // 左键逻辑
            if (Main.mouseLeft && !PressMouseLeft)
            {
                OnLeftClick();
                PressMouseLeft = true;
            }
            else if (!Main.mouseLeft && PressMouseLeft)
            {
                OnMouseLeftRelease();
                PressMouseLeft = false;
            }

            if (Main.mouseLeft) MouseLeft();

            // 右键逻辑
            if (Main.mouseRight && !PressMouseRight)
            {
                OnRightClick();
                PressMouseRight = true;
            }
            else if (!Main.mouseRight && PressMouseRight)
            {
                OnMouseRightRelease();
                PressMouseRight = false;
            }

            if (Main.mouseRight)
                MouseRight();
        }
        public virtual bool PreUpdateHover() => true;
        public virtual bool Colliding(Rectangle rectangle, Rectangle mouseRectangle) => rectangle.Contains(Main.MouseScreen.ToPoint());
        public virtual void MouseHover(bool isHover) { }
        public virtual void StartHover() { }
        public virtual void OutHover() { }
        public virtual void PostUpdate(bool mouseConsumed) { }
        public virtual void OnLeftClick() { }
        public virtual void MouseLeft() { }
        public virtual void OnRightClick() { }
        public virtual void MouseRight() { }
        public virtual void OnMouseLeftRelease() { }
        public virtual void OnMouseRightRelease() { }
        public virtual void OnActive() { }
        public virtual bool PreDeActive() => true;
        public virtual void Draw() { }
    }
}
