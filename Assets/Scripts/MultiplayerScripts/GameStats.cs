using Fusion;
using System;
using UnityEngine;
using Zenject;

namespace alexshkorp.bumpcars.Multiplayer
{
    public class GameStats : NetworkBehaviour, IGameStats
    {
        [Inject]
        NetworkRunner _runner;

        [Inject]
        IGameStateLogic _gameState;

        /// <summary>
        /// The players score
        /// </summary>
        [Networked(OnChanged = nameof(UpdateScore))] NetworkDictionary<PlayerRef, int> playersScore => default;

        /// <summary>
        /// The current state of the game
        /// </summary>
        [Networked(OnChanged = nameof(UpdateState))] GameState State { get; set; }


        public GameState CurrentState
        {
            get
            {
                return State;
            }
            set
            {
                State = value;
            }
        }

        public NetworkDictionary<PlayerRef, int> Score => playersScore;

        /// <summary>
        /// Action to be performed when the score changes
        /// </summary>
        public static Action<NetworkDictionary<PlayerRef, int>> ActionScoreChange { get; set; }
        /// <summary>
        /// Action to be performed when the state of the game is changed
        /// </summary>
        public static Action<GameState> ActionStateChanged { get; set; }

        /// <summary>
        /// Call this funtion to recalculate the state of the game
        /// </summary>
        public void RecalculateState()
        {
            State = _gameState.CalculateGameState(State);
        }

        public override void Spawned()
        {
            base.Spawned();
            if (_runner.IsServer)
            {
                Debug.Log("Instantiated state");
                State = GameState.waitforplayer;
                RecalculateState();
            }
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            base.Despawned(runner, hasState);
        }

        /// <summary>
        /// Called when the score is changed by server
        /// </summary>
        /// <param name="changedVal"></param>
        private static void UpdateScore(Changed<GameStats> changedVal)
        {
            changedVal.LoadNew();
            ActionScoreChange?.Invoke(changedVal.Behaviour.playersScore);
        }

        /// <summary>
        /// Called when the state of the game is changed by the server
        /// </summary>
        /// <param name="changedVal"></param>
        private static void UpdateState(Changed<GameStats> changedVal)
        {
            changedVal.LoadNew();
            ActionStateChanged?.Invoke(changedVal.Behaviour.CurrentState);
        }
    }
}