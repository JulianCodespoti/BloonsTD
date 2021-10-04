using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace BloonsProject
{
    public class TowerPlacerGuiOptions
    {
        public TowerPlacerGuiOptions()
        {
            ClickableShapes = new Dictionary<Point2D, string>
            {
                [new Point2D { X = 845, Y = 200 }] = DartTower.Name,
                [new Point2D { X = 935, Y = 200 }] = LaserTower.Name,
                [new Point2D { X = 1025, Y = 200 }] = SniperTower.Name
            };
            Height = 112;
            Width = 82;
            SelectedInGui = "none";
        }

        public Dictionary<Point2D, string> ClickableShapes { get; }
        public int Height { get; }
        public string SelectedInGui { get; set; }
        public int Width { get; }

        public Bitmap ClickableShapeImage(string towerName)
        {
            if (towerName == DartTower.Name) return DartTower.Portrait;

            if (towerName == LaserTower.Name) return LaserTower.Portrait;

            if (towerName == SniperTower.Name) return SniperTower.Portrait;
            throw new Exception();
        }

        public void ClickShape(Point2D pt)
        {
            foreach (var (position, towerName) in ClickableShapes)
            {
                if (
                    pt.X >= position.X &&
                    pt.X <= Width + position.X &&
                    pt.Y >= position.Y &&
                    pt.Y <= position.Y + Height
                )
                {
                    SelectedInGui = towerName;
                    break;
                }

                SelectedInGui = "none";
            }
        }
    }
}