using SplashKitSDK;

namespace BloonsProject
{
    public class MapController
    {
        private readonly GameState _bloonSingleton = GameState.GetControllerSingletonInstance();

        public void SelectTowerOnMap(Point2D location, TowerOptionClicker towerOptions, string leftOrRightClick)
        {
            foreach (var (position, _) in towerOptions.ClickableShapes)
                if (location.X >= position.X && location.X <= towerOptions.Width + position.X &&
                    location.Y >= position.Y && location.Y <= position.Y + towerOptions.Height)
                    return;

            foreach (var tower in _bloonSingleton.Towers)
            {
                var towerRectangle = new Rectangle
                {
                    X = tower.Position.X,
                    Y = tower.Position.Y,
                    Height = Tower.Length,
                    Width = Tower.Length
                };
                if (leftOrRightClick == "left")
                {
                    tower.Selected = SplashKit.PointInRectangle(location, towerRectangle);
                }
                else
                {
                    tower.DebugModeSelected = SplashKit.PointInRectangle(location, towerRectangle);
                }
            }
        }

        public bool CanPlaceTowerOnMap(Point2D location, Map map)
        {
            for (var i = 0; i < map.Checkpoints.Count - 1; i++)
            {
                var line = SplashKit.LineFrom(map.Checkpoints[i], map.Checkpoints[i + 1]);
                if (SplashKit.PointLineDistance(location, line) < map.TrackWidthRadius ||
                    SplashKit.PointLineDistance(new Point2D { X = location.X + Tower.Length, Y = location.Y + Tower.Length }, line) <
                    map.TrackWidthRadius ||
                    location.X > map.Length
                )
                    return false;
            }

            foreach (var tower in _bloonSingleton.Towers)
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
    }
}