using Fusion;
using System;

namespace alexshkorp.bumpcars.Multiplayer
{
    public interface IGameStats
    {
        GameState CurrentState { get; set; }
        NetworkDictionary<PlayerRef, int> Score { get; }
    }
}