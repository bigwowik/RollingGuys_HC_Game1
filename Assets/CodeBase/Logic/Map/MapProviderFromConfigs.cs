using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeBase.Helpers.Debug;
using CodeBase.Logic.Map.MapData;
using CodeBase.StaticData;

namespace CodeBase.Logic.Map
{
    public class MapProviderFromConfigs : IMapProvider
    {
        private List<MapConfig> _mapConfigs;

        private IConfigsService _configsService;

        public MapProviderFromConfigs(IConfigsService configsService)
        {
            _configsService = configsService;
        }

        public Map GetMap(string currentLevel)
        {
            _mapConfigs = _configsService.MapsConfigs;
            
            var currentMap = _mapConfigs.Find(z => z.NameKey == currentLevel);
            if (currentMap == null)
            {
                WDebug.Log($"Cant found level with name {currentLevel}", WType.Logic);
                return null;
            }
            
            
            var newMap = new Map();
            
            using (StringReader reader = new StringReader(currentMap.MapData))
                while (true)
                {
                    var line = reader.ReadLine();
                    if (line == null) break;

                    var elements = line.Split(' ');
                    newMap.AddLine(elements);
                }

            return newMap;
        }
    }
}