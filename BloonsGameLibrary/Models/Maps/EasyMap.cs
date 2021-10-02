using SplashKitSDK;
using System.Collections.Generic;

namespace BloonsProject
{
    public class EasyMap : Map
    {
        public EasyMap() : base(SplashKit.LoadBitmap(
                "EasyMap",
                "./Images/BloonsMap.png"),
            800,
            560,
            25,
            new List<Point2D>
                {new Point2D {X = 0, Y = 100}, new Point2D {X = 555, Y = 100}, new Point2D {X = 555, Y = 560}}, "Easy Map")
        {
        }
    }
}