using LAP.Assets.Fonts;
using LAP.Assets.Menus;
using LAP.Core.Menus;
using LAP.Core.UISystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace MenuMod.Core.Menu.Buttoms.Depth_1
{
    public class SwitchModMenu : BaseUI
    {
        public string Text;
        public override void PostUpdate()
        {
            ModMenu currentMenu = MenuLoader.CurrentMenu;
            int newMenus;
            lock (MenuLoader.menus)
            {
                string[] knownMenus = MenuLoader.KnownMenus;
                foreach (ModMenu menu in MenuLoader.menus)
                {
                    menu.IsNew = menu.IsAvailable && !knownMenus.Contains(menu.FullName);
                }
                newMenus = MenuLoader.menus.Count((ModMenu m) => m.IsNew);
            }
            Position = new Vector2(Main.screenWidth / 2, Main.screenHeight - 20);
            DynamicSpriteFont font = LAPFontsRegister.Mouse_Text_Lilies.Value;
            Text = $"{Language.GetTextValue("tModLoader.ModMenuSwap")}: {currentMenu.DisplayName}{(newMenus == 0 ? "" : ModLoader.notifyNewMainMenuThemes ? $" ({newMenus} New)" : "")}";
            Vector2 size = ChatManager.GetStringSize(font, ChatManager.ParseMessage(Text, color).ToArray(), Vector2.One);
            Rectangle = Utils.CenteredRectangle(Position, size);
        }
        public override void MouseHover(bool isHover)
        {
            if (isHover)
            {
                if (Main.mouseLeft || Main.mouseRight)
                    Scale2 = MathHelper.Lerp(Scale2, 0.95f, 0.2f);
                else
                    Scale2 = MathHelper.Lerp(Scale2, 1.05f, 0.2f);
            }
            else
            {
                Scale2 = MathHelper.Lerp(Scale2, 1f, 0.2f);
            }
        }
        public override void StartHover()
        {
            SoundEngine.PlaySound(MenuSounds.Hover);
        }
        public override void OnMouseLeftRelease()
        {
            if (EnderMenus.CanOut)
                MenuLoader.OffsetModMenu(1);
        }
        public override void OnMouseRightRelease()
        {
            if (EnderMenus.CanOut)
                MenuLoader.OffsetModMenu(-1);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            DynamicSpriteFont font = LAPFontsRegister.Mouse_Text_Lilies.Value;
            Vector2 size = ChatManager.GetStringSize(font, Text, Vector2.One);
            ChatManager.DrawColorCodedString(spriteBatch, font, Text, Position, Color.Silver, 0, size / 2, Vector2.One * Scale2);
        }
    }
}
