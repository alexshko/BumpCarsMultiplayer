using Fusion;
using UnityEngine;
using Zenject;

public class ProjectEnjections : MonoInstaller
{
    [SerializeField] NetworkRunner networkRunnerRef;

    public override void InstallBindings()
    {
        Container.Bind<NetworkRunner>().FromComponentInNewPrefab(networkRunnerRef).AsSingle();
    }
}