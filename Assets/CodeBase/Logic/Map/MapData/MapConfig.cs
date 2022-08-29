using System.IO;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Logic.Map.MapData
{
    [CreateAssetMenu(fileName = "MapConfig 1", menuName = "Configs/MapConfig", order = 3)]
    public class MapConfig : ScriptableObject
    {
        public string NameKey;
        
        [TextArea(10,1000)]
        public string MapData;

#if UNITY_EDITOR
        void OnValidate() {
            string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
            NameKey = Path.GetFileNameWithoutExtension(assetPath);
        }
#endif
    }
}