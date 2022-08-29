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

        private ProgressData TryLoadData()
        {
            if (!_saveLoadService.Load(ProgressDataKey, out var data)) 
                Debug.LogWarning("Saved Data has/'t founded. Created new data.");

            return data;
            
        }

        public ProgressData LoadData()
        {
            return ProgressData;
        }

        public void CompleteLevel(bool withSuccess)
        {
            if(withSuccess)
                ProgressData.LevelCurrent++;
            
            _saveLoadService.Save(ProgressDataKey, ProgressData);
            Debug.LogWarning("Data was saved.");
        }
    }
}