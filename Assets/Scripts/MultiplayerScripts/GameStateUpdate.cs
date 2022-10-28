using Fusion;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace alexshkorp.bumpcars.Multiplayer
{
    public class GameStateUpdate : IGameStateUpdate
    {
        [Inject]
        NetworkRunner _runner;

        [Inject]
        Dictionary<PlayerRef, int> _playersScore;

        /// <summary>
        /// The current game state
        /// </summary>
        GameState _curState;

        private const int numOfRequiredPlayers = 2;
        private const int numOfRequiredGoals = 2;

        public GameState CalculateGameState()
        {
            GameState newGameSate = _curState;
            if (_curState == GameState.waitforplayer)
            {
                if (_runner.ActivePlayers.Count() == numOfRequiredPlayers)
                {
                    newGameSate = GameState.running;
                }
            }
            else if (_curState == GameState.running)
            {

            }
            else
            {

            }
            _curState = newGameSate;
            return _curState;
        }
    }
}