using SplashKitSDK;

namespace BloonsProject
{
    public class DartTower : Tower
    {
        public DartTower() : base("Dart Monkey", 120, "The Regular Tower", new Bitmap("DartTower",
            "./Resources/Dart.png"), new DartShot(), 100)
        {
        }

        public static string Name => "Dart Monkey";

        public static Bitmap Portrait => new Bitmap("Dart Portrait",
            "./Resources/DartSelect.png");
    }
}