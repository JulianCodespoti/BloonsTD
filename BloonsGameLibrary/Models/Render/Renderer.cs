using SplashKitSDK;
using Color = SplashKitSDK.Color;

namespace BloonsProject
{
    public class Renderer
    {
        private readonly Window _window;
        private readonly Map _map;
        private readonly GuiRenderer _mapRenderer = new GuiRenderer();
        private readonly EntityRenderer _entityRenderer = new EntityRenderer();

        private readonly GameState _bloonSingleton = GameState.GetControllerSingletonInstance();

        public Renderer(Window window, Map map)
        {
            _window = window;
            _map = map;
        }

        public void RenderMap()
        {
            SplashKit.ClearScreen();
            SplashKit.DrawBitmapOnWindow(_window, _map.BloonsMap, 0, 0);
            SplashKit.DrawBitmapOnWindow(_window, Gui.GuiBitmap, 800, 0);
            SplashKit.DrawText(_bloonSingleton.Player.Round.ToString(), Color.Black, 940, 45);
            SplashKit.DrawText(_bloonSingleton.Player.Lives.ToString(), Color.Black, 940, 85);
            SplashKit.DrawText(_bloonSingleton.Player.Money.ToString(), Color.Black, 940, 125);
        }

        public void RenderGuiTowerOptions(TowerPlacerClicker towerPlacer, TowerController towerController, MapController mapController)
        {
            // Draw tower select images
            foreach (var (towerPositionInGui, towerName) in towerPlacer.ClickableShapes)
            {
                SplashKit.DrawBitmap(towerPlacer.ClickableShapeImage(towerName), towerPositionInGui.X, towerPositionInGui.Y);

                if (towerPlacer.SelectedInGui != towerName) continue;

                // Draw outline around selected tower select image
                _mapRenderer.HighlightTowerInGui(towerPlacer, towerPositionInGui);

                // Create a new tower depending on the selected tower and write tower description in GUI
                var selectedTower = TowerFactory.CreateTowerOfType(towerPlacer.SelectedInGui);
                _mapRenderer.WriteTowerDescription(towerPlacer, selectedTower);

                // If you have enough money, selecting tower will draw the tower at your mouses location to place.
                if (!towerController.HaveSufficientFundsToPlaceTower(selectedTower)) return;

                var validPlacement = mapController.CanPlaceTowerOnMap(SplashKit.MousePosition(), _map);
                _mapRenderer.RenderTowerOnCursorWhilePlacing(selectedTower, validPlacement, towerController);
            }
        }

        public void RenderEntities(BloonController bloonController, TowerController towerController, TowerOptionClicker towerOptions)
        {
            _entityRenderer.RenderBloons(bloonController, _map);
            _entityRenderer.RenderTowers(towerController, towerOptions);
            _entityRenderer.RenderTowerProjectiles();
        }
    }
}