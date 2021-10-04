using SplashKitSDK;

namespace BloonsProject
{
    public class Projectile
    {
        public Projectile(Point2D projectileLocation, Point2D projectileDestination, IShotType projectileShotType)
        {
            ProjectileLocation = projectileLocation;
            ProjectileDestination = projectileDestination;
            ProjectileShotType = projectileShotType;
            ProjectileStationaryTimeSpent = 0;
        }

        public Point2D ProjectileDestination { get; set; }
        public Point2D ProjectileLocation { get; set; }
        public IShotType ProjectileShotType { get; set; }
        public double ProjectileStationaryTimeSpent { get; set; }
    }
}