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
        /// <summary>
        /// logic of game state
        /// </summary>
        [Inject]
        IGameStateLogic _gameState;

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
        IGameStats _gameStats;

        [Inject]
        IPlayerCreate _creatNewPlayer;

        [Inject]
        IInGamePlay _inGamePlay;


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

            //go to break between goals:
            _gameState.CurrentState = GameState.breakBetweenGoals;

            //respawn ball and players:
            _ballController.RecenterBall();

            //todo: add  respawn of players
        }

        public override void Spawned()
        {
            base.Spawned();
            if (_runner.IsServer)
            {
                Debug.Log("Server Init");
                _gameState.NotifyGameStateChange += UpdateStateOfGame;

                _gameStats.CurrentState = GameState.waitforplayer;
                Debug.Log("Instantiated state");
                RecalculateState();
            }
        }


        private void OnDestroy()
        {
            if (_runner.IsServer)
            {
                _gameState.NotifyGameStateChange -= UpdateStateOfGame;
            }
        }

        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();
            if (_runner.IsServer)
            {
                RecalculateState();
            }
        }

        /// <summary>
        /// Call this funtion to recalculate the state of the game
        /// </summary>
        private void RecalculateState()
        {
            _gameState.CalculateGameState();
        }

        /// <summary>
        /// Called every time the state of game is changed
        /// </summary>
        /// <param name="s"></param>
        private void UpdateStateOfGame(GameState s)
        {
            _ballController.SetBallByGameState(s);

            //update the stats state so it will get to the clients:
            _gameStats.CurrentState = s;

            //If there was g goal, then reposiotion the layers
            if (s == GameState.breakBetweenGoals)
            {
                _inGamePlay.ResetPlayersPositions();
            }
        }
    }
}