using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alexshkorp.bumpcars.Multiplayer
{
    public enum GameState
    {
        waitforplayer,
        running,
        end,
    }

    public class GameLogic : NetworkObject, ISpawned
    {
        /// <summary>
        /// logic of game state
        /// </summary>
        IGameStateUpdate _gameState;

        /// <summary>
        /// The players score
        /// </summary>
        [Networked] public Dictionary<PlayerRef, int> playersScore { get; set; }

        /// <summary>
        /// The current state of the game
        /// </summary>
        [Networked] public GameState State { get; set; }

        

        /// <summary>
        /// Called when a player makes a goal
        /// </summary>
        /// <param name="player"></param>
        public void MakeGoal(PlayerRef player)
        {
            if (!playersScore.ContainsKey(player))
            {
                playersScore[player] = 0;
            }
            playersScore[player]++;
            _gameState.CalculateGameState();
        }

        public void Spawned()
        {
            if (Runner.IsServer)
            {
                State = GameState.waitforplayer;
            }
        }
    }
}