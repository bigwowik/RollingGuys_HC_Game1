namespace CodeBase.Infrastructure.Services.Randomness
{
    internal class UnityRandomService : IRandomService
    {
        public int Next(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}