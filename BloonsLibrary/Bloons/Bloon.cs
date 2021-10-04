using SplashKitSDK;
using System.Diagnostics;

namespace BloonsProject
{
    public abstract class Bloon
    {
        private Point2D _position;

        public Bloon(int health, string name, Color color, int velocityX, int velocityY)
        {
            Health = health;
            Color = color;
            Radius = 15;
            _position.X = 20;
            _position.Y = 100;
            VelocityX = velocityX;
            VelocityY = velocityY;
            Checkpoint = 0;
            BloonStopwatch = new Stopwatch();
            BloonStopwatch.Start();
            DistanceTravelled = 0;
        }

        public Stopwatch BloonStopwatch { get; }
        public int Checkpoint { get; set; }
        public Color Color { get; }
        public double DistanceTravelled { get; set; }
        public int Health { get; set; }

        public Point2D Position
        {
            get => _position;
            set => _position = value;
        }

        public int Radius { get; }
        public float VelocityX { get; }
        public float VelocityY { get; }

        public void MoveBloonInDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    _position.X += VelocityX;
                    break;

                case Direction.Left:
                    _position.X -= VelocityX;
                    break;

                case Direction.Up:
                    _position.Y -= VelocityY;
                    break;

                case Direction.Down:
                    _position.Y += VelocityY;
                    break;
            }
        }

        public void TakeDamage(int damage)
        {
            if (damage > Health) Health = damage;
            Health -= damage;
        }
    }
}