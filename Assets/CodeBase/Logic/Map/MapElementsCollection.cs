using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic.Map
{
    public class MapElementsCollection
    {
        private Dictionary<string, GameObject> Elements = new Dictionary<string, GameObject>();

        
        public void AddMapElement(string elementKey, GameObject element) => 
            Elements[elementKey] = element;

        public GameObject GetMapElement(string elementKey)
        {
            if(Elements.TryGetValue(elementKey, out var element))
                return element;
            else
            {
                Debug.LogError($"Map element with key: {elementKey} has not found");
                return null;
            }
        }
    }
}