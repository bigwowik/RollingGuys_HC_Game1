using System;

namespace CodeBase.Infrastructure.Services.Progress
{
    [Serializable]
    public class LevelProgressData
    {
        public int LevelCurrent;
        public LevelProgressData()
        {
            LevelCurrent = 1;
        }
    }
}