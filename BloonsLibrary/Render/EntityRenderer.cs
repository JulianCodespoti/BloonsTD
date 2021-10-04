using SplashKitSDK;
using System.Linq;

namespace BloonsProject
{
    internal class EntityRenderer
    {
        private readonly EntityDrawer _entityRenderer = new EntityDrawer();
        private readonly GameState _gameState = GameState.GetControllerSingletonInstance();

        public void DisplayTowerDebugStats(Tower tower)
        {
            string debugText;
            debugText = tower.ShotType.TimeSinceLastShot > tower.ShotType.ShotSpeed
                ? "Ready"
                : tower.ShotType.TimeSinceLastShot.ToString() + "/" + tower.ShotType.ShotSpeed;
            SplashKit.DrawText(debugText, Color.Black, tower.Position.X, tower.Position.Y - 20);
        }

        public void RenderBloons(BloonController bloonController, Map map)
        {
            foreach (var bloon in _gameState.Bloons)
            {
                _entityRenderer.DrawBloon(bloon);
                bloonController.MoveBloon(bloon, map);
            }
        }

        public void RenderTowerDebugMode(Tower tower)
        {
            if (tower.DebugModeSelected)
            {
                DisplayTowerDebugStats(tower);
            }
        }

        public void RenderTowerProjectiles()
        {
            _entityRenderer.TowerProjectileRenderer();
        }

        public void RenderTowers(TowerController towerController)
        {
            foreach (var tower in _gameState.Towers.ToList())
            {
                _entityRenderer.DrawTower(tower);
                RenderTowerDebugMode(tower);
                if (!tower.Selected) continue;
                _entityRenderer.DrawTowerRange(tower);
            }
        }
    }
}