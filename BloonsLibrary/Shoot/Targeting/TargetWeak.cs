﻿using System.Collections.Generic;

namespace BloonsProject
{
    public class TargetWeak : ITarget
    {
        public TowerTargeting TargetType => TowerTargeting.Weak;

        public Bloon BloonToTarget(List<Bloon> bloons)
        {
            Bloon targetBloon = null;
            foreach (var bloon in bloons)
            {
                targetBloon ??= bloon;
                if (targetBloon.Health > bloon.Health)
                {
                    targetBloon = bloon;
                }
            }

            return targetBloon;
        }
    }
}