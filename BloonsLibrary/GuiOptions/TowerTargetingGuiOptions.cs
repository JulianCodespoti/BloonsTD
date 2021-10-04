using SplashKitSDK;
using System.Collections.Generic;

namespace BloonsProject
{
    public class TowerTargetingGuiOptions
    {
        public TowerTargetingGuiOptions()
        {
            TargetingButtonLocations = new Dictionary<Point2D, TowerTargeting>()
            {
                [new Point2D() { X = 830, Y = 330 }] = TowerTargeting.First,
                [new Point2D() { X = 900, Y = 330 }] = TowerTargeting.Last,
                [new Point2D() { X = 970, Y = 330 }] = TowerTargeting.Strong,
                [new Point2D() { X = 1040, Y = 330 }] = TowerTargeting.Weak
            };
            Height = 30;
            Width = 60;
            SelectedInGui = TowerTargeting.First;
        }

        public int Height { get; }
        public TowerTargeting SelectedInGui { get; set; }
        public Dictionary<Point2D, TowerTargeting> TargetingButtonLocations { get; }
        public int Width { get; }

        public void ClickShape(Point2D pt)
        {
            foreach (var (position, targetOption) in TargetingButtonLocations)
            {
                if (pt.X >= position.X && pt.X <= Width + position.X && pt.Y >= position.Y &&
                    pt.Y <= position.Y + Height)
                {
                    SelectedInGui = targetOption;
                    break;
                }
            }
        }
    }
}