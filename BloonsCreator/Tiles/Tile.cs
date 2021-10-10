using SplashKitSDK;
using Color = SplashKitSDK.Color;

namespace BloonsCreator
{
    public class Tile
    {
        public Tile(TileType isBloonsTrack, Bitmap bitmap)
        {
            TileType = isBloonsTrack;
            Bitmap = bitmap;
            Height = 50;
            Width = 50;
        }

        public Bitmap Bitmap { get; }
        public int Height { get; set; }
        public TileType TileType { get; set; }
        public Point2D Position { get; set; }
        public int Width { get; set; }
    }
}