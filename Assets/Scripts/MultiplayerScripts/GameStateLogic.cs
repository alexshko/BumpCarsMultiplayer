using Fusion;
using System;
using System.Linq;
using Zenject;

namespace alexshkorp.bumpcars.Multiplayer
{
    public class GameStateLogic : IGameStateLogic
    {
        NetworkRunner _runner;

        /// <summary>
        /// Reference to the player create methods for registering when user is created
        /// </summary>
        IPlayerCreate _playerCreator;

        ///// <summary>
        ///// The stats of the game (score and state)
        ///// </summary>
        //GameStats _gameStats;

        private const int numOfRequiredPlayers = 1;
        private const int numOfRequiredGoals = 2;

        [Inject]
        public GameStateLogic(IPlayerCreate _playerCreator, NetworkRunner _runner)
        {
            this._playerCreator = _playerCreator;
            this._runner = _runner;
            //this._gameStats = stats;
        }

        public Action<GameState> NotifyGameStateChange { get; set ; }

        public GameState CalculateGameState(GameState curState)
        {
            GameState newGameSate = curState;
            if (curState == GameState.waitforplayer)
            {
                if (_runner.ActivePlayers.Count() == numOfRequiredPlayers)
                {
                    newGameSate = GameState.running;
                }
            }
            else if (curState == GameState.running)
            {
                if (_runner.ActivePlayers.Count() < numOfRequiredPlayers)
                {
                    newGameSate = GameState.waitforplayer;
                }
            }
            else
            {

            }
            return newGameSate;
        }
    }
}