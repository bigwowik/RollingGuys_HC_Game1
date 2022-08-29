namespace CodeBase.Infrastructure.Services.Progress
{
    public interface ISaveLoadService<T> where T : new()
    {
        void Save(string key, T data);
        bool Load(string key, out T data);
    }
}