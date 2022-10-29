using Fusion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace alexshkorp.bumpcars.UI
{
    public class ScoreUpdate : MonoBehaviour
    {
        [Tooltip("Reference to the score text")]
        [SerializeField] TMP_Text txtRef;

        /// <summary>
        /// The players score
        /// </summary>
        [Inject(Id = "score")]
        Dictionary<PlayerRef, int> playersScore;

        private void Update()
        {
            if (playersScore != null && playersScore.Count > 0)
            {
                //change this:
                txtRef.text = playersScore[0].ToString();
            }
        }
    }
}