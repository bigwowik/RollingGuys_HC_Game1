using CodeBase.Helpers.Debug;
using CodeBase.Infrastructure.Services.Progress;

namespace CodeBase.Infrastructure.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        private readonly IProgressService _progressService;

        public SettingsService(IProgressService progressService)
        {
            _progressService = progressService;
        }

        public void SetSoundsEnable(bool value)
        {
            _progressService.ProgressData.SettingsData.IsSoundsEnable = value;
            //enable sounds
            WDebug.Log($"Set sounds to {value}.", WType.UI);

        }

        public void SetVibroEnable(bool value)
        {
            _progressService.ProgressData.SettingsData.IsVibroEnable = value;
            //enable vibro
            WDebug.Log($"Set vibro to {value}.", WType.UI);
        }
    }
}