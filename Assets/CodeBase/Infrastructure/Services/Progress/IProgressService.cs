using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure.Services.Progress
{
    public interface IProgressService
    {
        public ProgressData ProgressData {get;}
        ProgressData LoadData();
        void CompleteLevel(bool withSuccess);
        void Init();
        void Save();
    }
}