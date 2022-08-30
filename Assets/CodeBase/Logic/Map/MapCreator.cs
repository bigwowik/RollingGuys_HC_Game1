using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Map
{
    public class MapCreator : MonoBehaviour, IMapCreator
    {
        private const string ElementKey = "End";
        private const string FloorKey = "Floor";

        [Header("Grid")]
        [SerializeField] private float Step;
        [SerializeField] private float Width = 5;
        [Header("Floor")]
        [SerializeField] private float FloorLength = 5;
        [SerializeField]private float StartFloorPosition = -10f;
        [Header("Elements")]
        [SerializeField] private MapElementsCollection MapElementsCollection;


        public float MapEndPosition { get; private set; }
        
        private IGameFactory _gameFactory;        
        private Map _map;
        private float _lastElementPositionZ;
        private IMapProvider _mapProvider;
        private IProgressService _progressService;

        [Inject]
        public void Construct(IGameFactory gameFactory, IMapProvider mapProvider, IProgressService progressService)
        {
            _progressService = progressService;
            _mapProvider = mapProvider;
            _gameFactory = gameFactory;
        }

        public void CreateMap()
        {
            //_map = GetComponent<IMapProvider>().GetMap();

            var currentLevel = _progressService.ProgressData.LevelProgressData.LevelCurrent;
            _map = _mapProvider.GetMap(currentLevel.ToString());

            ParseMapDataAndSpawn();


            MapEndPosition = _lastElementPositionZ + Step;
            SpawnEndOfMap(MapEndPosition);
            
            CreateFloorOfMap();
            
        }

        private void CreateFloorOfMap()
        {
            var prefab = MapElementsCollection.GetMapElement(FloorKey);
            
            for (float dist = StartFloorPosition; dist <= MapEndPosition; dist += FloorLength)
            {
                var pos = new Vector3(0, 0, dist);
                SpawnOneElement(prefab, pos);
            }
        }

        private void ParseMapDataAndSpawn()
        {
            for (var i = 0; i < _map.MapList.Count; i++)
            {
                for (var j = 0; j < _map.MapList[i].Length; j++)
                {
                    var mapElementKey = _map.MapList[i][j];
                    if (mapElementKey != "")
                    {
                        var prefab = MapElementsCollection.GetMapElement(mapElementKey);
                        if (prefab == null) continue;
                        
                        var offset = -Vector3.right * 0.5f * Step * (Width - 1);
                        var newPosition = new Vector3(j * Step, 0, i * Step) + offset;
                        _lastElementPositionZ = newPosition.z;
                        SpawnOneElement(prefab, newPosition);
                    }
                }
            }
            
            
        }

        private void SpawnEndOfMap(float mapEndPosition)
        {
            var pos = new Vector3(0, 0, mapEndPosition);
            var prefab = MapElementsCollection.GetMapElement(ElementKey);
            SpawnOneElement(prefab, pos);
        }

        private void SpawnOneElement(GameObject prefab, Vector3 position)
        {
            _gameFactory.InstantiateThroughDi(prefab, position);
        }

        
    }
}