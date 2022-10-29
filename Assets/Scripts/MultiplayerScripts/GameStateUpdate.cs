using Fusion;
using System;
using System.Linq;
using Zenject;

namespace alexshkorp.bumpcars.Multiplayer
{
    public class GameStateUpdate : IGameStateUpdate
    {
        NetworkRunner _runner;

        //[Inject(Id ="score")]
        //Dictionary<PlayerRef, int> _playersScore;

        /// <summary>
        /// Reference to the player create methods for registering when user is created
        /// </summary>
        IPlayerCreate _playerCreator;

        /// <summary>
        /// The current game state
        /// </summary>
        GameState _curState;

        private const int numOfRequiredPlayers = 2;
        private const int numOfRequiredGoals = 2;

        [Inject]
        public GameStateUpdate(IPlayerCreate _playerCreator, NetworkRunner _runner)
        {
            this._playerCreator = _playerCreator;
            this._runner = _runner;
            this._playerCreator.NotifyNewPlayerCreated += () => { CalculateGameState(); };
        }

        ~GameStateUpdate() => _playerCreator.NotifyNewPlayerCreated -= () => { CalculateGameState(); };

        public Action<GameState> NotifyGameStateChange { get; set ; }

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
                if (_runner.ActivePlayers.Count() <= numOfRequiredPlayers)
                {
                    newGameSate = GameState.waitforplayer;
                }
            }
            else
            {

            }

            bool isAboutToChange = newGameSate != _curState;
            _curState = newGameSate;
            if (isAboutToChange)
            {
                NotifyGameStateChange?.Invoke(_curState);
            }
            return _curState;
        }
    }
}