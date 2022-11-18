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

        ///// <summary>
        ///// Reference to the player create methods for registering when user is created
        ///// </summary>
        //IPlayerCreate _playerCreator;

        /// <summary>
        /// The stats of the game (score and state)
        /// </summary>
        IGameStats _gameStats;

        GameState curState;
        public GameState CurrentState
        {
            get => curState;
            set 
            {
                bool shouldNotify = curState != value;
                curState = value; 

                if (shouldNotify)
                {
                    NotifyGameStateChange?.Invoke(curState);
                }
            }
        }

        private const int numOfRequiredPlayers = 2;
        private const int numOfRequiredGoals = 3;
        private const float timeToWaitBetweenGoals = 3;
        TickTimer timeElapsedBetweenGoals;

        [Inject]
        public GameStateLogic(IPlayerCreate _playerCreator, NetworkRunner _runner)
        {
            //this._playerCreator = _playerCreator;
            this._runner = _runner;
            //this._gameStats = stats;
        }

        public Action<GameState> NotifyGameStateChange { get; set ; }

        public float? TimeBetweenGoalsBreaksRemain => timeElapsedBetweenGoals.IsRunning ? timeElapsedBetweenGoals.RemainingTime(_runner) : null;


        /// <summary>
        /// Logic to calculate the state of the game
        /// </summary>
        /// <param name="curState"></param>
        /// <returns></returns>
        public void CalculateGameState()
        {
            GameState newGameSate = curState;

            if (curState == GameState.running)
            {
                if (_runner.ActivePlayers.Count() < numOfRequiredPlayers)
                {
                    newGameSate = GameState.waitforplayer;
                }
                else if (CheckIFGameEnd())
                {
                    newGameSate = GameState.end;
                }
            }
            else if (curState == GameState.waitforplayer)
            {
                if (_runner.ActivePlayers.Count() == numOfRequiredPlayers)
                {
                    newGameSate = GameState.running;
                }
            }
            else if (curState == GameState.breakBetweenGoals)
            {
                //init the timer if not running:
                if (!timeElapsedBetweenGoals.IsRunning)
                {
                    timeElapsedBetweenGoals = TickTimer.CreateFromSeconds(_runner, timeToWaitBetweenGoals);
                }
                //after the timer finished, go tot running mode:
                if (timeElapsedBetweenGoals.Expired(_runner))
                {
                    newGameSate = GameState.running;
                    timeElapsedBetweenGoals = TickTimer.None;
                }
            }

            if (newGameSate != curState)
            {
                curState = newGameSate;
                NotifyGameStateChange?.Invoke(curState);
            }
        }

        //the game is ended once the sum of goals is numOfRequiredGoals
        private bool CheckIFGameEnd() => _gameStats.Score.Select((p, s) => s).Sum() >= numOfRequiredGoals;
    }
}