using CodeBase.Helpers.Debug;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Progress
{
    public class ProgressService : IProgressService
    {
        private readonly ISaveLoadService<ProgressData> _saveLoadService;
        private const string ProgressDataKey = "ProgressDataKey";


        public ProgressData ProgressData { get; private set; }


        public ProgressService(ISaveLoadService<ProgressData> saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void Init()
        {
            ProgressData = TryLoadData();
        }

        public ProgressData LoadData()
        {
            return ProgressData;
        }

        public void CompleteLevel(bool withSuccess)
        {
            if(withSuccess)
                ProgressData.LevelProgressData.LevelCurrent++;

            //Save();
            WDebug.Log("Data was saved.", WType.Data);
        }

        private ProgressData TryLoadData()
        {
            if (!_saveLoadService.Load(ProgressDataKey, out var data)) 
                WDebug.Log("Saved Data has/'t founded. Created new data.",WType.Data);

            return data;
        }

        public void Save()
        {
            _saveLoadService.Save(ProgressDataKey, ProgressData);
        }
    }
}