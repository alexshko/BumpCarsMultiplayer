using System;

namespace alexshkorp.bumpcars.Multiplayer
{
    public interface IGameStateLogic
    {
        GameState CalculateGameState(GameState curState);
        Action<GameState> NotifyGameStateChange { get; set; }
    }
}