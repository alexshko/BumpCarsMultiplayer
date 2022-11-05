using alexshkorp.bumpcars.Multiplayer;
using alexshkorp.bumpcars.Objects;
using alexshkorp.bumpcars.UI;
using Fusion;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

[Serializable]
public struct PlayerSettings
{
    public Transform pos;
    public PlayerHoop hoopRef;
    public TMP_Text txtScoreRef;
}

public class GameEnjections : MonoInstaller
{
    [Tooltip("Prefab of the cars of players")]
    [SerializeField] NetworkObject carPrefab;

    [Tooltip("Prefab of the ball")]
    [SerializeField] NetworkObject ballPRefab;

    [Tooltip("Prefab of the GameLogic")]
    [SerializeField] NetworkObject gameLogicPrefab;

    [Tooltip("Prefab of the game state change logic")]
    [SerializeField] NetworkObject gameSateLogicPrefab;

    [SerializeField] PlayerSettings[] settings;

    public override void InstallBindings()
    {
        Container.Bind<Rigidbody>().FromComponentInChildren().WhenInjectedInto<PlayerMovementNetworked>();
        Container.Bind<Rigidbody>().FromComponentInChildren().WhenInjectedInto<PlayerMoveSimpleSinglePlayer>();
        Container.Bind<GameStats>().FromComponentInNewPrefab(gameSateLogicPrefab).AsSingle();
        Container.Bind<IGameUIUpdate>().To<ScoreUpdate>().AsSingle();
        Container.Bind<IBallController>().To<BallStateController>().AsTransient();
        Container.Bind<PlayerSettings[]>().FromInstance(settings).AsSingle();
        Container.Bind<IGameLogic>().To<GameLogic>().FromComponentInNewPrefab(gameLogicPrefab).AsSingle();
        Container.Bind<NetworkObject>().WithId("ballPref").FromInstance(ballPRefab).AsTransient();
        Container.Bind<IGameStateLogic>().To<GameStateLogic>().AsSingle().NonLazy();
    }

    private void Backup()
    {

        //NetworkDictionary<PlayerRef, int> mapping = default;
        //Container.Bind<NetworkDictionary<PlayerRef, int>>().WithId("score").FromInstance(mapping).AsSingle();
        //Container.Bind<int>().WithId("123").FromInstance(4).AsSingle();
        //Container.BindInstance<NetworkDictionary<PlayerRef, int>>(new NetworkDictionary<PlayerRef, int>()).WithId("score").AsSingle();

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