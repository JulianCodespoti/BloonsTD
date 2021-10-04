using SplashKitSDK;
using System.Linq;

namespace BloonsProject
{
    public class TowerController
    {
        private readonly GameState _gameState = GameState.GetControllerSingletonInstance();

        public void AddTower(Tower tower)
        {
            if (_gameState.Player.Money < tower.Cost) return;
            _gameState.Towers.Add(tower);
            _gameState.Player.Money -= tower.Cost;
        }

        public void ChangeTowerTargeting(TowerTargetingGuiOptions targetOptions, TowerController towerController)
        {
            foreach (var tower in _gameState.Towers.ToList())
            {
                foreach (var targetOption in targetOptions.TargetingButtonLocations.Values.Where(
                    targetOption => targetOptions.SelectedInGui == targetOption && tower.Selected))
                {
                    towerController.SetTowerTargeting(tower, targetOptions);
                }
            }

            targetOptions.SelectedInGui = TowerTargeting.Unselected;
        }

        public void DamageBloons()
        {
            foreach (Projectile projectile in _gameState.ProjectileManager.ProjectilesOnScreen)
            {
                foreach (Bloon bloon in _gameState.Bloons)
                {
                    var bloonCircle = new Circle() { Center = bloon.Position, Radius = bloon.Radius * projectile.ProjectileShotType.ProjectileSize };
                    var projectileLocation = new Point2D()
                    { X = projectile.ProjectileLocation.X + projectile.ProjectileShotType.ProjectileWidth / 2, Y = projectile.ProjectileLocation.Y + projectile.ProjectileShotType.ProjectileLength / 2 };
                    if (SplashKit.PointInCircle(projectileLocation, bloonCircle))
                    {
                        var oldBloonHealth = bloon.Health;
                        bloon.TakeDamage(projectile.ProjectileShotType.Damage);
                        _gameState.Player.Money += oldBloonHealth - bloon.Health;
                    }
                }
            }
        }

        public bool HaveSufficientFundsToPlaceTower(Tower tower)
        {
            return _gameState.Player.Money >= tower.Cost;
        }

        public void SetTowerTargeting(Tower tower, TowerTargetingGuiOptions targetOptions)
        {
            tower.Targeting = targetOptions.SelectedInGui switch
            {
                TowerTargeting.First => new TargetFirst(),
                TowerTargeting.Last => new TargetLast(),
                TowerTargeting.Strong => new TargetStrong(),
                TowerTargeting.Weak => new TargetWeak(),
                _ => tower.Targeting
            };
        }

        public void ShootBloons(Map map)
        {
            DamageBloons();
            _gameState.ProjectileManager.IncrementAllProjectiles();
            if (_gameState.Towers.Count <= 0) return;
            foreach (var tower in _gameState.Towers.Where(t => t.ShotTimer()))
            {
                if (!tower.ShotTimer()) continue;
                var bloonsInTowerRadius = _gameState.Bloons.Where(b =>
                    SplashKit.PointInCircle(b.Position, SplashKit.CircleAt(tower.Position, tower.Range))).ToList();
                if (bloonsInTowerRadius.Count == 0) return;

                var bloonToTarget = tower.Targeting.BloonToTarget(bloonsInTowerRadius);
                var projectileEndPoint = _gameState.ProjectileManager.GetProjectileEndPoint(bloonToTarget, tower);
                _gameState.ProjectileManager.AddProjectile(tower.Position, projectileEndPoint, tower.ShotType);
                tower.ResetTimer();
            }
        }

        public void TickAllTowers()
        {
            foreach (Tower tower in _gameState.Towers)
            {
                tower.ShotTimerTick();
            }
        }

        public void UpgradeOrSellSelectedTower(TowerController towerController, TowerGuiOptions towerOptions)
        {
            foreach (var tower in _gameState.Towers.ToList())
            {
                foreach (var towerOption in towerOptions.UpgradeOptionsInGui.Values.Where(
                    towerOption => towerOptions.SelectedInGui == towerOption && tower.Selected))
                {
                    towerController.UpgradeOrSellTower(tower, towerOption, towerOptions);
                }
            }

            towerOptions.SelectedInGui = "none";
        }

        public void UpgradeOrSellTower(Tower tower, string upgrade, TowerGuiOptions towerOptions)
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
    }
}