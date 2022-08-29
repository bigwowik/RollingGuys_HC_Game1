using System.Collections.Generic;

namespace CodeBase.Logic.Map
{
    public class Map
    {
        public List<string[]> MapList = new List<string[]>();

        public void AddLine(string[] line) => 
            MapList.Add(line);
    }
}