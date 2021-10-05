using SplashKitSDK;
using Color = SplashKitSDK.Color;

namespace BloonsProject
{
    public class Renderer
    {
        private readonly GameState _bloonSingleton = GameState.GetControllerSingletonInstance();
        private readonly EntityRenderer _entityRenderer = new EntityRenderer();
        private readonly GuiRenderer _guiRenderer = new GuiRenderer();
        private readonly Map _map;
        private readonly TowerOptionsRenderer _towerOptionsRenderer = new TowerOptionsRenderer();
        private readonly Window _window;

        public Renderer(Window window, Map map)
        {
            _window = window;
            _map = map;
            SplashKit.LoadFont("BloonFont", "./Resources/BloonFont.ttf");
        }

        public void RenderEntities(BloonController bloonController, TowerController towerController, TowerGuiOptions towerOptions, TowerTargetingGuiOptions targetOptions)
        {
            _entityRenderer.RenderBloons(bloonController, _map);
            _entityRenderer.RenderTowers(towerController);
            _entityRenderer.RenderTowerProjectiles();
        }

        public void RenderGuiTowerOptions(TowerPlacerGuiOptions towerPlacer, TowerController towerController, MapController mapController)
        {
            // Draw tower select images
            foreach (var (towerPositionInGui, towerName) in towerPlacer.ClickableShapes)
            {
                SplashKit.DrawBitmap(towerPlacer.ClickableShapeImage(towerName), towerPositionInGui.X, towerPositionInGui.Y);

                if (towerPlacer.SelectedInGui != towerName) continue;

                // Draw outline around selected tower select image
                _guiRenderer.HighlightTowerInGui(towerPlacer, towerPositionInGui);

                // Create a new tower depending on the selected tower and write tower description in GUI
                var selectedTower = TowerFactory.CreateTowerOfType(towerPlacer.SelectedInGui);
                _guiRenderer.WriteTowerDescription(towerPlacer, selectedTower);

                // If you have enough money, selecting tower will draw the tower at your mouses location to place.
                if (!towerController.HaveSufficientFundsToPlaceTower(selectedTower)) continue;

                var validPlacement = mapController.CanPlaceTowerOnMap(SplashKit.MousePosition(), _map);
                _guiRenderer.RenderTowerOnCursorWhilePlacing(selectedTower, validPlacement);
            }
        }

        public void RenderMap()
        {
            SplashKit.ClearScreen();
            SplashKit.DrawBitmapOnWindow(_window, SplashKit.LoadBitmap("map", _map.BloonsMap), 0, 0);
            SplashKit.DrawBitmapOnWindow(_window, Gui.GuiBitmap, 800, 0);
            SplashKit.DrawText(_bloonSingleton.Player.Round.ToString(), Color.AntiqueWhite, "BloonFont", 20, 950, 25);
            SplashKit.DrawText(_bloonSingleton.Player.Money.ToString(), Color.AntiqueWhite, "BloonFont", 20, 950, 65);
            SplashKit.DrawText(_bloonSingleton.Player.Lives.ToString(), Color.AntiqueWhite, "BloonFont", 20, 950, 100);
        }

        public void RenderSelectedTowerOptions(TowerGuiOptions towerOptions, TowerTargetingGuiOptions targetOptions)
        {
            _towerOptionsRenderer.RenderSelectedTowerOptions(towerOptions, targetOptions);
        }
    }
}
