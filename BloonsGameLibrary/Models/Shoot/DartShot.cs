using SplashKitSDK;

namespace BloonsProject
{
    public class DartShot : IShotType
    {
        public DartShot()
        {
            ShotSpeed = 100;
            Damage = 1;
            FirerateUpgradeCount = 0;
            RangeUpgradeCount = 0;
            FirerateUpgradeCost = 75;
            RangeUpgradeCost = 75;
            TimeSinceLastShot = 0;
            ProjectileBitmap = new Bitmap("Dart", "./Images/Spike.png");
            ProjectileSpeed = 0.3;
            ProjectileSize = 1;
            ProjectileLength = 48;
            ProjectileWidth = 48;
        }

        public Bitmap ProjectileBitmap { get; }
        public double ProjectileSize { get; }
        public double ProjectileSpeed { get; set; }
        public double ShotSpeed { get; set; }
        public int Damage { get; }
        public double ProjectileLength { get; }
        public double ProjectileWidth { get; }
        public int FirerateUpgradeCost { get; }
        public int RangeUpgradeCost { get; }
        public int FirerateUpgradeCount { get; set; }
        public int RangeUpgradeCount { get; set; }
        public int TimeSinceLastShot { get; set; }

        public void ShotTimerTick()
        {
            TimeSinceLastShot++;
        }

        public bool ShotTimer()
        {
            return TimeSinceLastShot > ShotSpeed;
        }

        public void ResetTimer()
        {
            TimeSinceLastShot = 0;
        }
    }
}