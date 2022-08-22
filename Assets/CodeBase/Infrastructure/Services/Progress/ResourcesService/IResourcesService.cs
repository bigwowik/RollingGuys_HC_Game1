namespace CodeBase.Infrastructure.States
{
    public interface IResourcesService
    {
        void AddResources(ResourceType resourceType, int value);
    }
}