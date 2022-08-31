using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.UI.Windows.EndLevelWindow;

namespace CodeBase.UI.Factory
{
    public interface IUIFactory : IService
    {
        void CreateSomeWindow();
        void CreateUIRoot();
        void CreatePauseMenu();
        void CreateStartMenu();
        EndLevelWindowPresenter CreateEndLevelWindow(LevelResult levelResult);
    }
}