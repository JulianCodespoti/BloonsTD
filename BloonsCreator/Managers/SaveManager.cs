using System;
using BloonsProject;
using BloonsProject.Models.Extensions;
using H.Utilities;
using SplashKitSDK;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using RandomNameGeneratorLibrary;
using Bitmap = System.Drawing.Bitmap;
using Rectangle = System.Drawing.Rectangle;

namespace BloonsCreator
{
    public class SaveManager : IObserver
    {
        private readonly CreatorState _creatorState = CreatorState.GetClickHandlerEvents();

        public static void CreateMapFor(string name, Bitmap screenshot, List<Point2D> tileManagerCheckpoints)
        {
            var serializablePoints = tileManagerCheckpoints.Select(t => SplashKitExtensions.VectorFromPoint(t)).ToList();
            var mapToSave = new Map($"../BloonsLibrary/Resources/{name}.jpeg", screenshot.Width, screenshot.Height, 25, serializablePoints, name);

            using var createStream = File.Create($"../BloonsLibrary/Maps/MapJsons/{name}.json");
            JsonSerializer.SerializeAsync(createStream, mapToSave);
        }

        public static Bitmap TakeScreenshotOf(Window window, string name)
        {
            var screenShot = Screenshoter.Shot(new Rectangle(window.X, window.Y, window.Width, window.Height - 150));
            screenShot.Save($"../BloonsLibrary/Resources/{name}.jpeg", ImageFormat.Jpeg);
            return screenShot;
        }

        public void Update(ISubject subject)
        {
            if (_creatorState.Checkpoints.Count() <= 1) return;

            var name = RandomPlaceNameExtensions.GenerateRandomPlaceName(new Random());
            var screenshot = SaveManager.TakeScreenshotOf(_creatorState.Window, name);
            SaveManager.CreateMapFor(name, screenshot, _creatorState.Checkpoints);
            Environment.Exit(0);
        }
    }
}