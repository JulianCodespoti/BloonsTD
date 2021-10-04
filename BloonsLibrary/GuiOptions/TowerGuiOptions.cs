using SplashKitSDK;
using System.Collections.Generic;

namespace BloonsProject
{
    public class TowerGuiOptions
    {
        public TowerGuiOptions()
        {
            UpgradeOptionsInGui = new Dictionary<Point2D, string>
            {
                [new Point2D { X = 850, Y = 360 }] = "Upgrade Range",
                [new Point2D { X = 990, Y = 360 }] = "Upgrade Firerate",
                [new Point2D { X = 920, Y = 460 }] = "Sell"
            };
            Height = 50;
            Width = 100;
            SelectedInGui = "none";
            SellTowerBitmap = new Bitmap("Sell",
                "./Resources/sell tower.png");
            UpgradeFirerateBitmap = new Bitmap("Firerrate",
                "./Resources/firerate upgrade.png");
            UpgradeRangeBitmap = new Bitmap("Range",
                "./Resources/range upgrade.png");
            ClickableShapeImages = new List<Bitmap>() { UpgradeRangeBitmap, UpgradeFirerateBitmap, SellTowerBitmap };
        }

        public List<Bitmap> ClickableShapeImages { get; set; }
        public int Height { get; }
        public string SelectedInGui { get; set; }
        public Bitmap SellTowerBitmap { get; }
        public Bitmap UpgradeFirerateBitmap { get; }
        public Dictionary<Point2D, string> UpgradeOptionsInGui { get; }
        public Bitmap UpgradeRangeBitmap { get; }
        public int Width { get; }

        public void ClickShape(Point2D pt)
        {
            foreach (var (position, towerOption) in UpgradeOptionsInGui)
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