using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloonsProject.Models.Maps
{
    public class MapManager
    {
        public List<Map> Maps { get; }

        public MapManager()
        {
            Map easyMap = new EasyMap();
            Map mediumMap = new MediumMap();
            Maps = new List<Map>() { easyMap, mediumMap };
        }

        public Map GetMapByName(string mapName)
        {
            return Maps.FirstOrDefault(map => map.Name == mapName);
        }
    }
}