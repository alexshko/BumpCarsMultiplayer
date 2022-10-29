using Fusion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace alexshkorp.bumpcars.UI
{
    public class ScoreUpdate : IGameUIUpdate
    {
        [Inject]
        PlayerSettings[] settings;

        public void UpdateScore(int numOfPlayer, int score)
        {
            settings[numOfPlayer].txtScoreRef.text = score.ToString();
        }

    }
}