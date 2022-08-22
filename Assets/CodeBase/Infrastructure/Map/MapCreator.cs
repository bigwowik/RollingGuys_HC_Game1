using CodeBase.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Map
{
    public class MapCreator : MonoBehaviour, IMapCreator
    {
        private MapElement _mapElement;

        [Inject]
        public void Construct(MapElement mapElement)
        {
            
            _mapElement = mapElement;
        }
        
        public void CreateMap()
        {
            Debug.Log("Map element:" + _mapElement.name);
        }
    }
}