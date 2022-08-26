
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

        //private readonly IAssets _assets;
        //private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly DiContainer _diContainer;
        private FriendPresenter.Factory _friendsFactory;

        //cashed references
        public GameObject Hud { get; private set; }
        public GameObject PlayerGameObject { get; set; }


        public GameFactory(IRandomService randomService, DiContainer diContainer)
        {
            //_assets = assets;
            //_staticData = staticData;
            _randomService = randomService;
            _diContainer = diContainer;

            
        }

        public void PrepareFriends()
        {
            _diContainer
                .Bind<ICharacterModel>()
                .To<FriendModel>()
                .WhenInjectedInto<FriendPresenter>();

            _diContainer
                .BindFactory<CharacterView, FriendPresenter, FriendPresenter.Factory>();
            
            var config = Resources.Load<FriendConfig>("Configs/FriendConfig");
            _diContainer.Bind<FriendConfig>().FromInstance(config);

            _friendsFactory = _diContainer.Resolve<FriendPresenter.Factory>();
            
        }

        public IMapCreator CreateMapCreator()
        {
            var prefab = Resources.Load<GameObject>(AssetPaths.MapCreatorPath);

            var instance = _diContainer.InstantiatePrefab(prefab).GetComponent<MapCreator>();

            _diContainer.Bind<IMapCreator>().FromInstance(instance);
            
            return (IMapCreator)instance;
        }

        public GameObject CreatePlayerCamera()
        {
            var prefab = Resources.Load<GameObject>(AssetPaths.PlayerCameraPath);
            var instance = _diContainer.InstantiatePrefab(prefab);
            instance.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>().Follow = PlayerGameObject.transform;
            return instance;
        }

        public GameObject CreateHero(Vector3 at)
        {
            var heroPrefab = Resources.Load<GameObject>(AssetPaths.PlayerPrefabPath);
            
            var heroConfig = Resources.Load<HeroConfig>("Configs/HeroConfig");
            _diContainer.Bind<HeroConfig>().FromInstance(heroConfig);

            PlayerGameObject = _diContainer.InstantiatePrefab(heroPrefab);
            PlayerGameObject.transform.position = at;
            
            var heroView = PlayerGameObject.GetComponentInChildren<CharacterView>();
      
            _diContainer
                .Bind<ICharacterModel>()
                .To<HeroModel>()
                .AsSingle()
                .WhenInjectedInto<HeroPresenter>();

            _diContainer
                .Bind<CharacterView>()
                .FromInstance(heroView)
                .AsSingle()
                .WhenInjectedInto<HeroPresenter>();

            
                
            
            _diContainer.Bind<HeroPresenter>().AsSingle().NonLazy();
            _diContainer.Resolve<HeroPresenter>().Start();

            return PlayerGameObject;
        }

        public GameObject CreateFriend(GameObject prefab, Vector3 at)
        {
            
        
            var instance = _diContainer.InstantiatePrefab(prefab);
            instance.transform.position = at;
            
            var view = instance.GetComponentInChildren<CharacterView>();

            //_diContainer.Bind<FriendMovement>().FromComponentInNewPrefab(prefab);
            
            // _diContainer
            //     .Bind<CharacterView>()
            //     .FromInstance(view)                
            //     .WhenInjectedInto<FriendPresenter>();


            var friendPresenter = _friendsFactory.Create(view);
            friendPresenter.Start();
            
            // _diContainer
            //     .Bind<FriendPresenter>()
            //     .WithArguments(view)
            //     .NonLazy();
            
            
            
            return instance;
        }

        public GameObject CreateHud()
        {
            var hudPrefab = Resources.Load<GameObject>(AssetPaths.HudPath);
            Hud = InstantiateObject(hudPrefab);
            

            return Hud;
        }

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
    }
}