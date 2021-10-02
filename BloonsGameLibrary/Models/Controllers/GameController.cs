using SplashKitSDK;
using System.Collections.Generic;
using System.Linq;

namespace BloonsProject
{
    public class GameController
    {
        private readonly GameState _bloonSingleton = GameState.GetControllerSingletonInstance();

        public GameController()
        {
        }

        public bool CheckBloons()
        {
            return _bloonSingleton.BloonsSpawned.Count == _bloonSingleton.BloonsToBeSpawned.Count && !_bloonSingleton.BloonsSpawned.Except(_bloonSingleton.BloonsToBeSpawned).Any();
        }

        public void LoseLives(Map map)
        {
            if (_bloonSingleton.Bloons.Count <= 0) return;
            var bloonsToBeDeleted = _bloonSingleton.Bloons.Where(b => b.Checkpoint == map.Checkpoints.Count).ToList();
            foreach (var bloon in bloonsToBeDeleted)
            {
                _bloonSingleton.Player.Lives -= bloon.Health;
                _bloonSingleton.Bloons.Remove(bloon);
            }
        }

        public void SetRound(Map map, int round)
        {
            _bloonSingleton.BloonsToBeSpawned = map.BloonsPerRound(round);
            _bloonSingleton.BloonsSpawned = new Dictionary<Color, int>
            {
                [Color.Red] = 0,
                [Color.Blue] = 0,
                [Color.Green] = 0
            };
        }
    }
}