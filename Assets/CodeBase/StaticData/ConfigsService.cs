using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.Friends;
using CodeBase.Logic.Map;
using CodeBase.Logic.Player;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class ConfigsService : IConfigsService
    {
        private const string HeroConfigPath = "Configs/HeroConfig";
        private const string FriendConfigPath = "Configs/FriendConfig";

        private Dictionary<string,Map> _levels;

        public HeroConfig HeroConfig { get; private set; }
        public FriendConfig FriendConfig{ get; private set; }

        public void LoadData()
        {
            HeroConfig = Resources.Load<HeroConfig>(HeroConfigPath);
            FriendConfig = Resources.Load<FriendConfig>(FriendConfigPath);

        }

    }
}