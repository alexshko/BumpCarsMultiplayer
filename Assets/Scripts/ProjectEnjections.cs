using alexshkorp.bumpcars.Multiplayer;
using Fusion;
using System;
using UnityEngine;
using Zenject;

public class ProjectEnjections : MonoInstaller
{
    [SerializeField] NetworkRunner networkRunnerRef;

    [Tooltip("Prefab of the cars of players")]
    [SerializeField] NetworkObject carPrefab;

    public override void InstallBindings()
    {
        Container.Bind<IPlayerCreate>().To<GamePlayersCreator>().AsSingle().NonLazy();
        Container.Bind<NetworkObject>().FromInstance(carPrefab).WhenInjectedInto<GamePlayersCreator>().NonLazy();
        Container.Bind<NetworkRunner>().FromComponentInNewPrefab(networkRunnerRef).AsSingle(); //.OnInstantiated(RegisterGameLogic);
        Container.Bind<IPlayerInput>().To<GamePlayerInput>().AsTransient();
    }

    private void RegisterGameLogic(InjectContext arg1, object arg2)
    {
        Container.Bind<IGameLogic>().To<GameLogic>().FromInstance(arg2 as GameLogic).AsSingle();
    }
}