using BloonsProject.Models.Extensions;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloonsProject
{
    public class ProjectileManager
    {
        public ProjectileManager()
        {
            ProjectilesOnScreen = new List<Projectile>();
        }

        public List<Projectile> ProjectilesOnScreen { get; set; }

        public void AddProjectile(Point2D initialPosition, Point2D projectileDestination, IShotType shotType)
        {
            var projectile = new Projectile(initialPosition, projectileDestination, shotType);
            ProjectilesOnScreen.Add(projectile);
        }

        public Point2D GetProjectileEndPoint(Bloon bloon, Tower tower)
        {
            var towerToBloonAngle =
                        Math.Atan((bloon.Position.Y - tower.Position.Y) / (bloon.Position.X - tower.Position.X)) + Math.PI;

            if (bloon.Position.X > tower.Position.X)
            {
                towerToBloonAngle = Math.Atan((bloon.Position.Y - tower.Position.Y) /
                                              (bloon.Position.X - tower.Position.X));
            }
            ; var projectileEndPoint = new Point2D()
            {
                X = tower.Position.X + (tower.Range * Math.Cos(towerToBloonAngle)),
                Y = tower.Position.Y + (tower.Range * Math.Sin(towerToBloonAngle))
            };
            projectileEndPoint.X -= tower.ShotType.ProjectileWidth / 2;
            projectileEndPoint.Y -= tower.ShotType.ProjectileLength / 2;
            return projectileEndPoint;
        }

        public void IncrementAllProjectiles()
        {
            foreach (Projectile projectile in ProjectilesOnScreen.ToList())
            {
                if (projectile.ProjectileStationaryTimeSpent == 0)
                {
                    projectile.ProjectileLocation = SplashKitExtensions.Lerp(projectile.ProjectileLocation, projectile.ProjectileDestination, projectile.ProjectileShotType.ProjectileSpeed);
                }

                if (!(Math.Abs(projectile.ProjectileLocation.X - projectile.ProjectileDestination.X) < 5) ||
                    !(Math.Abs(projectile.ProjectileLocation.Y - projectile.ProjectileDestination.Y) < 5)) continue;
                projectile.ProjectileStationaryTimeSpent++;
                if (projectile.ProjectileStationaryTimeSpent <
                    projectile.ProjectileShotType.ProjectileStationaryTime) continue;

                ProjectilesOnScreen.Remove(projectile);
            }
        }
    }
}