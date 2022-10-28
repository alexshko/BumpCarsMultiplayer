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

    public class GameLogic : NetworkObject, ISpawned, IGameLogic
    {
        /// <summary>
        /// Prefab of the ball to be instatnitated when the game starts to run
        /// </summary>
        [Inject(Id ="ballPref")]
        NetworkObject ballPref;

        /// <summary>
        /// logic of game state
        /// </summary>
        [Inject]
        IGameStateUpdate _gameState;

        /// <summary>
        /// The players score
        /// </summary>

        [Inject(Id = "score")]
        [Networked] public Dictionary<PlayerRef, int> playersScore { get; set; }

        /// <summary>
        /// The current state of the game
        /// </summary>
        [Networked] public GameState State { get; set; }

        NetworkObject ballRef;

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
                _gameState.NotifyGameStateChange += InstantiateBall;
            }
        }

        /// <summary>
        /// Spawn or despawn ball according to the 
        /// </summary>
        /// <param name="state"></param>
        private void InstantiateBall(GameState state)
        {
            if (state == GameState.running && ballRef == null)
            {
                ballRef = Runner.Spawn(ballPref, new Vector3(0, 1, 0), ballPref.transform.rotation, null);
            }
            else if (ballRef != null && state != GameState.running)
            {
                Runner.Despawn(ballRef);
            }
        }
    }
}