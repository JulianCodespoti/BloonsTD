using SplashKitSDK;
using System.Collections.Generic;

namespace BloonsProject
{
    public class MediumMap : Map
    {
        public MediumMap() : base(SplashKit.LoadBitmap("MediumMap", "./Images/BloonsMap2.png"),
            800,
            560,
            25,
            new List<Point2D>
            {
                new Point2D {X = 0, Y = 234},
                new Point2D {X = 404, Y = 230},
                new Point2D {X = 404, Y = 105},
                new Point2D {X = 269, Y = 105},
                new Point2D {X = 269, Y = 430},
                new Point2D {X = 215, Y = 455},
                new Point2D {X = 162, Y = 455},
                new Point2D {X = 132, Y = 418},
                new Point2D {X = 140, Y = 328},
                new Point2D {X = 491, Y = 316},
                new Point2D {X = 523, Y = 292},
                new Point2D {X = 523, Y = 190},
                new Point2D {X = 616, Y = 190},
                new Point2D {X = 625, Y = 411},
                new Point2D {X = 374, Y = 414},
                new Point2D {X = 364, Y = 560}
            }, "Medium Map")
        {
        }
    }
}