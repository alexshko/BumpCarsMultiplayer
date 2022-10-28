using System;

namespace alexshkorp.bumpcars.Multiplayer
{
    public interface IGameStateUpdate
    {
        GameState CalculateGameState();
        Action<GameState> NotifyGameStateChange { get; set; }
    }
}