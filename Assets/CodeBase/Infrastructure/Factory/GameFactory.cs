
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Services.Randomness;
using CodeBase.Logic.Map;
using CodeBase.Logic.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        //private readonly IAssets _assets;
        //private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly DiContainer _diContainer;

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

            
            var heroView = PlayerGameObject.GetComponentInChildren<HeroView>();
            _diContainer.Bind<HeroView>().FromInstance(heroView);

            
            _diContainer.Bind<HeroModel>().AsSingle();
            
            _diContainer.Bind<HeroPresenter>().AsSingle().NonLazy();


            return PlayerGameObject;
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
            var instance = _diContainer.InstantiatePrefab(prefab);
            instance.transform.position = at;
            
            return instance;
        }
        
    }
}