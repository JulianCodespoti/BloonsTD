using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BloonsProject
{
    public static class MapManager
    {
        public static List<Map> GetAllMaps()
        {
            var listOfMaps = new List<Map>();
            string mapJsonPath = "./Maps/MapJsons";
            DirectoryInfo directoryInfo = new DirectoryInfo(mapJsonPath);

            foreach (var json in directoryInfo.GetFiles("*.json"))
            {
                string jsonString = File.ReadAllText(json.ToString());
                listOfMaps.Add(JsonSerializer.Deserialize<Map>(jsonString));
            }

            return listOfMaps;
        }

        public static Map GetMapByName(string mapName)
        {
            return GetAllMaps().FirstOrDefault(map => map.Name == mapName);
        }
    }
}