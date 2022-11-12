namespace alexshkorp.bumpcars.Multiplayer
{
    public interface IBallController
    {
        void SetBallByGameState(GameState state);

        void RecenterBall();
    }
}