using SplashKitSDK;

namespace BloonsCreator
{
    public class GrassTileButton : TileButton
    {
        public GrassTileButton() : base(TileType.Normal, new Bitmap("grassBig", "../BloonsLibrary/Resources/grassBig.png"), ButtonTypes.AddRegularTile, new Point2D() { X = 100, Y = 100})
        {
        }
    }
}