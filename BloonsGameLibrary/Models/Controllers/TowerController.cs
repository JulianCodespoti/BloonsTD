using System;
using BloonsProject.Models.Shoot;
using SplashKitSDK;
using System.Linq;

namespace BloonsProject
{
    public class TowerController
    {
        private readonly GameState _gameState = GameState.GetControllerSingletonInstance();

        public void ShootBloons(Map map)
        {
            DamageBloons();
            if (_gameState.ProjectileManager.ProjectilesOnScreen != null) _gameState.ProjectileManager.IncrementAllProjectiles();

            if (_gameState.Towers.Count <= 0) return;

            foreach (var tower in _gameState.Towers.Where(t => t.ShotType.ShotTimer()))
                foreach (var bloon in _gameState.Bloons.Where(b =>
                    SplashKit.PointInCircle(b.Position, SplashKit.CircleAt(tower.Position, tower.Range))))
                {
                    if (!tower.ShotType.ShotTimer()) continue;

                    var projectileEndPoint = _gameState.ProjectileManager.GetProjectileEndPoint(bloon, tower);
                    tower.DirectionFacing = (float)Math.Atan(projectileEndPoint.Y / projectileEndPoint.X);
                    _gameState.ProjectileManager.AddProjectile(tower.Position, projectileEndPoint, tower.ShotType);

                    tower.ShotType.ResetTimer();
                }
        }

        public void DamageBloons()
        {
            foreach (Projectile projectile in _gameState.ProjectileManager.ProjectilesOnScreen)
            {
                foreach (Bloon bloon in _gameState.Bloons)
                {
                    var bloonCircle = new Circle() { Center = bloon.Position, Radius = bloon.Radius * projectile.ProjectileShotType.ProjectileSize };
                    var projectileLocation = new Point2D()
                    { X = projectile.ProjectileLocation.X + projectile.ProjectileShotType.ProjectileWidth / 2, Y = projectile.ProjectileLocation.Y + projectile.ProjectileShotType.ProjectileLength / 2};
                    if (SplashKit.PointInCircle(projectileLocation, bloonCircle))
                    {
                        var oldBloonHealth = bloon.Health;
                        bloon.TakeDamage(projectile.ProjectileShotType.Damage);
                        _gameState.Player.Money += oldBloonHealth - bloon.Health;
                    }
                }
            }
        }

        public void TickAllTowers()
        {
            foreach (Tower tower in _gameState.Towers)
            {
                tower.ShotType.ShotTimerTick();
            }
        }

        public void AddTower(Tower tower)
        {
            if (_gameState.Player.Money < tower.Cost) return;
            _gameState.Towers.Add(tower);
            _gameState.Player.Money -= tower.Cost;
        }

        public bool HaveSufficientFundsToPlaceTower(Tower tower)
        {
            return _gameState.Player.Money >= tower.Cost;
        }

        public void UpgradeOrSellTower(Tower tower, string upgrade, TowerOptionClicker towerOptions)
        {
            switch (upgrade)
            {
                case "Upgrade Range":
                    if (tower.ShotType.RangeUpgradeCount == 3) break;
                    if (_gameState.Player.Money < tower.ShotType.RangeUpgradeCost) break;
                    tower.Range += 50;
                    towerOptions.SelectedInGui = "none";
                    _gameState.Player.Money -= tower.ShotType.RangeUpgradeCost;
                    tower.SellPrice += 0.7 * tower.ShotType.RangeUpgradeCost;
                    tower.ShotType.RangeUpgradeCount++;
                    break;

                case "Upgrade Firerate":
                    if (tower.ShotType.FirerateUpgradeCount == 3) break;
                    if (_gameState.Player.Money < tower.ShotType.FirerateUpgradeCost) break;
                    tower.ShotType.ShotSpeed -= 10;
                    towerOptions.SelectedInGui = "none";
                    _gameState.Player.Money -= tower.ShotType.FirerateUpgradeCost;
                    tower.ShotType.FirerateUpgradeCount++;
                    tower.SellPrice += 0.7 * tower.ShotType.FirerateUpgradeCost;
                    break;

                case "Sell":
                    towerOptions.SelectedInGui = "none";
                    _gameState.Player.Money += tower.SellPrice;
                    _gameState.Towers.Remove(tower);
                    break;
            }
        }

        public void DisplayTowerDebugStats(Tower tower)
        {
            string debugText;
            if (tower.ShotType.TimeSinceLastShot > tower.ShotType.ShotSpeed)
            {
                debugText = "Ready";
            }
            else
            {
                debugText = tower.ShotType.TimeSinceLastShot.ToString() + "/" + tower.ShotType.ShotSpeed;
            }
            SplashKit.DrawText(debugText, Color.Black, tower.Position.X, tower.Position.Y - 20);
        }

        public void UpgradeOrSellSelectedTower(TowerController towerController, TowerOptionClicker towerOptions)
        {
            foreach (var tower in _gameState.Towers.ToList())
            {
                foreach (var (_, towerOption) in towerOptions.ClickableShapes.ToList())
                {
                    if (towerOptions.SelectedInGui == towerOption && tower.Selected)
                    {
                        towerController.UpgradeOrSellTower(tower, towerOption, towerOptions);
                    }
                }
            }

            towerOptions.SelectedInGui = "none";
        }
    }
}