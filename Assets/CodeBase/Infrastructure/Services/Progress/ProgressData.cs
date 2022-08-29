using System;

namespace CodeBase.Infrastructure.Services.Progress
{
    [Serializable]
    public class ProgressData
    {
        public ProgressData()
        {
            LevelCurrent = 1;
        }

        public int LevelCurrent;
    }
}