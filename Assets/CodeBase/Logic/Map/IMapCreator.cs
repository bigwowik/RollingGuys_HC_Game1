namespace CodeBase.Logic.Map
{
    public interface IMapCreator
    {
        void CreateMap();
        float MapEndPosition { get; }
    }
}