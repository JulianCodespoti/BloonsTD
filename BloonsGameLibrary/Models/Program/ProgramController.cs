using System;
using SplashKitSDK;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualBasic;

namespace BloonsProject
{
    public class ProgramController
    {
        private readonly Map _map;
        private readonly Window _window;
        private readonly GameState _bloonSingleton = GameState.GetControllerSingletonInstance();
        private readonly TowerPlacerClicker _towerPlacer = new TowerPlacerClicker();
        private readonly BloonController _bloonController = new BloonController();
        private readonly TowerController _towerController = new TowerController();
        private readonly GameController _gameController = new GameController();
        private readonly MapController _mapController = new MapController();
        private readonly Stopwatch _bloonStopWatch = new Stopwatch();
        private readonly Renderer _renderer;
        private readonly TowerOptionClicker _towerOptions = new TowerOptionClicker();

        private bool _isPaused;
        public event Action PauseEventHandler;

        public ProgramController(Map map)
        {
            _map = map;
            _window = new Window("Bloons", 1135, 560);
            _renderer = new Renderer(_window, _map);
        }

        public void DrawBloonsGame()
        {
            _renderer.RenderMap();
            _renderer.RenderGuiTowerOptions(_towerPlacer, _towerController, _mapController);
            _renderer.RenderEntities(_bloonController, _towerController, _towerOptions);
        }

        public void StartUpGame()
        {
            _bloonStopWatch.Start();
            _gameController.SetRound(_map, _bloonSingleton.Player.Round);
        }

        public void GameEvents()
        {
            _towerController.UpgradeOrSellSelectedTower(_towerController, _towerOptions);
            _gameController.LoseLives(_map);
            _towerController.ShootBloons(_map);
            _bloonController.CheckBloonHealth();
            _towerController.TickAllTowers();

            if (_gameController.CheckBloons() && _bloonController.BloonsOnScreen(_window) == 0)
            {
                _bloonSingleton.Player.Round++;
                _bloonSingleton.Player.Money += 20;
                _gameController.SetRound(_map, _bloonSingleton.Player.Round);
            }

            _bloonController.ProcessBloons(_bloonSingleton.Player, _map);
        }

        public void SelectedTowerEvents()
        {
            _mapController.SelectTowerOnMap(SplashKit.MousePosition(), _towerOptions, "left");
            if (_mapController.CanPlaceTowerOnMap(SplashKit.MousePosition(), _map))
            {
                if (_towerPlacer.SelectedInGui != "none")
                {
                    var tower = TowerFactory.CreateTowerOfType(_towerPlacer.SelectedInGui);
                    _towerController.AddTower(tower);
                    _towerPlacer.SelectedInGui = "none";
                }
            }
            else if (_mapController.CanPlaceTowerOnMap(SplashKit.MousePosition(), _map) == false)
            {
                _towerPlacer.ClickShape(SplashKit.MousePosition());
                _towerOptions.ClickShape(SplashKit.MousePosition());
            }
        }

        public void SelectedDebugTowerEvents()
        {
            _mapController.SelectTowerOnMap(SplashKit.MousePosition(), _towerOptions, "right");
        }

        public void RunGame()
        {
            StartUpGame();
            do
            {
                SplashKit.RefreshScreen(60);
                SplashKit.ProcessEvents();
                
                if (_isPaused) continue;

                DrawBloonsGame();
                GameEvents();

                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    SelectedTowerEvents();
                }
                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    SelectedDebugTowerEvents();
                }

                if (SplashKit.KeyTyped(KeyCode.PKey))
                {
                    PauseEventHandler?.Invoke();
                    _isPaused = true;
                }
            } while (!SplashKit.WindowCloseRequested("Bloons"));
        }

        public void Unpause() => _isPaused = false;
    }
}