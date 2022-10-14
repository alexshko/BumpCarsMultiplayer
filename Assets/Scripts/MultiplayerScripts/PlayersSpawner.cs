using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersSpawner : SimulationBehaviour, IPlayerJoined, IPlayerLeft, ISpawned
{
    public void PlayerJoined(PlayerRef player)
    {
        throw new System.NotImplementedException();
    }

    public void PlayerLeft(PlayerRef player)
    {
        throw new System.NotImplementedException();
    }

    public void Spawned()
    {
        throw new System.NotImplementedException();
    }
}
