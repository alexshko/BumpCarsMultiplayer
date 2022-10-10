using Fusion;

public interface IGetMultiplayerParams
{
    string GetSession(GameMode mode);

    string GetLoginName(GameMode mode);
}