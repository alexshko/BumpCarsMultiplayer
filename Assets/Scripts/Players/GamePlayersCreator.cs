using Fusion;
using System.Linq;
using UnityEngine;
using Zenject;

public class GamePlayersCreator : IPlayerCreate
{
    /// <summary>
    /// The prefab of the car to be instantiated
    /// </summary>
    [Inject]
    NetworkObject _carPrefab;

    /// <summary>
    /// The network runner used for instantiating across clients
    /// </summary>
    [Inject]
    NetworkRunner _networkRunner;

    /// <summary>
    /// Array of positions for starting points of the players
    /// </summary>
    Transform[] positionsForInit;

    public GamePlayersCreator()
    {
        Debug.Log("Found posiotion");
        FindInitPositions();
    }

    private void FindInitPositions()
    {
        var rootInitPos = GameObject.FindGameObjectWithTag("initPos");
        positionsForInit = rootInitPos.transform.GetComponentsInChildren<Transform>().Where(t => t != rootInitPos).ToArray();
    }

    public void CreateCarInstance(PlayerRef player)
    {
        Vector3 posToInit = positionsForInit[_networkRunner.ActivePlayers.Count()].position;
        var playerObject = _networkRunner.Spawn(_carPrefab, posToInit, Quaternion.identity, player);
        _networkRunner.SetPlayerObject(player, playerObject);
    }

    public void RemoveCarInstance(PlayerRef player)
    {
        if (_networkRunner.TryGetPlayerObject(player, out var carObj))
        {
            _networkRunner.Despawn(carObj);
        }
        _networkRunner.SetPlayerObject(player, null);
    }

}
