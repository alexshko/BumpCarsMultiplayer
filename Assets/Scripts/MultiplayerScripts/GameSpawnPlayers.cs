using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using Zenject;

public class GameSpawnPlayers : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [Inject]
    public IPlayerCreate _playersCreator;

    public void PlayerLeft(PlayerRef player)
    {
        //_playersCreator.RemoveCarInstance(player);
    }

    public void PlayerJoined(PlayerRef player)
    {
        //_playersCreator.CreateCarInstance(player);
    }
}
