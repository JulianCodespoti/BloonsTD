using SplashKitSDK;

namespace BloonsProject
{
    public class GuiRenderer
    {
        public void HighlightTowerInGui(TowerPlacerGuiOptions towerPlacer, Point2D position)
        {
            SplashKit.FillRectangle(new Color() { A = 200, B = 1, G = 1, R = 1 }, position.X - 3, position.Y - 3,
                towerPlacer.Width,
                towerPlacer.Height);
        }

        public void RenderTowerOnCursorWhilePlacing(Tower selectedTower, bool validPlacement)
        {
            var towerCentre = new Point2D
            { X = SplashKit.MousePosition().X + 15, Y = SplashKit.MousePosition().Y + 15 };
            SplashKit.FillCircle(new Color() { A = 160, B = 1, G = 1, R = 1 }, new Circle { Center = towerCentre, Radius = selectedTower.Range });
            SplashKit.DrawBitmap(selectedTower.TowerBitmap, SplashKit.MousePosition().X - 15, SplashKit.MousePosition().Y - 15);
            if (!validPlacement)
            {
                SplashKit.FillCircle(new Color() { A = 160, B = 255, G = 255, R = 1 }, new Circle { Center = towerCentre, Radius = selectedTower.Range });
            }
        }

        public void WriteTowerDescription(TowerPlacerGuiOptions towerPlacer, Tower selectedTower)
        {
            var textPositionY = 350;
            // Draw description of selected tower
            foreach (var selectedTowerSmallDescription in selectedTower.FullDescription)
            {
                SplashKit.DrawText(selectedTowerSmallDescription, Color.AntiqueWhite, "BloonFont", 20, 820, textPositionY);
                textPositionY += 30;
            }
        }
    }
}