using Fusion;
using System;

public interface IPlayerCreate
{
    void CreateCarInstance(PlayerRef player);
    void RemoveCarInstance(PlayerRef player);

    /// <summary>
    /// Notify when a new player created
    /// </summary>
    Action NotifyNewPlayerCreated { get; set; }
}