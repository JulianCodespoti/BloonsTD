using SplashKitSDK;
using System.Collections.Generic;

namespace BloonsProject
{
    public class EmptyCornfieldMap : Map
    {
        public EmptyCornfieldMap() : base(SplashKit.LoadBitmap(
                "EmptyCornfieldMap",
                "./Images/EmptyCornfieldMap.png"),
            811,
            560,
            25,
            new List<Point2D>
            {
                new Point2D {X = 25, Y = 0},
                new Point2D {X = 27, Y = 536},
                new Point2D {X = 50, Y = 545},
                new Point2D {X = 275, Y = 542},
                new Point2D {X = 285, Y = 213},
                new Point2D {X = 452, Y = 212},
                new Point2D {X = 464, Y = 388},
                new Point2D {X = 718, Y = 385},
                new Point2D {X = 723, Y = 0}
            }, "Empty Cornfield Map")
        {
        }
    }
}