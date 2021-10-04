using SplashKitSDK;

namespace BloonsProject
{
    public class LaserTower : Tower
    {
        public LaserTower() : base("Laser Monkey", 400, "Did someone say rapidfire?", new Bitmap("LaserTower", "./Resources/Laser.png"), new LaserShot(), 300)
        {
        }

        public static string Name => "Laser Monkey";

        public static Bitmap Portrait => new Bitmap("Laser Portrait",
            "./Resources/LaserSelect.png");
    }
}