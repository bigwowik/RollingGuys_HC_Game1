public interface IMediator
{
    void DisableMainMenuButtons();
    void SetLevelText(string text);
    void SetCoinsText(string text);
    void EnableSettings(bool enable);
    void EnableMainMenu(bool enable);
}