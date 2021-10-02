using SplashKitSDK;

namespace BloonsProject
{
    public interface IShotType
    {
        public double ShotSpeed { get; set; }
        public int Damage { get; }
        public double ProjectileLength { get; }
        public double ProjectileWidth { get; }
        public int FirerateUpgradeCost { get; }
        public int RangeUpgradeCost { get; }
        public int FirerateUpgradeCount { get; set; }
        public int RangeUpgradeCount { get; set; }
        public int TimeSinceLastShot { get; set; }
        public Bitmap ProjectileBitmap { get; }
        public double ProjectileSpeed { get; set; }
        public double ProjectileSize { get; }

        void ShotTimerTick();

        bool ShotTimer();

        void ResetTimer();
    }
}