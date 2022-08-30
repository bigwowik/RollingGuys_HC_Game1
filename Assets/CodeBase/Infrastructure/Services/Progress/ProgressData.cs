using System;

namespace CodeBase.Infrastructure.Services.Progress
{
    [Serializable]
    public class ProgressData
    {
        public LevelProgressData LevelProgressData;
        public ResourcesData ResourcesData;

        public ProgressData()
        {
            LevelProgressData = new LevelProgressData();
            ResourcesData = new ResourcesData();
        }
    }
}