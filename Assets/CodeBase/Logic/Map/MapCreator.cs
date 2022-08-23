using CodeBase.Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Map
{
    public interface IMapCreator
    {
        void CreateMap();
        float MapEndPosition { get; }
    }

    public class MapCreator : MonoBehaviour, IMapCreator
    {
        private const string ElementKey = "End";
        [SerializeField] private float Step;
        [SerializeField] private float Width = 5;
        private Map _map;
        
        [SerializeField] private MapElementsCollection MapElementsCollection;

        public GameObject[] Elements;
        private IGameFactory _gameFactory;

        private float _lastZElementPosition;

        public float MapEndPosition { get; private set; }


        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            //from cofings service

            
        }

        public void CreateMap()
        {
            _map = GetComponent<IMapProvider>().GetMap();


            for (var i = 0; i < _map.MapList.Count; i++)
            {
                for (var j = 0; j < _map.MapList[i].Length; j++)
                {
                    var mapElementKey = _map.MapList[i][j];
                    if (mapElementKey != "")
                    {
                        var prefab = MapElementsCollection.GetMapElement(mapElementKey);
                        if(prefab == null) continue;
                        var offset = -Vector3.right * 0.5f * Step * (Width - 1);
                        var newPosition = new Vector3(j * Step, 0, i * Step) + offset;
                        _lastZElementPosition = newPosition.z;
                        SpawnOneElement(prefab, newPosition);
                    }
                }
            }

            MapEndPosition = _lastZElementPosition + Step;
            SpawnEndOfMap(MapEndPosition);
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