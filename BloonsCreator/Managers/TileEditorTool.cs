using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using Rectangle = SplashKitSDK.Rectangle;

namespace BloonsCreator
{
    public class TileEditorTool : IObserver
    {
        private readonly CreatorState _creatorState = CreatorState.GetClickHandlerEvents();

        public TileEditorTool()
        {
            TemplateTiles = new List<TileButton>
            {
                TileButtonFactory.CreateTileOfType(TileType.Normal, new Point2D() {X = 100, Y = 575}),
                TileButtonFactory.CreateTileOfType(TileType.Checkpoint, new Point2D { X = 600, Y = 575 })
        };
            foreach (var tileButton in TemplateTiles)
            {
                tileButton.Attach(this);
                _creatorState.Buttons.Add(tileButton);
            }

            SelectedTileType = TileType.Checkpoint;
        }

        public TileType SelectedTileType { get; set; }
        public List<TileButton> TemplateTiles { get; }

        public void AddTileAt(Point2D mousePosition)
        {
            if (mousePosition.Y >= 550) return;

            var tileToAdd = TileFactory.CreateTileOfType(SelectedTileType);
            tileToAdd.Position = mousePosition;
            var gridPoints = GridCalculations.GetGridPoints(tileToAdd.Width, tileToAdd.Height);
            var gridPoint = gridPoints.FirstOrDefault(p => SplashKit.PointInRectangle(tileToAdd.Position,
                new Rectangle { Width = tileToAdd.Width, Height = tileToAdd.Height, X = p.X, Y = p.Y }));
            var existingTile = _creatorState.Tiles.FirstOrDefault(t => t.Position.X == gridPoint.X && t.Position.Y == gridPoint.Y);
            tileToAdd.Position = gridPoint;
            if (tileToAdd.TileType == TileType.Checkpoint)
            {
                if (!CanAddCheckpointTile(tileToAdd as CheckpointTile)) return;
                var checkpointTile = tileToAdd as CheckpointTile;
                _creatorState.Checkpoints.Add(checkpointTile.Checkpoint);
            }

            if (existingTile is not null)
            {
                if (tileToAdd.GetType() == existingTile.GetType()) return;
                RemoveTile(existingTile);
            }

            _creatorState.Tiles.Add(tileToAdd);
        }

        public bool CanAddCheckpointTile(CheckpointTile checkpointTile)
        {
            if (_creatorState.Tiles.All(t => t.TileType != TileType.Checkpoint)) return true;
            var checkpointTiles = _creatorState.Tiles.Where(t => t.TileType == TileType.Checkpoint);
            var lastCheckPointTile = checkpointTiles.Last() as CheckpointTile;
            var distanceBetweenPoints = Math.Sqrt(
                Math.Pow((lastCheckPointTile.Checkpoint.X - checkpointTile.Checkpoint.X), 2) +
                Math.Pow((lastCheckPointTile.Checkpoint.Y - checkpointTile.Checkpoint.Y), 2));
            return distanceBetweenPoints <= lastCheckPointTile.Height;
        }

        public TileButton CurrentSelectedTileButton()
        {
            return TemplateTiles.FirstOrDefault(tileButton => tileButton.TileType == SelectedTileType);
        }

        public void InitializeAllTilesAsGreen()
        {
            var gridPoints = GridCalculations.GetGridPoints(50, 50);
            foreach (Point2D gridPoint in gridPoints)
            {
                _creatorState.Tiles.Add(new GrassTile() { Position = gridPoint });
            }
        }

        public void RemoveTile(Tile tile)
        {
            if (tile.TileType == TileType.Checkpoint)
            {
                var tileAsCheckpoint = tile as CheckpointTile;
                _creatorState.Checkpoints.Remove(tileAsCheckpoint.Checkpoint);
            }
            foreach (var t in _creatorState.Tiles.ToList()
                .Where(t => t.Position.X == tile.Position.X && t.Position.Y == tile.Position.Y))
            {
                _creatorState.Tiles.Remove(t);
            }
        }

        public void Update(ISubject subject)
        {
            var subjectAsTileButton = subject as TileButton;
            SelectedTileType = subjectAsTileButton.TileType;
        }
    }
}