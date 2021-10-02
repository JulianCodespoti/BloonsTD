using SplashKitSDK;

namespace BloonsProject
{
    public class LaserTower : Tower
    {
        public LaserTower() : base("Laser Monkey", 200, "Did someone say rapidfire?", new Bitmap("LaserTower", "./Images/Laser.png"), new LaserShot(), 100)
        {
        }

        public static string Name => "Laser Monkey";

        public static Bitmap Portrait => new Bitmap("Laser Portrait",
            "./Images/LaserSelect.png");
    }
}