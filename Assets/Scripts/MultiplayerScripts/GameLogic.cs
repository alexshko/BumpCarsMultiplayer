using Fusion;
using System.Linq;
using UnityEngine;
using Zenject;

namespace alexshkorp.bumpcars.Multiplayer
{
    public enum GameState
    {
        waitforplayer,
        running,
        breakBetweenGoals,
        end,
    }

    public class GameLogic : NetworkBehaviour , IGameLogic
    {
        ///// <summary>
        ///// logic of game state
        ///// </summary>
        //[Inject]
        //static IGameStateLogic _gameState;

        /// <summary>
        /// Ref to the ball instantiation in the game
        /// </summary>
        [Inject]
        IBallController _ballController;

        //[Inject]
        //static IGameUIUpdate _gameUI;

        [Inject]
        NetworkRunner _runner;

        [Inject]
        GameStats _gameStats;

        [Inject]
        IPlayerCreate _creatNewPlayer;


        /// <summary>
        /// Called when a player makes a goal
        /// </summary>
        /// <param name="player"></param>
        public void TakenGoal(PlayerRef player)
        {
            //find the player which is not the one who got the goal:
            var playerMadeGoal = Runner.ActivePlayers.First(p => p != player);
            if (!_gameStats.Score.ContainsKey(playerMadeGoal.PlayerId))
            {
                _gameStats.Score.Set(playerMadeGoal,0);
            }
            _gameStats.Score.Set(playerMadeGoal, _gameStats.Score.Get(playerMadeGoal) + 1);
            _ballController.RecenterBall();
        }

        public override void Spawned()
        {
            base.Spawned();
            if (_runner.IsServer)
            {
                Debug.Log("Server Init");
                GameStats.ActionStateChanged += s => _ballController.SetBallByGameState(s);
                _creatNewPlayer.NotifyNewPlayerCreated += () => _gameStats.RecalculateState();
            }
        }


        private void OnDestroy()
        {
            if (_runner.IsServer)
            {
                GameStats.ActionStateChanged -= s => _ballController.SetBallByGameState(s);
                _creatNewPlayer.NotifyNewPlayerCreated -= () => _gameStats.RecalculateState();
            }
        }
    }
}