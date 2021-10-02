using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace BloonsProject.Models.Shoot
{
    public class Projectile
    {
        public Projectile(Point2D projectileLocation, Point2D projectileDestination, IShotType projectileShotType)
        {
            ProjectileLocation = projectileLocation;
            ProjectileDestination = projectileDestination;
            ProjectileShotType = projectileShotType;
        }

        public Point2D ProjectileDestination { get; set; }
        public Point2D ProjectileLocation { get; set; }
        public IShotType ProjectileShotType { get; set; }
    }
}