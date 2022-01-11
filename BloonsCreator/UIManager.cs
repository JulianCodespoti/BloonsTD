using System.Collections.Generic;
using PInvoke;
using SplashKitSDK;

namespace BloonsCreator
{
    public class UIManager
    {
        private static UIManager _state;

        public List<Button> Buttons = new List<Button>();

        public delegate void ButtonClickerHandler(Button button);

        public event ButtonClickerHandler buttonClickerEvent;

        private static readonly object Locker = new object();

        protected UIManager()
        {
        }

        public static UIManager GetClickHandlerEvents()
        {
            if (_state == null)
            {
                lock (Locker)
                {
                    if (_state == null)
                    {
                        _state = new UIManager();
                    }
                }
            }

            return _state;
        }

        public void Update()
        {
            var mousePos = SplashKit.MousePosition();
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                foreach (Button button in Buttons)
                {
                    if (SplashKit.PointInRectangle(mousePos, SplashKit.RectangleFrom(button.Position, button.Width, button.Height)))
                    {
                        buttonClickerEvent(button);
                    }
                }
            }
        }
    }
}