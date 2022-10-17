using Fusion;
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
        Container.Bind<NetworkRunner>().FromComponentInNewPrefab(networkRunnerRef).AsSingle();
        Container.Bind<IPlayerInput>().To<GamePlayerInput>().AsTransient();
    }
}