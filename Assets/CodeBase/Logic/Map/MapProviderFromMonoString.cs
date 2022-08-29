using System.IO;
using UnityEngine;

namespace CodeBase.Logic.Map
{
    public class MapProviderFromMonoString : MonoBehaviour, IMapProvider
    {
        [TextArea(10,1000)] public string MapString;


        public Map GetMap()
        {
            var newMap = new Map();
            using (StringReader reader = new StringReader(MapString))
            {
                while (true)
                {
                   
                    var line = reader.ReadLine();
                    if(line == null) break;
                    
                    var elements = line.Split(' ');
                    newMap.AddLine(elements);
                    
                    
                }
            }

            return newMap;
        }

        public Map GetMap(string currentLevel)
        {
            throw new System.NotImplementedException();
        }
    }
}