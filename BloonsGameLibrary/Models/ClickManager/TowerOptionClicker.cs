using SplashKitSDK;
using System.Collections.Generic;

namespace BloonsProject
{
    public class TowerOptionClicker
    {
        public Dictionary<Point2D, string> ClickableShapes { get; }
        public string SelectedInGui { get; set; }
        public int Width { get; }
        public int Height { get; }
        public Bitmap SellTowerBitmap { get; }
        public Bitmap UpgradeRangeBitmap { get; }
        public Bitmap UpgradeFirerateBitmap { get; }
        public List<Bitmap> ClickableShapeImages { get; set; }

        public TowerOptionClicker()
        {
            ClickableShapes = new Dictionary<Point2D, string>
            {
                [new Point2D { X = 850, Y = 350 }] = "Upgrade Range",
                [new Point2D { X = 990, Y = 350 }] = "Upgrade Firerate",
                [new Point2D { X = 920, Y = 450 }] = "Sell"
            };
            Height = 50;
            Width = 100;
            SelectedInGui = "none";
            SellTowerBitmap = new Bitmap("Sell",
                "./Images/sell tower.png");
            UpgradeFirerateBitmap = new Bitmap("Firerrate",
                "./Images/firerate upgrade.png");
            UpgradeRangeBitmap = new Bitmap("Range",
                "./Images/range upgrade.png");
            ClickableShapeImages = new List<Bitmap>() { UpgradeRangeBitmap, UpgradeFirerateBitmap, SellTowerBitmap };
        }

        public void ClickShape(Point2D pt)
        {
            foreach (var (position, towerOption) in ClickableShapes)
            {
                if (pt.X >= position.X && pt.X <= Width + position.X && pt.Y >= position.Y &&
                    pt.Y <= position.Y + Height)
                {
                    SelectedInGui = towerOption;
                    break;
                }
                SelectedInGui = "none";
            }
        }
    }
}