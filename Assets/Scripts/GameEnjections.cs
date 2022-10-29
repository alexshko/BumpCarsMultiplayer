using alexshkorp.bumpcars.Multiplayer;
using alexshkorp.bumpcars.Objects;
using Fusion;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameEnjections : MonoInstaller
{
    [Tooltip("Prefab of the cars of players")]
    [SerializeField] NetworkObject carPrefab;

    [Tooltip("Prefab of the ball")]
    [SerializeField] NetworkObject ballPRefab;

    [Tooltip("Prefab of the GameLogic")]
    [SerializeField] NetworkObject gameLogicPrefab;

    public override void InstallBindings()
    {
        Container.Bind<Rigidbody>().FromComponentInChildren().WhenInjectedInto<PlayerMovementNetworked>();
        Container.Bind<Rigidbody>().FromComponentInChildren().WhenInjectedInto<PlayerMoveSimpleSinglePlayer>();

        Container.BindInstance<NetworkDictionary<PlayerRef, int>>(new NetworkDictionary<PlayerRef, int>()).WithId("score").AsSingle();
        Container.Bind<IGameLogic>().To<GameLogic>().FromComponentInNewPrefab(gameLogicPrefab).AsSingle();
        Container.Bind<NetworkObject>().WithId("ballPref").FromInstance(ballPRefab).AsTransient();
        Container.Bind<IGameStateUpdate>().To<GameStateUpdate>().AsSingle().NonLazy();
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