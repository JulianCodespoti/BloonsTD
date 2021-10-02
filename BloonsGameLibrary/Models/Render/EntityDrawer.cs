using System;
using System.Reflection.Metadata.Ecma335;
using BloonsProject.Models.Shoot;
using SplashKitSDK;

namespace BloonsProject
{
    internal class EntityDrawer
    {
        private readonly GameState _bloonSingleton = GameState.GetControllerSingletonInstance();

        public void DrawBloon(Bloon bloon)
        {
            SplashKit.FillCircle(bloon.Color, bloon.Position.X, bloon.Position.Y, bloon.Radius);
        }

        public void DrawTower(Tower tower)
        {
            SplashKit.DrawBitmap(tower.TowerBitmap, tower.Position.X - 13, tower.Position.Y - 13);
        }

        public void DrawTowerRange(Tower tower)
        {
            var towerCentre = new Point2D
            { X = tower.Position.X + Tower.Length / 2, Y = tower.Position.Y + Tower.Length / 2 };
            SplashKit.DrawCircle(Color.White, new Circle { Center = towerCentre, Radius = tower.Range });
        }

        public void TowerProjectileRenderer()
        {
            if (_bloonSingleton.ProjectileManager.ProjectilesOnScreen == null) return;
            foreach (Projectile projectile in _bloonSingleton.ProjectileManager.ProjectilesOnScreen)
            {
                SplashKit.DrawBitmap(projectile.ProjectileShotType.ProjectileBitmap, projectile.ProjectileLocation.X, projectile.ProjectileLocation.Y);
            }
        }
    }
}