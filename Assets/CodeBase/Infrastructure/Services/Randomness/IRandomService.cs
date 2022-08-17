namespace CodeBase.Infrastructure.Services.Randomness
{
    public interface IRandomService : IService
    {
        int Next(int lootMin, int lootMax);
    }
}