using BloonLibrary.Extensions;
using SplashKitSDK;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BloonsProject
{
    public class Map
    {
        [JsonConstructor]
        public Map(string bloonsMap, int length, int height, int trackWidthRadius, List<VectorExtension> checkpoints, string name)
        {
            BloonsMap = bloonsMap;
            Length = length;
            Height = height;
            TrackWidthRadius = trackWidthRadius;
            Checkpoints = checkpoints;
            Name = name;
        }

        public string BloonsMap { get; }
        public List<VectorExtension> Checkpoints { get; }
        public int Height { get; }
        public int Length { get; }
        public string Name { get; }
        public int TrackWidthRadius { get; }

        public Dictionary<Color, int> BloonsPerRound(int round)
        {
            const int bloonAmount = 1;
            var currentRound = new Dictionary<Color, int>();

            var redBloonAmount = 2 * bloonAmount * (round + 1);
            var blueBloonAmount = bloonAmount * round * round;
            var greenBloonAmount = bloonAmount * (round - 1) * (round - 1) * (round - 1);

            currentRound[Color.Red] = redBloonAmount;
            currentRound[Color.Blue] = blueBloonAmount;
            currentRound[Color.Green] = greenBloonAmount;

            return currentRound;
        }
    }
}