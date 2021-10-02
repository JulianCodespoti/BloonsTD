using SplashKitSDK;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace BloonsProject
{
    public abstract class Map
    {
        public Map(Bitmap bloonsMap, int length, int height, int trackWidthRadius, List<Point2D> checkpoints, string name)
        {
            BloonsMap = bloonsMap;
            Length = length;
            Height = height;
            TrackWidthRadius = trackWidthRadius;
            Checkpoints = checkpoints;
            Name = name;
        }

        public Bitmap BloonsMap { get; }
        public string Name { get; }
        public int Height { get; }

        public int Length { get; }
        public int TrackWidthRadius { get; }
        public List<Point2D> Checkpoints { get; }

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