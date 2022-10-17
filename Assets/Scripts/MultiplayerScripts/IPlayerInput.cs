using Fusion;
using UnityEngine;

public struct PlayerInputData : INetworkInput
{
    public Vector3 directionMove;
}

public interface IPlayerInput
{
    Vector3 InputDirection { get; }
}