﻿using SplashKitSDK;

namespace BloonsProject
{
    public class SniperTower : Tower
    {
        public SniperTower() : base("Sniper Monkey", 75,
            "Has a powerful shot", new Bitmap("SniperTower",
                "./Images/Sniper.png"), new SniperShot(), 300)
        {
        }

        public static string Name => "Sniper Monkey";

        public static Bitmap Portrait => new Bitmap("Sniper Portrait",
            "./Images/SniperSelect.png");
    }
}