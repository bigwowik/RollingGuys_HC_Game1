using UnityEngine;

namespace CodeBase.Logic.Map
{
    [System.Serializable]
    public class MapElement
    {
        [SerializeField]
        public string Key;
        [SerializeField]
        public GameObject Prefab;

    }
}