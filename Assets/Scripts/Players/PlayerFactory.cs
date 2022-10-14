using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerFactoryPlaceholder : PlaceholderFactory<NetworkRunner, PlayerRef, NetworkObject>
{
}

public class PlayerFactory : IFactory<NetworkRunner, PlayerRef, NetworkObject>
{
    //NetworkRunner _runner;

    [Inject(Id ="PlayerPrefab")]
    NetworkPrefabRef playerPrefab;

    public PlayerFactory ()
    {
        //_runner = runner;
    }

    public NetworkObject Create(NetworkRunner Runner, PlayerRef player)
    {
        var networkObj = Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);
        return networkObj;
    }
}
