using LAP.Core.AnimationHandle;
using LAP.Core.MiscDate;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.UISystem
{
    /// <summary>
    /// 必须分配深度
    /// UI是否允许使用除了过对应函数外，还需要没有更高一级的深度的UI
    /// 比如一个1级UI，当有2级UI时，1级UI就不会起效了
    /// </summary>
    public abstract class BaseUI : ModType
    {
        public AnimationHelper AniProgress = new AnimationHelper(5);
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
        /// <summary>
        /// 必须分配一个深度
        /// </summary>
        public virtual int UIDepth => 1;
        protected sealed override void Register()
        {
            Type = UIManager.UICollection.Count;
            if (!UIManager.UICollection.Contains(this))
                UIManager.UICollection.Add(this);

            SetDefaults();
        }
        public virtual void SetDefaults()
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

            bool CanUpdate = PreUpdateHover() && !UIManager.ActiveDepth[UIDepth + 1];
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

            UIManager.TopUI = Type;
        }
        /// <summary>
        /// 是否更新悬停效果，true为更新，false为完全不更新，null为常驻按照不悬停的模式更新
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
