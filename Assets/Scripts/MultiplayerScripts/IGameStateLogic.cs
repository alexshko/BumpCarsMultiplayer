using System;

namespace alexshkorp.bumpcars.Multiplayer
{
    public interface IGameStateLogic
    {
        /// <summary>
        /// Calculate the current game state
        /// if the state is changed should also call the action NotifyGameStateChange
        /// </summary>
        /// <returns></returns>
        void CalculateGameState();

        /// <summary>
        /// The current state of the game
        /// </summary>
        GameState CurrentState { get; set; }

        /// <summary>
        /// Notification whenever the game changes it's state
        /// </summary>
        Action<GameState> NotifyGameStateChange { get; set; }

        /// <summary>
        /// If between goals break, how much time remain
        /// </summary>
        float? TimeBetweenGoalsBreaksRemain { get; }
    }
}