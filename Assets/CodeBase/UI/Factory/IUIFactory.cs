using CodeBase.Infrastructure.Services;

namespace CodeBase.UI.Factory
{
    public interface IUIFactory : IService
    {
        void CreateSomeWindow();
        void CreateUIRoot();
        void CreatePauseMenu();
    }
}