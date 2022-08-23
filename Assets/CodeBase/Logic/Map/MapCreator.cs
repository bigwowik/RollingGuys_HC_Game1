using CodeBase.Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Map
{
    public interface IMapCreator
    {
        void CreateMap();
    }

    public class MapCreator : MonoBehaviour, IMapCreator
    {
        [SerializeField] private float Step;
        [SerializeField] private float Width = 5;
        private Map _map;
        private MapElementsCollection _mapElementsCollectionCollection;

        public GameObject[] Elements;
        private IGameFactory _gameFactory;


        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            //from cofings service

            
        }

        public void CreateMap()
        {
            CreateMapElements();
            
            _map = GetComponent<IMapProvider>().GetMap();


            for (var i = 0; i < _map.MapList.Count; i++)
            {
                for (var j = 0; j < _map.MapList[i].Length; j++)
                {
                    var mapElementKey = _map.MapList[i][j];
                    if (mapElementKey != "")
                    {
                        var prefab = _mapElementsCollectionCollection.GetMapElement(mapElementKey);
                        if(prefab == null) continue;
                        var offset = -Vector3.right * 0.5f * Step * (Width - 1);
                        var newPosition = new Vector3(j * Step, 0, i * Step) + offset;
                        SpawnOneElement(prefab, newPosition);
                    }
                }
            }
        }

        private void SpawnOneElement(GameObject prefab, Vector3 position)
        {
            _gameFactory.InstantiateThroughDi(prefab, position);
        }

        private void CreateMapElements()
        {
            _mapElementsCollectionCollection = new MapElementsCollection();
            _mapElementsCollectionCollection.AddMapElement("1", Elements[0]);
            _mapElementsCollectionCollection.AddMapElement("2", Elements[1]);
        }
    }
}