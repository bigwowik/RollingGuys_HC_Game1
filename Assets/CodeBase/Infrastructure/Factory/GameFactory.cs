
using Cinemachine;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Services.Randomness;
using CodeBase.Infrastructure.States;
using CodeBase.Logic.Friends;
using CodeBase.Logic.Map;
using CodeBase.Logic.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string FriendIdentifier = "Friend";
        private const string HeroIdentifier = "Hero";
        private const string FriendConfigPath = "Configs/FriendConfig";
        private const string HeroConfigPath = "Configs/HeroConfig";

        //private readonly IAssets _assets;
        //private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly DiContainer _diContainer;
        private readonly ILevelProgressService _levelProgressService;
        private GameObject _heroPrefab;

        //cashed references
        public GameObject Hud { get; private set; }
        public GameObject PlayerGameObject { get; set; }


        public GameFactory(IRandomService randomService,  DiContainer diContainer, ILevelProgressService levelProgressService)
        {
            //_assets = assets;
            //_staticData = staticData;
            _randomService = randomService;
            _diContainer = diContainer;
            _levelProgressService = levelProgressService;
        }

        public void PrepareFactory()
        {
            _diContainer.Bind<FriendConfig>().FromResource(FriendConfigPath).AsSingle();
            _diContainer.Bind<HeroConfig>().FromResource(HeroConfigPath).AsSingle();
            
            _heroPrefab = Resources.Load<GameObject>(AssetPaths.PlayerPrefabPath);
            
        }

        #region Hero

        public GameObject CreateHero(Vector3 at)
        {
            PlayerGameObject = _diContainer.InstantiatePrefab(_heroPrefab);
            PlayerGameObject.transform.position = at;

            //set
            _levelProgressService.ActivePlayer = PlayerGameObject.transform;

            return PlayerGameObject;
        }

        public GameObject CreatePlayerCamera()
        {
            var prefab = Resources.Load<GameObject>(AssetPaths.PlayerCameraPath);
            var instance = _diContainer.InstantiatePrefab(prefab);
            var camera = instance.GetComponentInChildren<CinemachineVirtualCamera>();
            camera.Follow = PlayerGameObject.transform;

            _levelProgressService.PlayerCamera = camera;
            return instance;
        }

        #endregion

        #region Friends

        public GameObject CreateFriend(GameObject prefab, Vector3 at)
        {
            var instance = _diContainer.InstantiatePrefab(prefab);
            instance.transform.position = at;
            return instance;
        }

        #endregion

        #region Map
        public IMapCreator CreateMapCreator()
        {
            var prefab = Resources.Load<GameObject>(AssetPaths.MapCreatorPath);

            var instance = _diContainer.InstantiatePrefab(prefab).GetComponent<MapCreator>();

            _diContainer.Bind<IMapCreator>().FromInstance(instance);
            
            return (IMapCreator)instance;
        }
    
        #endregion

        #region UI and HUD
        public GameObject CreateHud()
        {
            var hudPrefab = Resources.Load<GameObject>(AssetPaths.HudPath);
            Hud = InstantiateObject(hudPrefab);
            return Hud;
        }
        #endregion

        #region Instantiate

        private GameObject InstantiateObject(GameObject prefab, Vector3 position)
        {
            GameObject gameObject = GameObject.Instantiate(prefab, position, Quaternion.identity);
            return gameObject;
        }

        private GameObject InstantiateObject(GameObject prefab)
        {
            return InstantiateObject(prefab, Vector3.zero);
        }
        
        public GameObject InstantiateThroughDi(GameObject prefab, Vector3 at)
        {
            GameObject instance = null;
            if (prefab.GetComponent<IFriend>() != null)                                //TEMPERARY
                instance = CreateFriend(prefab, at);
            else
            {
                instance = _diContainer.InstantiatePrefab(prefab);
                instance.transform.position = at;
            }

            return instance;
        }
        #endregion
        
        public void CreateDialogUI()
        {
            var prefab = Resources.Load<GameObject>(AssetPaths.DialogUIPath);
            var dialogUI = InstantiateObject(prefab);
        }

        public void CreateEnemySpawner(Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(AssetPaths.EnemySpawner);
            var enemySpawner = InstantiateObject(prefab, at);
        }

        public void CleanUp()
        {
            //_assets.CleanUp();
        }


        
    }
}