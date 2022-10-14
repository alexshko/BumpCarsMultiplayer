using Fusion;
using UnityEngine;
using Zenject;

public class GameEnjections : MonoInstaller
{
    public override void InstallBindings()
    {

        //register factory for creating object for player, by using NetworkRunner:
        Container.BindFactory<NetworkRunner, PlayerRef, NetworkObject, PlayerFactoryPlaceholder>().FromFactory<PlayerFactory>();
    }
}