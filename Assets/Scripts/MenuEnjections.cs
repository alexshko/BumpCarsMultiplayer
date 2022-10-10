using alexshkorp.bumpcars.Multiplayer;
using Fusion;
using UnityEngine;
using Zenject;

public class MenuEnjections : MonoInstaller
{
    [SerializeField] NetworkRunner networkRunnerRef;
    [SerializeField] GetMultiplayerParams multipleParamsRef;
    public override void InstallBindings()
    {
        Container.Bind<ICreateGame>().To<CreateGame>().AsTransient();
        Container.Bind<IGetMultiplayerParams>().FromInstance(multipleParamsRef);
        Container.Bind<NetworkRunner>().WithId("StartGame").FromComponentInNewPrefab(networkRunnerRef).AsSingle();
    }
}