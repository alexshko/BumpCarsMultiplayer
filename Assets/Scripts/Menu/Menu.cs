using alexshkorp.bumpcars.Multiplayer;
using UnityEngine;
using Zenject;

namespace alexshkorp.bumpcars.Menu
{
    public class Menu : MonoBehaviour
    {
        [Inject]
        ICreateGame _createGame;

        [Inject]
        IGetMultiplayerParams _getParams;


        public void StartAsHost()
        {
            string loginName = _getParams.GetLoginName(Fusion.GameMode.AutoHostOrClient);
            string session = _getParams.GetSession(Fusion.GameMode.AutoHostOrClient);

            _createGame.StartGame(Fusion.GameMode.AutoHostOrClient, loginName, session);
        }

        public void StartAsClient()
        {
            string loginName = _getParams.GetLoginName(Fusion.GameMode.Client);
            string session = _getParams.GetSession(Fusion.GameMode.Client);

            _createGame.StartGame(Fusion.GameMode.Client, loginName, session);
        }
    }
}
