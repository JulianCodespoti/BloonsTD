using System.Collections.Generic;
using PInvoke;
using SplashKitSDK;

namespace BloonsCreator
{
    public class CreatorState
    {
        private static CreatorState _state;

        public List<Point2D> Checkpoints = new List<Point2D>();
        public List<Tile> Tiles = new List<Tile>();
        public List<Button> Buttons = new List<Button>();
        public Window Window;

        private static readonly object Locker = new object();

        protected CreatorState()
        {
        }

        public static CreatorState GetClickHandlerEvents()
        {
            if (_state == null)
            {
                lock (Locker)
                {
                    if (_state == null)
                    {
                        _state = new CreatorState();
                    }
                }
            }

            return _state;
        }
    }
}