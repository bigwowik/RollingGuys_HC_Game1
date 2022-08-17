using CodeBase.Infrastructure.Services;

namespace CodeBase.UI.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowsId);
    }
}