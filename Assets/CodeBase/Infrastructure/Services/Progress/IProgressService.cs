using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure.Services.Progress
{
    public interface IProgressService
    {
        public ProgressData ProgressData {get;}
        ProgressData LoadData();
        void EndLevel(bool withSuccess);
        void IncreaseLevelValue();
    }

    class ProgressService : IProgressService
    {
        public ProgressData ProgressData { get; }
        public ProgressData LoadData()
        {
            return new ProgressData();
            //throw new System.NotImplementedException();
        }

        public void EndLevel(bool withSuccess)
        {
            //throw new System.NotImplementedException();
        }

        public void IncreaseLevelValue()
        {
            ProgressData.LevelCurrent++;
        }
    }
}