using SplashKitSDK;
using System.Collections.Generic;
using System.Drawing;
using BloonsProject.Models.Extensions;

namespace BloonsProject
{
    public abstract class Tower
    {
        public static int Length = 35;
        private readonly string _description;
        private readonly string _name;

        public Tower(string name, int cost, string description, Bitmap towerBitmap, IShotType shotType, int range)
        {
            TowerBitmap = towerBitmap;
            Selected = false;
            Cost = cost;
            Position = SplashKit.MousePosition();
            _description = description;
            _name = name;
            SellPrice = 0.7 * Cost;
            DebugModeSelected = false;
            ShotType = shotType;
            Range = range;
            DirectionFacing = 0;
        }

        public IShotType ShotType { get; set; }
        public float DirectionFacing { get; set; }
        public Point2D Position { get; set; }
        public int Range { get; set; }

        public double SellPrice { get; set; }
        public int Cost { get; }
        public bool Selected { get; set; }

        public Bitmap TowerBitmap { get; }
        public bool DebugModeSelected { get; set; }

        public List<string> FullDescription
        {
            get
            {
                var fullDescription = new List<string>();
                fullDescription.Add(_name);
                fullDescription.Add(_description);
                fullDescription.Add("Attack Speed " + ShotType.ShotSpeed);
                fullDescription.Add("Range " + Range);
                fullDescription.Add("Cost " + Cost);
                return fullDescription;
            }
        }
    }
}