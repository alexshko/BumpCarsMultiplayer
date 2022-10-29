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

    public class GameLogic : NetworkObject, ISpawned,IDespawned , IGameLogic
    {
        /// <summary>
        /// logic of game state
        /// </summary>
        [Inject]
        IGameStateUpdate _gameState;

        /// <summary>
        /// Ref to the ball instantiation in the game
        /// </summary>
        IBallController _ballController;

        /// <summary>
        /// The players score
        /// </summary>
        [Inject(Id = "score")]
        [Networked] public Dictionary<PlayerRef, int> playersScore { get; set; }


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
            if (!playersScore.ContainsKey(playerMadeGoal))
            {
                playersScore[playerMadeGoal] = 0;
            }
            playersScore[playerMadeGoal]++;
            _gameState.CalculateGameState();
        }

        public void Spawned()
        {
            if (Runner.IsServer)
            {
                Debug.Log("Instantiated state");
                State = GameState.waitforplayer;
                _gameState.CalculateGameState();
                _gameState.NotifyGameStateChange += _ballController.SetBallByGameState;
            }
        }

        public void Despawned(NetworkRunner runner, bool hasState)
        {
            _gameState.NotifyGameStateChange -= _ballController.SetBallByGameState;
        }
    }
}