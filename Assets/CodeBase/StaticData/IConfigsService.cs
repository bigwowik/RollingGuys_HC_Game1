using CodeBase.Infrastructure.Services;
using CodeBase.Logic.Friends;
using CodeBase.Logic.Player;

namespace CodeBase.StaticData
{
    public interface IConfigsService : IService
    {
        HeroConfig HeroConfig { get; }
        FriendConfig FriendConfig { get; }
        void LoadData();
    }
}