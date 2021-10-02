using SplashKitSDK;
using System.Collections.Generic;
using System.Linq;

namespace BloonsProject
{
    internal class EntityRenderer
    {
        private readonly EntityDrawer _entityRenderer = new EntityDrawer();
        private readonly GameState _bloonSingleton = GameState.GetControllerSingletonInstance();

        public void RenderBloons(BloonController bloonController, Map map)
        {
            foreach (var bloon in _bloonSingleton.Bloons)
            {
                _entityRenderer.DrawBloon(bloon);
                bloonController.MoveBloon(bloon, map);
            }
        }

        public void RenderTowers(TowerController towerController, TowerOptionClicker towerOptions)
        {
            foreach (var tower in _bloonSingleton.Towers.ToList())
            {
                if (tower.DebugModeSelected)
                {
                    towerController.DisplayTowerDebugStats(tower);
                }
                _entityRenderer.DrawTower(tower);
                if (!tower.Selected) continue;
                _entityRenderer.DrawTowerRange(tower);

                var imageLocationList = new List<Point2D>(towerOptions.ClickableShapes.Keys);
                var currentTowerUpgradesList = new List<double>()
                    {tower.ShotType.RangeUpgradeCount, tower.ShotType.FirerateUpgradeCount, tower.SellPrice};
                for (int i = 0; i < towerOptions.ClickableShapes.Count; i++)
                {
                    if (i == towerOptions.ClickableShapes.Count - 1)
                    {
                        SplashKit.DrawBitmap(towerOptions.ClickableShapeImages[i], imageLocationList[i].X, imageLocationList[i].Y);
                        SplashKit.DrawText("Sell price: " + currentTowerUpgradesList[i].ToString(), Color.Black,
                            imageLocationList[i].X - 5, imageLocationList[i].Y + towerOptions.Height + 10);
                        continue;
                    }
                    SplashKit.DrawBitmap(towerOptions.ClickableShapeImages[i], imageLocationList[i].X, imageLocationList[i].Y);
                    SplashKit.DrawText("Upgrades: " + currentTowerUpgradesList[i].ToString() + "/3", Color.Black,
                        imageLocationList[i].X, imageLocationList[i].Y + towerOptions.Height + 10);
                }
            }
        }

        public void RenderTowerProjectiles()
        {
            _entityRenderer.TowerProjectileRenderer();
        }
    }
}