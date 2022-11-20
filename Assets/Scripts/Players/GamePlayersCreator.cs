using alexshkorp.bumpcars.Objects;
using Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace alexshkorp.bumpcars.Multiplayer
{
    public class GamePlayersCreator : IPlayerCreate, IInGamePlay
    {
        /// <summary>
        /// The prefab of the car to be instantiated
        /// </summary>
        [Inject]
        NetworkObject _carPrefab;

        /// <summary>
        /// The network runner used for instantiating across clients
        /// </summary>
        [Inject]
        NetworkRunner _networkRunner;

        public Action NotifyNewPlayerCreated { get; set; }

        /// <summary>
        /// Array of positions for starting points of the players
        /// </summary>
        Transform[] positionsForInit;


        /// <summary>
        /// Dictionary for mapping players and their position
        /// </summary>
        Dictionary<PlayerRef, Transform> playersPositions;

        public GamePlayersCreator()
        {
            Debug.Log("Found posiotion");
            playersPositions = new Dictionary<PlayerRef, Transform>();
        }

        ~GamePlayersCreator()
        {
            positionsForInit = null;
            playersPositions = null;
        }

        private void FindInitPositions()
        {
            var rootInitPos = GameObject.FindGameObjectWithTag("initPos");
            positionsForInit = rootInitPos.transform.GetComponentsInChildren<Transform>().Where(t => t != rootInitPos).ToArray();
        }

        public void CreateCarInstance(PlayerRef player)
        {
            if (positionsForInit == null || positionsForInit.Length == 0)
            {
                FindInitPositions();
            }
            Transform posToInit = positionsForInit[_networkRunner.ActivePlayers.Count()];
            var playerObject = _networkRunner.Spawn(_carPrefab, posToInit.position, posToInit.rotation, player);

            //register the players "home" transform
            if (!playersPositions.ContainsKey(playerObject.InputAuthority))
            {
                playersPositions[playerObject.InputAuthority] = posToInit;
            }
            _networkRunner.SetPlayerObject(player, playerObject);

            //update the hoop of the player:
            var hoopRef = posToInit.GetComponent<PlayerHoopReference>();
            hoopRef.hoop.SetPlayer(player);

            NotifyNewPlayerCreated?.Invoke();
        }

        public void RemoveCarInstance(PlayerRef player)
        {
            if (_networkRunner.TryGetPlayerObject(player, out var carObj))
            {
                _networkRunner.Despawn(carObj);
            }
            _networkRunner.SetPlayerObject(player, null);

            NotifyNewPlayerCreated?.Invoke();
        }

        public void ResetPlayersPositions()
        {
            var players = _networkRunner.ActivePlayers.ToArray();
            for (int i = 0; i < players.Length; i++)
            {
                _networkRunner.GetPlayerObject(players[i]).transform.position = playersPositions[players[i]].position;
            }
        }
    }
}