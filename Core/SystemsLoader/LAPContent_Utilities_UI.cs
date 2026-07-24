using LAP.Core.UISystem;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        public static void ActiveUI(int Type)
        {
            if (!UIManager.ActiveUIs.Contains(UIManager.UICollection[Type]))
            {
                BaseUI ui = UIManager.UICollection[Type];
                UIManager.ActiveUIs.Add(ui);
                ui.Active = true;
                ui.OnActive();
            }
        }
        public static void ActiveUI<T>() where T : BaseUI
        {
            if (!UIManager.ActiveUIs.Contains(GetInstance<T>()))
            {
                BaseUI ui = UIManager.UICollection[GetInstance<T>().Type];
                UIManager.ActiveUIs.Add(ui);
                ui.Active = true;
                ui.OnActive();
            }
        }
        public static void DeActive(int Type)
        {
            if (UIManager.ActiveUIs.Contains(UIManager.UICollection[Type]))
            {
                BaseUI ui = UIManager.UICollection[Type];
                if (ui.PreDeActive())
                    UIManager.ActiveUIs.RemoveAll(ui => ui.Type == Type);
            }
        }
        public static void DeActive<T>() where T : BaseUI
        {
            if (UIManager.ActiveUIs.Contains(GetInstance<T>()))
            {
                if (GetInstance<T>().PreDeActive())
                    UIManager.ActiveUIs.RemoveAll(particle => particle == GetInstance<T>());
            }
        }
        public static BaseUI GetUI<T>() where T : BaseUI
        {
            return UIManager.UICollection[GetInstance<T>().Type];
        }
        public static BaseUI GetUI(int type)
        {
            return UIManager.UICollection[type];
        }
    }
}
