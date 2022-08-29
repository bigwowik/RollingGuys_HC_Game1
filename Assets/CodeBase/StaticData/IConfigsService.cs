using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic.Friends;
using CodeBase.Logic.Map;
using CodeBase.Logic.Map.MapData;
using CodeBase.Logic.Player;

namespace CodeBase.StaticData
{
    public interface IConfigsService : IService
    {
        HeroConfig HeroConfig { get; }
        FriendConfig FriendConfig { get; }
        List<MapConfig> MapsConfigs { get; }
        void LoadData();
    }
}