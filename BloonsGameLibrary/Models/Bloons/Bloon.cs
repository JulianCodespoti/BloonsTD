using SplashKitSDK;
using System.Diagnostics;

namespace BloonsProject
{
    public abstract class Bloon
    {
        private readonly float _velocityX;
        private readonly float _velocityY;
        private Point2D _position;

        public Bloon(int health, string name, Color color, int velocityX, int velocityY)
        {
            Health = health;
            Color = color;
            Radius = 15;
            _position.X = 20;
            _position.Y = 100;
            _velocityX = velocityX;
            _velocityY = velocityY;
            Checkpoint = 0;
            BloonStopwatch = new Stopwatch();
            BloonStopwatch.Start();
        }

        public Stopwatch BloonStopwatch { get; }
        public int Checkpoint { get; set; }
        public int Health { get; private set; }
        public Color Color { get; }

        public int Radius { get; }

        public Point2D Position
        {
            get => _position;
            set => _position = value;
        }

        public void TakeDamage(int damage)
        {
            if (damage > Health) Health = damage;
            Health -= damage;
        }

        public void MoveBloonInDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    _position.X += _velocityX;
                    break;

                case Direction.Left:
                    _position.X -= _velocityX;
                    break;

                case Direction.Up:
                    _position.Y -= _velocityY;
                    break;

                case Direction.Down:
                    _position.Y += _velocityY;
                    break;
            }
        }
    }
}