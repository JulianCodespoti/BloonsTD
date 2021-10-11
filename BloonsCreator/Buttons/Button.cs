using SplashKitSDK;
using System.Collections.Generic;

namespace BloonsCreator
{
    public class Button : IClickable
    {
        public Button(int width, int height, Bitmap templateTileBitmap, ButtonTypes buttonTypes)
        {
            Width = width;
            Height = height;
            TemplateTileBitmap = templateTileBitmap;
            ButtonType = buttonTypes;
        }

        public ButtonTypes ButtonType { get; set; }
        public int Height { get; }
        public Point2D Position { get; set; }
        public Bitmap TemplateTileBitmap { get; }
        public int Width { get; }

        private List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.OnClick(this);
            }
        }

        public void PressButton()
        {
            Notify();
        }
    }
}