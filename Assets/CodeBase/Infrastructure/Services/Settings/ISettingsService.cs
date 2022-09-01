namespace CodeBase.Infrastructure.Services.Settings
{
    public interface ISettingsService
    {
        public void SetSoundsEnable(bool enable);
        public void SetVibroEnable(bool enable);
    }
}