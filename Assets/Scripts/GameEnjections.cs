using Fusion;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameEnjections : MonoInstaller
{
    [Tooltip("Prefab of the cars of players")]
    [SerializeField] NetworkObject carPrefab;

    public override void InstallBindings()
    {
        Container.Bind<Rigidbody>().FromComponentInChildren().WhenInjectedInto<PlayerMovementNetworked>();
        Container.Bind<Rigidbody>().FromComponentInChildren().WhenInjectedInto<PlayerMoveSimpleSinglePlayer>();

        Container.BindInstance<Dictionary<PlayerRef, int>>(new Dictionary<PlayerRef, int>()).WithId("score").AsSingle();
    }

    private void Backup()
    {
        //Container.Bind<Rigidbody>().FromComponentInChildren().WhenInjectedInto<PlayerMovementNetworked>().NonLazy();
        //Container.Bind<Rigidbody>().FromComponentInChildren().WhenInjectedInto<PlayerMoveSimpleSinglePlayer>();
        //Container.Bind<Rigidbody>().FromInstance(carPrefab.GetComponent<Rigidbody>()).WhenInjectedInto<PlayerMovement>().NonLazy();
        //Container.Bind<IPlayerCreate>().To<GamePlayersCreator>().AsSingle().NonLazy();
        //Container.Bind<NetworkObject>().FromInstance(carPrefab).WhenInjectedInto<GamePlayersCreator>().NonLazy();
        //Container.Bind<NetworkPrefabRef>().WithId("CarPrefab").FromInstance(carPrefab).AsSingle().WhenInjectedInto<IPlayerCreate>();

        //Container.Bind<IPlayerCreate>().To<GamePlayersCreator>().FromComponentInNewPrefab(playerSpawnerPref).AsSingle();
        //register factory for creating object for player, by using NetworkRunner:
        //Container.BindFactory<NetworkRunner, PlayerRef, NetworkObject, PlayerFactoryPlaceholder>().FromFactory<PlayerFactory>();
    }
}