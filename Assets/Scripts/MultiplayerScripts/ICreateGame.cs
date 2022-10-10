using Fusion;
namespace alexshkorp.bumpcars.Multiplayer
{
    public interface ICreateGame
    {
        void StartGame(GameMode mode, string username, string sessionName);
    }
}