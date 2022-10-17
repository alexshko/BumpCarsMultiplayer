using alexshkorp.bumpcars.Multiplayer;
using UnityEngine;
using Zenject;

public class MenuEnjections : MonoInstaller
{
    [SerializeField] GetMultiplayerParams multipleParamsRef;
    public override void InstallBindings()
    {
        Container.Bind<ICreateGame>().To<CreateGame>().AsTransient();
        Container.Bind<IGetMultiplayerParams>().FromInstance(multipleParamsRef);
    }
}