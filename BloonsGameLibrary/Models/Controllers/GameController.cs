using SplashKitSDK;
using System.Collections.Generic;
using System.Linq;

namespace BloonsProject
{
    public class GameController
    {
        private readonly GameState _gameState = GameState.GetControllerSingletonInstance();

        public GameController()
        {
        }

        public bool CheckBloons()
        {
            return _gameState.BloonsSpawned.Count == _gameState.BloonsToBeSpawned.Count && !_gameState.BloonsSpawned.Except(_gameState.BloonsToBeSpawned).Any();
        }

        public bool DepletedLives()
        {
            return _gameState.Player.Lives <= 0;
        }

        public void LoseLives(Map map)
        {
            if (_gameState.Bloons.Count <= 0) return;
            var bloonsToBeDeleted = _gameState.Bloons.Where(b => b.Checkpoint == map.Checkpoints.Count).ToList();
            foreach (var bloon in bloonsToBeDeleted)
            {
                _gameState.Player.Lives -= bloon.Health;
                _gameState.Bloons.Remove(bloon);
            }
        }

        public void SetRound(Map map, int round)
        {
            _gameState.BloonsToBeSpawned = map.BloonsPerRound(round);
            _gameState.BloonsSpawned = new Dictionary<Color, int>
            {
                [Color.Red] = 0,
                [Color.Blue] = 0,
                [Color.Green] = 0
            };
        }
    }
}