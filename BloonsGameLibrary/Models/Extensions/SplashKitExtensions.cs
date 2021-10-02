using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace BloonsProject.Models.Extensions
{
    internal class SplashKitExtensions
    {
        private static double Lerp(double firstDouble, double secondDouble, double by)
        {
            return firstDouble * (1 - by) + secondDouble * by;
        }

        public static Point2D Lerp(Point2D firstVector, Point2D secondVector, double by)
        {
            double retX = Lerp(firstVector.X, secondVector.X, by);
            double retY = Lerp(firstVector.Y, secondVector.Y, by);
            return new Point2D() { X = retX, Y = retY };
        }
    }
}