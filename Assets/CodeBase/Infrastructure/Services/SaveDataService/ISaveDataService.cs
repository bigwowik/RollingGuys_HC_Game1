using CodeBase.Infrastructure.Services.Progress;

namespace CodeBase.Infrastructure.States
{
    public interface ISaveDataService
    {
        void SaveData(ProgressData progressData);
        ProgressData LoadData();
    }
}