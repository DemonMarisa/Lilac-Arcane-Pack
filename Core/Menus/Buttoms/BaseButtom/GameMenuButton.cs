using LAP.Assets.Fonts;
using LAP.Assets.Menus;
using LAP.Core.Menus.MenuUtilities;
using LAP.Core.UISystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI.Chat;

namespace LAP.Core.Menus.Buttoms.BaseButtom
{
    public class GameMenuButton : BaseUI
    {
        public virtual string Text => "开始游戏";
        public virtual Vector2 Center => Vector2.Zero;
        public virtual int TargetMenuID => MenuID.CharacterSelect;
        public virtual void PPUpdate()
        {

        }
        public float EdgeOpacity;
        public float XEdgeScale;
        public override void SetDefaults()
        {
            Position = Center;
            Rectangle = Utils.CenteredRectangle(Position, new Vector2(180, 70));
            XEdgeScale = 1f;
        }
        public override void StartHover()
        {
            SoundEngine.PlaySound(MenuSounds.Hover);
        }
        public override void PostUpdate()
        {
            Position = Center;
            Rectangle = Utils.CenteredRectangle(Position, new Vector2(320, 70));
            PPUpdate();
        }
        public override void MouseHover(bool isHover)
        {
            if (isHover)
            {
                EdgeOpacity = MathHelper.Lerp(EdgeOpacity, 1f, 0.2f);
                if (Main.mouseLeft)
                    XEdgeScale = MathHelper.Lerp(XEdgeScale, 0.95f, 0.2f);
                else
                    XEdgeScale = MathHelper.Lerp(XEdgeScale, 1.1f, 0.2f);
            }
            else
            {
                EdgeOpacity = MathHelper.Lerp(EdgeOpacity, 0f, 0.2f);
                XEdgeScale = MathHelper.Lerp(XEdgeScale, 1f, 0.2f);
            }
        }
        public override void OnLeftClick()
        {
            SoundEngine.PlaySound(MenuSounds.Click);
        }
        public override void MouseLeft()
        {
        }
        public override void OnMouseLeftRelease()
        {
            UIUtilities.ChangeMenu(TargetMenuID);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            UIDrawUtilities.DrawEdge(spriteBatch, Center, EdgeOpacity * Opacity, XEdgeScale);
            DynamicSpriteFont font = LAPFontsRegister.Combat_Crit_Lilies.Value;
            Vector2 Size = ChatManager.GetStringSize(LAPFontsRegister.Combat_Crit_Lilies.Value, Text, Vector2.One);
            ChatManager.DrawColorCodedString(spriteBatch, font, Text, Center, Color.White * Opacity, 0, Size / 2, Vector2.One);
            ChatManager.DrawColorCodedString(spriteBatch, font, Text, Center, Color.White * Opacity, 0, Size / 2, Vector2.One);
        }
    }
}
