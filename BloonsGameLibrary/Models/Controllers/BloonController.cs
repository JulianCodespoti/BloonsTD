using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloonsProject
{
    public class BloonController
    {
        public int ticksSinceLastSentBloon { get; set; }

        private readonly GameState _bloonSingleton = GameState.GetControllerSingletonInstance();

        public int BloonsOnScreen(Window window)
        {
            var amountOfBloons = _bloonSingleton.Bloons.Count(b =>
                b.Position.X > 0 && b.Position.X < window.Width && b.Position.Y > 0 && b.Position.Y < window.Height);
            return amountOfBloons;
        }

        public void CheckBloonHealth()
        {
            foreach (var b in _bloonSingleton.Bloons.ToList())
            {
                if (b.Color.ToString() == Color.Green.ToString() && b.Health == 2)
                {
                    _bloonSingleton.Bloons.Add(new BlueBloon { Position = b.Position, Checkpoint = b.Checkpoint });
                    _bloonSingleton.Bloons.Remove(b);
                }

                if (b.Color.ToString() == Color.Blue.ToString() && b.Health == 1)
                {
                    _bloonSingleton.Bloons.Add(new RedBloon { Position = b.Position, Checkpoint = b.Checkpoint });
                    _bloonSingleton.Bloons.Remove(b);
                }

                if (b.Health <= 0) _bloonSingleton.Bloons.Remove(b);
            }
        }

        public void ProcessBloons(Player player, Map map)
        {
            ticksSinceLastSentBloon++;
            var sendBloonSpeed = 30 - player.Round;
            if (player.Round >= 20) sendBloonSpeed = 1;
            if (ticksSinceLastSentBloon <= sendBloonSpeed) return;
            var bloonsToAdd = new List<Bloon> { new RedBloon(), new BlueBloon(), new GreenBloon() };
            var randomBloonSelection = new Random().Next(bloonsToAdd.Count);
            var bloon = bloonsToAdd[randomBloonSelection];
            if (_bloonSingleton.BloonsSpawned[bloon.Color] >= _bloonSingleton.BloonsToBeSpawned[bloon.Color]) return;
            AddBloon(bloon);
            bloon.Position = map.Checkpoints[0];
        }

        public void AddBloon(Bloon bloon)
        {
            _bloonSingleton.Bloons.Add(bloon);
            _bloonSingleton.BloonsSpawned[bloon.Color] += 1;
            ticksSinceLastSentBloon = 0;
        }

        public void MoveBloon(Bloon bloon, Map map)
        {
            if (bloon.Position.X <= map.Checkpoints[bloon.Checkpoint].X) bloon.MoveBloonInDirection(Direction.Right);
            if (bloon.Position.Y <= map.Checkpoints[bloon.Checkpoint].Y) bloon.MoveBloonInDirection(Direction.Down);
            if (bloon.Position.X >= map.Checkpoints[bloon.Checkpoint].X) bloon.MoveBloonInDirection(Direction.Left);
            if (bloon.Position.Y >= map.Checkpoints[bloon.Checkpoint].Y) bloon.MoveBloonInDirection(Direction.Up);
            if (SplashKit.PointInRectangle(bloon.Position, new Rectangle
            {
                X = map.Checkpoints[bloon.Checkpoint].X - 4,
                Y = map.Checkpoints[bloon.Checkpoint].Y - 4,
                Height = 4,
                Width = 4
            }))
                bloon.Checkpoint++;
        }
    }
}