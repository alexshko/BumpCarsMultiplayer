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
        IBallController _ballController;

        /// <summary>
        /// The players score
        /// </summary>
        //[Inject(Id = "score")]
        [Networked] NetworkDictionary<PlayerRef, int> playersScore => default;


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

        public override void Spawned()
        {
            base.Spawned();
            if (Runner.IsServer)
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
    }
}