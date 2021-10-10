using System.Linq;
using SplashKitSDK;
using Color = SplashKitSDK.Color;

namespace BloonsCreator
{
    public class Program
    {
        private static void Main()
        {
            var _creatorState = CreatorState.GetClickHandlerEvents();
            var window = new Window("BloonsCreator", 800, 700);
            var drawer = new Renderer(50, 50);
            var tileEditorTool = new TileEditorTool();
            var saveButton = new SaveButton();
            var saveManager = new SaveManager();
            _creatorState.Window = window;
            _creatorState.Buttons.Add(saveButton);
            saveButton.Attach(saveManager);
            tileEditorTool.InitializeAllTilesAsGreen();
            do
            {
                SplashKit.ClearWindow(_creatorState.Window, Color.DarkGreen);
                drawer.RenderButton(saveButton.TemplateTileBitmap, saveButton.SaveButtonRectangle);
                drawer.RenderTemplateTiles(tileEditorTool.TemplateTiles);
                drawer.RenderTiles(_creatorState.Tiles);
                drawer.RenderGrid();
                var selectedButton = tileEditorTool.CurrentSelectedTileButton();
                drawer.ShadeButton(SplashKit.RectangleFrom(selectedButton.Position, selectedButton.Height, selectedButton.Width));

                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    foreach (var button in _creatorState.Buttons.Where(button => SplashKit.PointInRectangle(SplashKit.MousePosition(),
                        new Rectangle()
                        { Height = button.Height, Width = button.Width, X = button.Position.X, Y = button.Position.Y })))
                    {
                        button.PressButton();
                    }

                    tileEditorTool.AddTileAt(SplashKit.MousePosition());
                }

                SplashKit.RefreshScreen(60);
                SplashKit.ProcessEvents();
            } while (!SplashKit.WindowCloseRequested("BloonsCreator"));
        }
    }
}