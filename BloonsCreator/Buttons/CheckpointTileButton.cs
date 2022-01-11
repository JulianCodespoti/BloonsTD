using SplashKitSDK;

namespace BloonsCreator
{
    public class CheckpointTileButton : TileButton
    {
        public CheckpointTileButton() : base(TileType.Checkpoint, new Bitmap("stoneBig", "../BloonsLibrary/Resources/stoneBig.png"), ButtonTypes.AddCheckpointTile, new Point2D() { X = 100, Y = 100 })
        {
        }
    }
}