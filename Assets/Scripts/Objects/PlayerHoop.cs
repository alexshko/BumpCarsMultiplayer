using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Zenject;
using alexshkorp.bumpcars.Multiplayer;

namespace alexshkorp.bumpcars.Objects
{

    public class PlayerHoop : MonoBehaviour
    {
        [Inject]
        IGameLogic _gameLogic;

        /// <summary>
        /// The player which this hoop belogns to
        /// </summary>
        PlayerRef _playerRelated;

        bool ballDuringEnter = false;
        
        /// <summary>
        /// Seets the player this hoop is related to
        /// </summary>
        /// <param name="player"></param>
        public void SetPlayer(PlayerRef player) => _playerRelated = player;


        private void OnTriggerEnter(Collider other)
        {
            if (_playerRelated == null)
            {
                return;
            }

            //if it's the ball, register it so it makes sure it also exits (passes all the way):
            if (other.gameObject.GetComponent<Ball>() != null)
            {
                ballDuringEnter = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_playerRelated == null)
            {
                return;
            }

            if (other.gameObject.GetComponent<Ball>() != null)
            {
                ballDuringEnter = false;
            }

            //notify about the goal
            _gameLogic.TakenGoal(_playerRelated);
        }
    }
}