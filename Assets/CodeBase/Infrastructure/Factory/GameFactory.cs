﻿
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Services.Randomness;
using CodeBase.InteractableObjects;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        public GameObject Hud { get; private set; }

        public GameObject PlayerGameObject { get; set; }

        public GameFactory(IAssets assets, IStaticDataService staticData, IRandomService randomService)
        {
            _assets = assets;
            _staticData = staticData;
            _randomService = randomService;
        }

        public GameObject CreateHero(Vector3 at)
        {
            var heroPrefab = Resources.Load<GameObject>(AssetPaths.PlayerPrefabPath);
            PlayerGameObject = InstantiateObject(heroPrefab, at);

            
            
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
        
    }
}