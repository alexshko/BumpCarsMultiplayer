using Fusion;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace alexshkorp.bumpcars.Multiplayer
{
    public class CreateGame : ICreateGame
    {
        [Inject]
        NetworkRunner _networkInst;

        public void StartGame(GameMode mode, string username, string sessionName) 
        {
            if (_networkInst == null)
            {
                Debug.LogError("Missing network runner");
            }
            Debug.Log("Connecting to multiplayer game");

            _networkInst.ProvideInput = true;
            LoadGame(mode, sessionName).ConfigureAwait(false);
        }

        private async Task LoadGame(GameMode mode, string sessionName)
        {
            StartGameArgs gameArgs = new StartGameArgs { 
                GameMode = mode,
                SessionName = sessionName,
                Scene = 1,
                PlayerCount = 2,
                SceneManager = _networkInst.GetComponent<NetworkSceneManagerDefault>()
            };
            await _networkInst.StartGame(gameArgs);

            Debug.Log("Connected to multiplayer game");

            _networkInst.SetActiveScene("Game");
        }

    }
}
