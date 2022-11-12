using Fusion;
using System;
using System.Linq;
using UnityEngine;
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

        private const int numOfRequiredPlayers = 2;
        private const int numOfRequiredGoals = 2;
        private const float timeToWaitBetweenGoals = 3;
        Fusion.TickTimer timeElapsedBetweenGoals;

        [Inject]
        public GameStateLogic(IPlayerCreate _playerCreator, NetworkRunner _runner)
        {
            this._playerCreator = _playerCreator;
            this._runner = _runner;
            //this._gameStats = stats;
        }

        public Action<GameState> NotifyGameStateChange { get; set ; }

        /// <summary>
        /// Logic to calculate the state of the game
        /// </summary>
        /// <param name="curState"></param>
        /// <returns></returns>
        public GameState CalculateGameState(GameState curState)
        {
            //GameState newGameSate = curState;
            if (curState == GameState.running)
            {
                if (_runner.ActivePlayers.Count() < numOfRequiredPlayers)
                {
                    return GameState.waitforplayer;
                }
            }

            else if (curState == GameState.waitforplayer)
            {
                if (_runner.ActivePlayers.Count() == numOfRequiredPlayers)
                {
                   return GameState.running;
                }
                else
                {
                    return GameState.waitforplayer;
                }
            }
            else if (curState == GameState.breakBetweenGoals)
            {
                // wait few seconds and pass to running state:
                if (!timeElapsedBetweenGoals.IsRunning)
                {
                    timeElapsedBetweenGoals = TickTimer.CreateFromSeconds(_runner, timeToWaitBetweenGoals);
                }
                if (timeElapsedBetweenGoals.Expired(_runner))
                {
                    timeElapsedBetweenGoals = TickTimer.None;
                    return GameState.running;
                }
                else
                {
                    return GameState.breakBetweenGoals;
                }
            }
            return GameState.running;
        }
    }
}