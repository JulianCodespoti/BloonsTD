using BloonsProject.Models.Extensions;
using SplashKitSDK;
using System.Linq;

namespace BloonsProject
{
    public class MapController
    {
        private readonly GameState _gameState = GameState.GetControllerSingletonInstance();

        public bool CanPlaceTowerOnMap(Point2D location, Map map)
        {
            for (var i = 0; i < map.Checkpoints.Count - 1; i++)
            {
                var line = SplashKit.LineFrom(SplashKitExtensions.PointFromVector(map.Checkpoints[i]), SplashKitExtensions.PointFromVector(map.Checkpoints[i + 1]));
                if (SplashKit.PointLineDistance(location, line) < map.TrackWidthRadius ||
                    SplashKit.PointLineDistance(new Point2D { X = location.X + Tower.Length, Y = location.Y + Tower.Length }, line) <
                    map.TrackWidthRadius ||
                    location.X > map.Length
                )
                    return false;
            }

            foreach (var tower in _gameState.Towers)
                if (SplashKit.PointInRectangle(location,
                    new Rectangle
                    {
                        X = tower.Position.X - Tower.Length,
                        Y = tower.Position.Y - Tower.Length,
                        Height = 2 * Tower.Length,
                        Width = 2 * Tower.Length
                    }))
                    return false;
            return true;
        }

        public void ClickOnMap(Point2D location, TowerGuiOptions towerOptions, TowerTargetingGuiOptions targetOptions, MouseClickType clickType)
        {
            if (towerOptions.UpgradeOptionsInGui.Keys.Any(position => SplashKit.PointInRectangle(location, new Rectangle()
            {
                Height = towerOptions.Height,
                Width = towerOptions.Width,
                X = position.X,
                Y = position.Y
            })))
                return;
            if (targetOptions.TargetingButtonLocations.Keys.Any(position => SplashKit.PointInRectangle(location, new Rectangle()
            {
                Height = targetOptions.Height,
                Width = targetOptions.Width,
                X = position.X,
                Y = position.Y
            })))
                return;

            foreach (var tower in _gameState.Towers)
            {
                var towerRectangle = new Rectangle
                {
                    X = tower.Position.X,
                    Y = tower.Position.Y,
                    Height = Tower.Length,
                    Width = Tower.Length
                };
                if (clickType == MouseClickType.left)
                {
                    tower.Selected = SplashKit.PointInRectangle(location, towerRectangle);
                }
                else
                {
                    tower.DebugModeSelected = SplashKit.PointInRectangle(location, towerRectangle);
                }
            }
        }
    }
}