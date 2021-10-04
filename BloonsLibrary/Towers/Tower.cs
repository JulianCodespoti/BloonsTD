using SplashKitSDK;
using System.Collections.Generic;

namespace BloonsProject
{
    public abstract class Tower
    {
        public static int Length = 35;
        private readonly string _description;
        private readonly string _name;

        public Tower(string name, int cost, string description, Bitmap towerBitmap, IShotType shotType, int range)
        {
            TowerBitmap = towerBitmap;
            Selected = false;
            Cost = cost;
            Position = SplashKit.MousePosition();
            _description = description;
            _name = name;
            SellPrice = 0.7 * Cost;
            DebugModeSelected = false;
            ShotType = shotType;
            Range = range;
            Targeting = new TargetFirst();
        }

        public int Cost { get; }
        public bool DebugModeSelected { get; set; }

        public List<string> FullDescription =>
            new List<string>
            {
                _name,
                _description,
                "Attack Speed " + 200 / ShotType.ShotSpeed,
                "Range " + Range,
                "Cost " + Cost
            };

        public Point2D Position { get; }
        public int Range { get; set; }
        public bool Selected { get; set; }
        public double SellPrice { get; set; }
        public IShotType ShotType { get; }
        public ITarget Targeting { get; set; }
        public Bitmap TowerBitmap { get; }

        public void ResetTimer()
        {
            ShotType.TimeSinceLastShot = 0;
        }

        public bool ShotTimer()
        {
            return ShotType.TimeSinceLastShot > ShotType.ShotSpeed;
        }

        public void ShotTimerTick()
        {
            ShotType.TimeSinceLastShot++;
        }
    }
}