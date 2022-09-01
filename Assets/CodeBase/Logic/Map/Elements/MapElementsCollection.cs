using System.Collections.Generic;
using System.Linq;
using CodeBase.Helpers.Debug;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Logic.Map
{
    [CreateAssetMenu(fileName = "MapElementsCollection", menuName = "Configs/MapElementsCollection", order = 1)]
    public class MapElementsCollection : ScriptableObject
    {

        public List<MapElement> Elements = new List<MapElement>();

        

        public GameObject GetMapElement(string elementKey)
        {
            var element = Elements.Find(z => z.Key == elementKey);

            if (element != null)
                return element.Prefab;
            else
            {
                WDebug.Log($"Map element with key: {elementKey} has not found", WType.Logic);
                return null;
            }
        }
    }
}