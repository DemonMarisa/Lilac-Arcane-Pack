using LAP.Core.UISystem;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        public static void ActiveUI(int Type)
        {
            if (!UIManager.ActiveUI.Contains(Type))
            {
                BaseUI ui = UIManager.UICollection[Type];
                UIManager.ActiveUI.Add(Type);
                ui.Active = true;
                ui.OnActive();
            }
        }
        public static void ActiveUI<T>() where T : BaseUI
        {
            if (!UIManager.ActiveUI.Contains(GetInstance<T>().Type))
            {
                BaseUI ui = UIManager.UICollection[GetInstance<T>().Type];
                UIManager.ActiveUI.Add(ui.Type);
                ui.Active = true;
                ui.OnActive();
            }
        }
        public static void DeActive(int Type)
        {
            if (UIManager.ActiveUI.Contains(Type))
            {
                BaseUI ui = UIManager.UICollection[Type];
                if (ui.PreDeActive())
                    UIManager.ActiveUI.RemoveAll(particle => particle == ui.Type);
            }
        }
        public static void DeActive<T>() where T : BaseUI
        {
            if (UIManager.ActiveUI.Contains(GetInstance<T>().Type))
            {
                if (GetInstance<T>().PreDeActive())
                    UIManager.ActiveUI.RemoveAll(particle => particle == GetInstance<T>().Type);
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
