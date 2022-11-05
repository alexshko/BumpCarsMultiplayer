using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using Zenject;
using alexshkorp.bumpcars.Multiplayer;

public class NetworkConnections : MonoBehaviour, INetworkRunnerCallbacks
{
    [Inject]
    public IPlayerCreate _playersCreator;

    [Inject]
    private IPlayerInput _playerInput;

    public Action ActionPlayersChanged { get; set; }
    void INetworkRunnerCallbacks.OnConnectedToServer(NetworkRunner runner)
    {
        //throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        //throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        //throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        //throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnDisconnectedFromServer(NetworkRunner runner)
    {
        //throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        //throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new PlayerInputData();
        data.directionMove = _playerInput.InputDirection;
        Debug.LogFormat("Move Input: {0}", data.directionMove);

        input.Set(data);
    }

    void INetworkRunnerCallbacks.OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        //throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.LogFormat("{0}: player joined", runner.IsServer? "Server" : "Client");
        if (runner.IsServer)
        {
            _playersCreator.CreateCarInstance(player);
            ActionPlayersChanged?.Invoke();
        }
    }

    void INetworkRunnerCallbacks.OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.LogFormat("{0}: player left", runner.IsServer ? "Server" : "Client");
        if (runner.IsServer)
        {
            _playersCreator.RemoveCarInstance(player);
        }
    }

    void INetworkRunnerCallbacks.OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        //throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnSceneLoadDone(NetworkRunner runner)
    {
        //throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnSceneLoadStart(NetworkRunner runner)
    {
        //throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        //throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        //throw new NotImplementedException();
        Debug.Log("Server Shut down");
    }

    void INetworkRunnerCallbacks.OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        //throw new NotImplementedException();
    }

}
