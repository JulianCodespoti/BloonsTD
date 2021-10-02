using SplashKitSDK;

namespace BloonsProject
{
    public class GuiRenderer
    {
        public void RenderTowerOnCursorWhilePlacing(Tower selectedTower, bool validPlacement, TowerController towerController)
        {
            var towerCentre = new Point2D
            { X = SplashKit.MousePosition().X + 15, Y = SplashKit.MousePosition().Y + 15 };
            SplashKit.DrawCircle(Color.White, new Circle { Center = towerCentre, Radius = selectedTower.Range });

            SplashKit.DrawBitmap(selectedTower.TowerBitmap, SplashKit.MousePosition().X - 15, SplashKit.MousePosition().Y - 15);
            if (validPlacement)
            {
                SplashKit.FillRectangle(Color.Green,
                    new Rectangle
                    {
                        X = SplashKit.MousePosition().X + 12,
                        Y = SplashKit.MousePosition().Y + 12,
                        Height = Tower.Length / 3,
                        Width = Tower.Length / 3
                    });
                return;
            }
            SplashKit.FillRectangle(Color.Red,
            new Rectangle
            {
                X = SplashKit.MousePosition().X + 12,
                Y = SplashKit.MousePosition().Y + 12,
                Height = Tower.Length / 3,
                Width = Tower.Length / 3
            });
        }

        public void HighlightTowerInGui(TowerPlacerClicker towerPlacer, Point2D position)
        {
            SplashKit.DrawRectangle(Color.Black, position.X - 3, position.Y - 3,
                towerPlacer.Width,
                towerPlacer.Height);
        }

        public void WriteTowerDescription(TowerPlacerClicker towerPlacer, Tower selectedTower)
        {
            var textPositionY = 350;
            // Draw description of selected tower
            foreach (var selectedTowerSmallDescription in selectedTower.FullDescription)
            {
                SplashKit.DrawText(selectedTowerSmallDescription, Color.Black, 820, textPositionY);
                textPositionY += 10;
            }
        }
    }
}