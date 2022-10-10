using alexshkorp.bumpcars.Multiplayer;
using Zenject;

public class MenuEnjections : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ICreateGame>().To<CreateGame>().AsTransient();
    }
}