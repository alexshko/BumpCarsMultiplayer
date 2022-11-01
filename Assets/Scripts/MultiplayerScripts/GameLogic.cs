using alexshkorp.bumpcars.UI;
using Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace alexshkorp.bumpcars.Multiplayer
{
    public enum GameState
    {
        waitforplayer,
        running,
        end,
    }

    public class GameLogic : NetworkBehaviour , IGameLogic
    {
        /// <summary>
        /// logic of game state
        /// </summary>
        [Inject]
        IGameStateUpdate _gameState;

        /// <summary>
        /// Ref to the ball instantiation in the game
        /// </summary>
        [Inject]
        IBallController _ballController;

        [Inject]
        static IGameUIUpdate _gameUI;

        [Inject]
        NetworkRunner _runner;

        /// <summary>
        /// The players score
        /// </summary>
        [Networked(OnChanged = nameof(UpdateScore))] NetworkDictionary<PlayerRef, int> playersScore => default;

        /// <summary>
        /// The current state of the game
        /// </summary>
        [Networked] public GameState State { get; set; }


        /// <summary>
        /// Called when a player makes a goal
        /// </summary>
        /// <param name="player"></param>
        public void TakenGoal(PlayerRef player)
        {
            //find the player which is not the one who got the goal:
            var playerMadeGoal = Runner.ActivePlayers.First(p => p != player);
            if (!playersScore.ContainsKey(playerMadeGoal.PlayerId))
            {
                playersScore.Set(playerMadeGoal,0);
            }
            playersScore.Set(playerMadeGoal, playersScore.Get(playerMadeGoal) + 1);
            _gameState.CalculateGameState();
        }

        public void Start()
        {
            base.Spawned();
            if (_runner.IsServer)
            {
                Debug.Log("Instantiated state");
                State = GameState.waitforplayer;
                _gameState.CalculateGameState();
                _gameState.NotifyGameStateChange += _ballController.SetBallByGameState;
            }
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            base.Despawned(runner, hasState);
            _gameState.NotifyGameStateChange -= _ballController.SetBallByGameState;
        }

        private static void UpdateScore(Changed<GameLogic> changedVal)
        {
            changedVal.LoadNew();
            for (int i = 0; i < changedVal.Behaviour.playersScore.Count(); i++)
            {
                _gameUI.UpdateScore(i, changedVal.Behaviour.playersScore[i]);
            }
        }
    }
}