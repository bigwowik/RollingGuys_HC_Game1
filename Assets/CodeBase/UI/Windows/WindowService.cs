using CodeBase.UI.Factory;

namespace CodeBase.UI.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowsId)
        {
            switch (windowsId)
            {
                case WindowId.Pause:
                    _uiFactory.CreatePauseMenu();
                    break;
                case WindowId.SomeWindow:
                    _uiFactory.CreateSomeWindow();
                    break;
            }
        }

        
    }
}