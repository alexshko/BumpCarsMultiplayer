using Fusion;

namespace alexshkorp.bumpcars.Multiplayer
{
    public interface IGameLogic
    {
        GameState State { get; set; }

        void TakenGoal(PlayerRef player);
    }
}